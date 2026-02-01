using static Totalizator.DataManager;

namespace Totalizator
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService = new();

        private readonly Size _loginSize = new Size(600, 420);
        private readonly Size _registerSize = new Size(600, 620);

        public LoginForm()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Font = new Font("Segoe UI", 10f);
            InitializeComponent();
            ApplyModeUi();

            textBoxEmail.Focus();
        }

        private bool IsLoginMode => radioModeLogin.Checked;

        private void radioModeLogin_CheckedChanged(object sender, EventArgs e) => ApplyModeUi();
        private void radioModeRegister_CheckedChanged(object sender, EventArgs e) => ApplyModeUi();

        private void ApplyModeUi()
        {
            bool isLogin = radioModeLogin.Checked;

            groupRole.Visible = isLogin;

            labelLastName.Visible = textBoxLastName.Visible = !isLogin;
            labelFirstName.Visible = textBoxFirstName.Visible = !isLogin;
            labelMiddleName.Visible = textBoxMiddleName.Visible = !isLogin;

            Text = isLogin ? "Авторизация" : "Регистрация";
            labelTitle.Text = isLogin ? "Вход" : "Регистрация";
            buttonAction.Text = isLogin ? "Войти" : "Зарегистрироваться";

            ClientSize = isLogin ? _loginSize : _registerSize;
            table.PerformLayout();
        }

        private void linkClear_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBoxLastName.Text = textBoxFirstName.Text = textBoxMiddleName.Text = "";
            textBoxEmail.Text = textBoxPassword.Text = "";
            textBoxEmail.Focus();
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните электронную почту и пароль.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (IsLoginMode)
                    DoLogin(email, password);
                else
                    DoRegister(email, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка:\n{ex.Message}", "Ошибка базы данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoLogin(string email, string password)
        {
            var authResult = _authService.Authenticate(email, password);
            if (authResult == null)
            {
                MessageBox.Show("Неверная электронная почта или пароль.", "Ошибка входа",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.Focus();
                textBoxPassword.SelectAll();
                return;
            }

            string expectedRole = radioAdmin.Checked ? "администратор" : "пользователь";

            if (!string.Equals(authResult.Role, expectedRole, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    $"Вы пытаетесь войти как «{expectedRole}»,\n" +
                    $"но учётная запись имеет роль «{authResult.Role}».",
                    "Несоответствие роли", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserInfo userInfo;
            try
            {
                userInfo = _authService.GetUserInfo(authResult.UserId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить профиль:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fullName = $"{userInfo.LastName} {userInfo.FirstName}".Trim();
            if (!string.IsNullOrWhiteSpace(userInfo.MiddleName))
                fullName += $" {userInfo.MiddleName}";

            MessageBox.Show(
                $"Вход в систему выполнен",
                "Успешный вход",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            var session = new SessionContext(
                userInfo.UserId,
                userInfo.Role,
                userInfo.Email
            );

            Form mainForm = userInfo.Role.Equals("администратор", StringComparison.OrdinalIgnoreCase)
                ? new MainAdminForm(session, userInfo)
                : new MainUserForm(session, userInfo);

            mainForm.Show();
            this.Hide();
        }

        private void DoRegister(string email, string password)
        {
            string lastName = textBoxLastName.Text.Trim();
            string firstName = textBoxFirstName.Text.Trim();
            string middleName = textBoxMiddleName.Text.Trim();

            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("Фамилия и имя — обязательные поля.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var regResult = _authService.Register(
                email: email,
                password: password,
                lastName: lastName,
                firstName: firstName,
                middleName: string.IsNullOrWhiteSpace(middleName) ? null : middleName,
                initialBalance: 0.00m,
                bcryptCost: 12);

            MessageBox.Show(
                $"Регистрация успешно завершена!\n" +
                $"ID: {regResult.UserId}\n" +
                $"Email: {regResult.Email}\n" +
                $"Роль: {regResult.Role}",
                "Добро пожаловать!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            radioModeLogin.Checked = true;
            ApplyModeUi();
            textBoxPassword.Text = "";
            textBoxEmail.Focus();
        }
    }
}