using Npgsql;
using System.Data;
using static Totalizator.DataManager;

namespace Totalizator
{
    public partial class EventDetailsForm : Form
    {
        private readonly SessionContext _session;
        private readonly int _eventId;
        private readonly bool _isAdmin;
        public event EventHandler<decimal> BalanceUpdated;

        internal EventDetailsForm(SessionContext session, int eventId)
        {
            InitializeComponent();

            _session = session;
            _eventId = eventId;
            _isAdmin = session.Role.Equals("администратор", StringComparison.OrdinalIgnoreCase);

            this.Text = $"Событие #{eventId}";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(900, 650);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            pnlBet.Visible = !_isAdmin;

            LoadEventDetails();
            LoadOutcomes();

        }

        private void LoadEventDetails()
        {
            try
            {
                using var conn = OpenConnectionAs(_session);
                using var cmd = new NpgsqlCommand(
                    "SELECT * FROM get_event_details(@eid)",
                    conn);
                cmd.Parameters.AddWithValue("eid", _eventId);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblName.Text = reader.GetString("название");
                    lblSport.Text = reader.GetString("тип_спорта");
                    lblDate.Text = reader.GetDateTime("дата_проведения").ToString("dd.MM.yyyy HH:mm");
                    lblLocation.Text = reader.GetString("место_проведения");
                    lblStatus.Text = reader.GetString("статус");

                    // Если статус не позволяет ставить — скрываем панель
                    if (!_isAdmin && lblStatus.Text != "ожидается" && lblStatus.Text != "идёт")
                    {
                        pnlBet.Visible = false;
                        lblBetInfo.Text = "Ставки на это событие больше не принимаются";
                    }
                }
                else
                {
                    MessageBox.Show("Событие не найдено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadOutcomes()
        {
            dgvOutcomes.Rows.Clear();
            dgvOutcomes.Columns.Clear();

            dgvOutcomes.Columns.Add("outcomeId", "ID");
            dgvOutcomes.Columns.Add("participant", "Участник");
            dgvOutcomes.Columns.Add("placeInTable", "Место");
            dgvOutcomes.Columns.Add("coefficient", "Коэффициент");
            dgvOutcomes.Columns.Add("result", "Результат");

            dgvOutcomes.Columns["outcomeId"].Visible = false;

            try
            {
                using var conn = OpenConnectionAs(_session);
                using var cmd = new NpgsqlCommand(
                    "SELECT * FROM get_event_outcomes(@eid)",
                    conn);
                cmd.Parameters.AddWithValue("eid", _eventId);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dgvOutcomes.Rows.Add(
                        reader.GetInt32("идентификатор_исхода"),
                        reader.GetString("участник"),
                        reader.GetInt32("место_участника_в_таблице"),
                        reader.GetDecimal("коэффициент").ToString("F2"),
                        reader.GetString("результат")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки исходов:\n{ex.Message}", "Ошибка");
            }
        }

        private void btnPlaceBet_Click(object sender, EventArgs e)
        {
            if (dgvOutcomes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите исход для ставки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvOutcomes.SelectedRows[0];

            int outcomeId = Convert.ToInt32(selectedRow.Cells["outcomeId"].Value);
            string participant = selectedRow.Cells["participant"].Value?.ToString() ?? "неизвестный участник";
            string coefStr = selectedRow.Cells["coefficient"].Value?.ToString() ?? "0";

            if (!decimal.TryParse(coefStr, out decimal coef) || coef < 1.01m)
            {
                MessageBox.Show("Некорректный коэффициент.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var betForm = new Form
            {
                Text = $"Ставка на {participant}",
                Size = new Size(420, 220),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lbl = new Label { Text = $"Исход: {participant}\nКоэффициент: {coef:F2}\nСумма ставки (₽):", Location = new Point(20, 20), AutoSize = true };
            var txtAmount = new TextBox { Location = new Point(20, 80), Width = 200, Text = "100" };
            var btnOk = new Button { Text = "Поставить", Location = new Point(20, 130), DialogResult = DialogResult.OK };
            var btnCancel = new Button { Text = "Отмена", Location = new Point(120, 130), DialogResult = DialogResult.Cancel };

            betForm.Controls.Add(lbl);
            betForm.Controls.Add(txtAmount);
            betForm.Controls.Add(btnOk);
            betForm.Controls.Add(btnCancel);

            betForm.AcceptButton = btnOk;
            txtAmount.Focus();
            txtAmount.SelectAll();

            if (betForm.ShowDialog(this) != DialogResult.OK)
                return;

            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму больше 0.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = DataManager.OpenConnectionAs(_session);
                using var cmd = new NpgsqlCommand(
                    "SELECT place_bet(@uid, @eid, @outcome_id, @amount, @place)",
                    conn);

                cmd.Parameters.AddWithValue("@uid", _session.UserId);
                cmd.Parameters.AddWithValue("@eid", _eventId);
                cmd.Parameters.AddWithValue("@outcome_id", outcomeId);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@place", 1);

                var result = cmd.ExecuteScalar();

                if (result is bool success && success)
                {
                    MessageBox.Show(
                        $"Ставка {amount:N2} ₽ на «{participant}» (кэф {coef:F2}) успешно принята!",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    using var infoCmd = new NpgsqlCommand(
                        "SELECT * FROM get_user_info(@uid)",
                        conn);

                    infoCmd.Parameters.AddWithValue("@uid", _session.UserId);

                    using var reader = infoCmd.ExecuteReader();

                    if (reader.Read())
                    {
                        decimal newBalance = reader.GetDecimal(reader.GetOrdinal("баланс"));
                        BalanceUpdated?.Invoke(this, newBalance);
                    }
                }
                else
                {
                    MessageBox.Show("Ставка не была принята (функция вернула false или null).",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (PostgresException pgEx)
            {
                MessageBox.Show($"Ошибка базы данных:\n{pgEx.MessageText}", "Ошибка PostgreSQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось разместить ставку:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}