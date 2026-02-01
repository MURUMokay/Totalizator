using Npgsql;
using System.Data;

namespace Totalizator
{
    internal static class DataManager
    {

        public static string ConnectionString { get; } =
            "Host=127.0.0.1;Port=5432;Database=postgres;Username=totalizator_app;Password=CHANGE_ME_APP_PASSWORD";

        public static NpgsqlConnection CreateConnection() => new(ConnectionString);

        public static NpgsqlConnection OpenAppConnection()
        {
            var connection = CreateConnection();
            connection.Open();
            return connection;
        }

        public static NpgsqlConnection OpenConnectionAs(SessionContext session)
        {
            var connection = OpenAppConnection();
            ApplySessionContext(connection, session);
            ApplyExecRole(connection, session.Role);
            return connection;
        }

        private static void ApplyExecRole(NpgsqlConnection connection, string appRole)
        {
            var dbRole = MapAppRoleToDbExecRole(appRole);
            using var cmd = new NpgsqlCommand($"SET ROLE {dbRole};", connection);
            cmd.ExecuteNonQuery();
        }

        private static void ApplySessionContext(NpgsqlConnection connection, SessionContext session)
        {
            const string sql = @"
                SELECT set_config('app.user_id', @uid::text, false),
                       set_config('app.role', @role, false);";

            using var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("uid", session.UserId);
            cmd.Parameters.AddWithValue("role", session.Role);
            cmd.ExecuteNonQuery();
        }

        private static string MapAppRoleToDbExecRole(string appRole)
        {
            return string.Equals(appRole, "администратор", StringComparison.OrdinalIgnoreCase)
                ? "totalizator_admin_exec"
                : "totalizator_user_exec";
        }

        public static bool EnsureConnection()
        {
            try
            {
                using var connection = OpenAppConnection();
                return true;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Postgres error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Other error: {ex.Message}");
                return false;
            }
        }

        internal sealed class AuthService
        {
            public AuthResult? Authenticate(string email, string password)
            {
                const string sql = @"SELECT * FROM public.authenticate_user(@email, @password);";

                using var connection = OpenAppConnection();
                using var command = new NpgsqlCommand(sql, connection);

                command.Parameters.AddWithValue("email", email.Trim());
                command.Parameters.AddWithValue("password", password);

                using var reader = command.ExecuteReader();
                if (!reader.Read())
                    return null;

                return new AuthResult
                {
                    UserId = reader.GetInt32(reader.GetOrdinal("идентификатор_пользователя")),
                    Role = reader.GetString(reader.GetOrdinal("роль")),
                    Balance = reader.GetDecimal(reader.GetOrdinal("баланс")),
                    LastName = reader.GetString(reader.GetOrdinal("фамилия")),
                    FirstName = reader.GetString(reader.GetOrdinal("имя")),
                    MiddleName = reader.IsDBNull(reader.GetOrdinal("отчество")) ? null : reader.GetString(reader.GetOrdinal("отчество")),
                    Email = reader.GetString(reader.GetOrdinal("электронная_почта")),
                };
            }
            public RegisterResult Register(
                string email,
                string password,
                string lastName,
                string firstName,
                string? middleName = null,
                decimal initialBalance = 0.00m,
                int bcryptCost = 12)
            {
                const string sql = @"SELECT * FROM public.register_user(
                    @email, @password, @last, @first, @middle, @balance, @cost
                );";

                using var connection = OpenAppConnection();
                using var command = new NpgsqlCommand(sql, connection);

                command.Parameters.AddWithValue("email", email.Trim());
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("last", lastName.Trim());
                command.Parameters.AddWithValue("first", firstName.Trim());
                command.Parameters.AddWithValue("middle", (object?)middleName?.Trim() ?? DBNull.Value);
                command.Parameters.AddWithValue("balance", initialBalance);
                command.Parameters.AddWithValue("cost", bcryptCost);

                using var reader = command.ExecuteReader();
                if (!reader.Read())
                    throw new InvalidOperationException("Регистрация не вернула результат.");

                return new RegisterResult
                {
                    UserId = reader.GetInt32(reader.GetOrdinal("идентификатор_пользователя")),
                    Email = reader.GetString(reader.GetOrdinal("электронная_почта")),
                    Role = reader.GetString(reader.GetOrdinal("роль")),
                    RegisteredAt = reader.GetDateTime(reader.GetOrdinal("дата_регистрации"))
                };
            }

            public UserInfo GetUserInfo(int userId)
            {
                const string sql = "SELECT * FROM public.get_user_info(@p_user_id);";

                using var conn = OpenAppConnection();
                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("p_user_id", userId);

                using var reader = cmd.ExecuteReader();

                if (!reader.Read())
                    throw new InvalidOperationException($"Пользователь {userId} не найден");

                return new UserInfo(
                    UserId: reader.GetInt32("идентификатор_пользователя"),
                    LastName: reader.GetString("фамилия"),
                    FirstName: reader.GetString("имя"),
                    MiddleName: reader.IsDBNull("отчество") ? null : reader.GetString("отчество"),
                    Email: reader.GetString("электронная_почта"),
                    Role: reader.GetString("роль"),
                    Balance: reader.GetDecimal("баланс"),
                    RegisteredAt: reader.GetDateTime("дата_регистрации")
                );
            }


            internal sealed class AuthResult
            {
                public int UserId { get; init; }
                public string Role { get; init; } = string.Empty;
                public decimal Balance { get; init; }
                public string LastName { get; init; } = string.Empty;
                public string FirstName { get; init; } = string.Empty;
                public string? MiddleName { get; init; }
                public string Email { get; init; } = string.Empty;
            }

            internal sealed class RegisterResult
            {
                public int UserId { get; init; }
                public string Email { get; init; } = string.Empty;
                public string Role { get; init; } = string.Empty;
                public DateTime RegisteredAt { get; init; }
            }
        }

        // Контекст для форм
        public sealed record SessionContext(int UserId, string Role, string Email);

        // Полная информация о пользователе для интерфейса
        public sealed record UserInfo(int UserId, string LastName, string FirstName, string? MiddleName, string Email, string Role, decimal Balance, DateTime RegisteredAt);
    }
}