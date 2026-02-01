using Npgsql;
using System.Data;
using static Totalizator.DataManager;

namespace Totalizator
{
    public partial class MainUserForm : Form
    {
        private readonly SessionContext _session;
        private UserInfo _userInfo;
        public event EventHandler<decimal> BalanceUpdated;
        // Разделы
        private DataGridView dgvEvents;
        private DataGridView dgvBets;
        private DataGridView dgvPayouts;

        // Поиск
        private TextBox txtSearch;
        private System.Windows.Forms.Timer debounceTimer;
        private string currentSearchText = "";

        // Текущий режим
        private string currentMode = "events";

        // Для формы пополнения
        private Form balanceForm;

        internal MainUserForm(SessionContext session, UserInfo userInfo)
        {
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _userInfo = userInfo ?? throw new ArgumentNullException(nameof(userInfo));

            // Инфо о пользователе
            string fullName = $"{_userInfo.LastName} {_userInfo.FirstName}";
            if (!string.IsNullOrWhiteSpace(_userInfo.MiddleName))
                fullName += $" {_userInfo.MiddleName}";

            lblUserName.Text = fullName;
            lblBalance.Text = $"{_userInfo.Balance:N2} ₽";
            lblEmail.Text = _userInfo.Email;

            this.Text = $"Приложение тотализатор (пользователь) • {fullName}";

            dgvEvents = CreateDataGridView("События");
            dgvEvents.DoubleClick += new EventHandler(dgvEvents_DoubleClick);
            dgvBets = CreateDataGridView("Мои ставки");
            dgvPayouts = CreateDataGridView("Мои выплаты");

            txtSearch = new TextBox
            {
                Dock = DockStyle.Top,
                Height = 28,
                Font = new System.Drawing.Font("Segoe UI", 10f),
                Margin = new Padding(8, 8, 8, 4),
                PlaceholderText = "Поиск по названию события или виду спорта...",
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            debounceTimer = new System.Windows.Forms.Timer { Interval = 400 };
            debounceTimer.Tick += DebounceTimer_Tick;

            // По умолчанию: события
            ShowEventsPanel();

        }

        private DataGridView CreateDataGridView(string title)
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = System.Drawing.Color.White
            };
        }

        // Переключение режимов
        private void ShowEventsPanel()
        {
            currentMode = "events";
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(txtSearch);
            pnlContent.Controls.Add(dgvEvents);
            txtSearch.Visible = true;
            txtSearch.BringToFront();
            LoadActiveEvents(currentSearchText);
        }

        private void ShowBetsPanel()
        {
            currentMode = "bets";
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(dgvBets);
            txtSearch.Visible = false;
            LoadMyBets();
        }

        private void ShowPayoutsPanel()
        {
            currentMode = "payouts";
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(dgvPayouts);
            txtSearch.Visible = false;
            LoadMyPayouts();
        }

        // Загрузка активных событий
        private void LoadActiveEvents(string search = "")
        {
            try
            {
                using var conn = DataManager.OpenConnectionAs(_session);
                using var cmd = new NpgsqlCommand(
                    "SELECT * FROM public.get_active_events(@search, @lim, @off)",
                    conn);

                cmd.Parameters.AddWithValue("search", search.Trim());
                cmd.Parameters.AddWithValue("lim", 50);
                cmd.Parameters.AddWithValue("off", 0);

                using var adapter = new NpgsqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);

                dgvEvents.DataSource = dt;

                // Настройка колонок
                if (dgvEvents.Columns["идентификатор_события"] != null)
                {
                    dgvEvents.Columns["идентификатор_события"].Visible = false;
                }

                if (dgvEvents.Columns["название"] != null)
                    dgvEvents.Columns["название"].HeaderText = "Событие";

                if (dgvEvents.Columns["тип_спорта"] != null)
                    dgvEvents.Columns["тип_спорта"].HeaderText = "Вид спорта";

                if (dgvEvents.Columns["дата_проведения"] != null)
                {
                    dgvEvents.Columns["дата_проведения"].HeaderText = "Дата и время";
                    dgvEvents.Columns["дата_проведения"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                }

                if (dgvEvents.Columns["место_проведения"] != null)
                    dgvEvents.Columns["место_проведения"].HeaderText = "Место";

                if (dgvEvents.Columns["статус"] != null)
                    dgvEvents.Columns["статус"].HeaderText = "Статус";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить активные события:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Поиск
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (currentMode != "events") return;

            currentSearchText = txtSearch.Text.Trim();
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            debounceTimer.Stop();
            LoadActiveEvents(currentSearchText);
        }

        // Загрузка ставок пользователя
        private void LoadMyBets(int limit = 25, int offset = 0)
        {
            try
            {
                using var conn = DataManager.OpenConnectionAs(_session);
                using var cmd = new NpgsqlCommand(
                    "SELECT * FROM public.get_my_bets(@uid, @lim, @off)", conn);

                cmd.Parameters.AddWithValue("uid", _session.UserId);
                cmd.Parameters.AddWithValue("lim", limit);
                cmd.Parameters.AddWithValue("off", offset);

                using var adapter = new NpgsqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);

                dgvBets.DataSource = dt;

                // Настройка заголовков
                if (dgvBets.Columns["ставка_id"] != null) dgvBets.Columns["ставка_id"].HeaderText = "ID";
                if (dgvBets.Columns["дата_ставки"] != null) dgvBets.Columns["дата_ставки"].HeaderText = "Дата";
                if (dgvBets.Columns["событие"] != null) dgvBets.Columns["событие"].HeaderText = "Событие";
                if (dgvBets.Columns["исход"] != null) dgvBets.Columns["исход"].HeaderText = "Исход";
                if (dgvBets.Columns["коэффициент"] != null) dgvBets.Columns["коэффициент"].HeaderText = "Коэф.";
                if (dgvBets.Columns["сумма_ставки"] != null) dgvBets.Columns["сумма_ставки"].HeaderText = "Сумма";
                if (dgvBets.Columns["статус"] != null) dgvBets.Columns["статус"].HeaderText = "Статус";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить мои ставки:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Загрузка моих выплат
        private void LoadMyPayouts(int limit = 15, int offset = 0)
        {
            try
            {
                using var conn = DataManager.OpenConnectionAs(_session);
                using var cmd = new NpgsqlCommand(
                    "SELECT * FROM public.get_my_payouts(@uid, @lim, @off)", conn);

                cmd.Parameters.AddWithValue("uid", _session.UserId);
                cmd.Parameters.AddWithValue("lim", limit);
                cmd.Parameters.AddWithValue("off", offset);

                using var adapter = new NpgsqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);

                dgvPayouts.DataSource = dt;

                // Настройка заголовков
                if (dgvPayouts.Columns["выплата_id"] != null) dgvPayouts.Columns["выплата_id"].HeaderText = "ID";
                if (dgvPayouts.Columns["ставка_id"] != null) dgvPayouts.Columns["ставка_id"].HeaderText = "№ ставки";
                if (dgvPayouts.Columns["дата_выплаты"] != null) dgvPayouts.Columns["дата_выплаты"].HeaderText = "Дата выплаты";
                if (dgvPayouts.Columns["сумма_выплаты"] != null) dgvPayouts.Columns["сумма_выплаты"].HeaderText = "Сумма";
                if (dgvPayouts.Columns["событие"] != null) dgvPayouts.Columns["событие"].HeaderText = "Событие";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить мои выплаты:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик кнопки «+» (пополнение баланса)
        private void btnAddBalance_Click(object sender, EventArgs e)
        {
            if (balanceForm != null && !balanceForm.IsDisposed)
            {
                balanceForm.Activate();
                return;
            }

            balanceForm = new Form
            {
                Text = "Пополнение баланса",
                Size = new Size(380, 220),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false
            };

            var lblPrompt = new Label
            {
                Text = "Введите сумму пополнения (₽):",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };

            var txtAmount = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(320, 28),
                Font = new Font("Segoe UI", 11F)
            };

            var btnConfirm = new Button
            {
                Text = "Пополнить",
                Location = new Point(20, 110),
                Size = new Size(320, 40),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            var lblError = new Label
            {
                ForeColor = Color.Red,
                Location = new Point(20, 160),
                Size = new Size(320, 30),
                AutoSize = true
            };

            btnConfirm.Click += async (s, ev) =>
            {
                lblError.Text = "";

                if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
                {
                    lblError.Text = "Введите корректную сумму больше 0";
                    return;
                }

                try
                {
                    using var conn = DataManager.OpenConnectionAs(_session);

                    // 1. Пополняем баланс
                    using (var cmd = new NpgsqlCommand("SELECT public.add_funds(@uid, @summa)", conn))
                    {
                        cmd.Parameters.AddWithValue("uid", _session.UserId);
                        cmd.Parameters.AddWithValue("summa", amount);

                        var result = await cmd.ExecuteScalarAsync();

                        if (result is not true)
                        {
                            lblError.Text = "Не удалось пополнить баланс";
                            return;
                        }
                    }

                    // 2. Получаем актуальный баланс
                    using var infoCmd = new NpgsqlCommand(
                        "SELECT баланс FROM get_user_info(@uid)",
                        conn);
                    infoCmd.Parameters.AddWithValue("@uid", _session.UserId);

                    var newBalanceObj = await infoCmd.ExecuteScalarAsync();

                    if (newBalanceObj != null && newBalanceObj != DBNull.Value)
                    {
                        decimal newBalance = Convert.ToDecimal(newBalanceObj);

                        UpdateBalanceDisplay(newBalance);

                        MessageBox.Show($"Баланс успешно пополнен на {amount:N2} ₽\nТекущий баланс: {newBalance:N2} ₽",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        balanceForm.Close();
                    }
                    else
                    {
                        lblError.Text = "Не удалось получить обновлённый баланс";
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "Ошибка: " + ex.Message;
                }
            };

            balanceForm.Controls.Add(lblPrompt);
            balanceForm.Controls.Add(txtAmount);
            balanceForm.Controls.Add(btnConfirm);
            balanceForm.Controls.Add(lblError);

            txtAmount.Focus();

            balanceForm.FormClosed += (s, ev) => balanceForm = null;

            balanceForm.ShowDialog(this);
        }
        public void UpdateBalanceDisplay(decimal newBalance)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateBalanceDisplay(newBalance)));
                return;
            }

            _userInfo = _userInfo with { Balance = newBalance };
            lblBalance.Text = $"{newBalance:N2} ₽";
        }
        private void dgvEvents_DoubleClick(object sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 0) return;

            int eventId = Convert.ToInt32(dgvEvents.SelectedRows[0].Cells["идентификатор_события"].Value);

            var detailsForm = new EventDetailsForm(_session, eventId);

            detailsForm.BalanceUpdated += DetailsForm_BalanceUpdated;

            detailsForm.ShowDialog(this);
        }

        private void DetailsForm_BalanceUpdated(object sender, decimal newBalance)
        {
            _userInfo = _userInfo with { Balance = newBalance };
            lblBalance.Text = $"{newBalance:N2} ₽";
        }
        private void btnEvents_Click(object sender, EventArgs e)
        {
            ShowEventsPanel();
        }

        private void btnMyBets_Click(object sender, EventArgs e)
        {
            ShowBetsPanel();
        }

        private void btnMyPayouts_Click(object sender, EventArgs e)
        {
            ShowPayoutsPanel();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Выйти из аккаунта?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();

                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                loginForm.FormClosed += (s, args) =>
                {
                    if (loginForm.DialogResult != DialogResult.OK)
                    {
                        Application.Exit();
                    }
                };
            }
        }
    }
}