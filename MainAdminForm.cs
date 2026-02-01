using Npgsql;
using System.Data;
using static Totalizator.DataManager;

namespace Totalizator
{
    public partial class MainAdminForm : Form
    {
        private readonly SessionContext _session;
        private readonly UserInfo _userInfo;

        private string currentSearchText = "";
        private System.Windows.Forms.Timer debounceTimer;

        internal MainAdminForm(SessionContext session, UserInfo userInfo)
        {
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _userInfo = userInfo ?? throw new ArgumentNullException(nameof(userInfo));

            string fullName = "Администратор";
            lblUserName.Text = fullName;
            lblEmail.Text = _userInfo.Email;
            lblBalance.Text = "—";
            lblBalance.Visible = false;

            this.Text = $"Тотализатор — Администрирование • {_userInfo.Email}";

            // Таймер для поиска
            debounceTimer = new System.Windows.Forms.Timer { Interval = 400 };
            debounceTimer.Tick += DebounceTimer_Tick;

            // Привязка событий
            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnEvents.Click += (s, e) => ShowEventsPanel();
            btnCompleteEvent.Click += BtnCompleteEvent_Click;
            btnAddTestEvent.Click += BtnAddTestEvent_Click;
            btnLogout.Click += BtnLogout_Click;

            SetupDataGridColumns();
            // События по умолчанию
            ShowEventsPanel();
        }

        private void SetupDataGridColumns()
        {
            dgvEvents.AutoGenerateColumns = true;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.ReadOnly = true;
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.RowHeadersVisible = false;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.BackgroundColor = Color.White;
        }
        private void LoadEvents()
        {
            try
            {
                using var conn = OpenConnectionAs(_session);
                using var cmd = new NpgsqlCommand(
                "SELECT * FROM public.admin_get_all_events(@search, @lim, @off)",
                conn);

                cmd.Parameters.AddWithValue("search", currentSearchText ?? "");
                cmd.Parameters.AddWithValue("lim", 255);
                cmd.Parameters.AddWithValue("off", 0);

                using var adapter = new NpgsqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);

                dgvEvents.DataSource = dt;

                if (dgvEvents.Columns.Contains("идентификатор_события"))
                    dgvEvents.Columns["идентификатор_события"].HeaderText = "ID";

                if (dgvEvents.Columns.Contains("название"))
                    dgvEvents.Columns["название"].HeaderText = "Событие";

                if (dgvEvents.Columns.Contains("тип_спорта"))
                    dgvEvents.Columns["тип_спорта"].HeaderText = "Вид спорта";

                if (dgvEvents.Columns.Contains("дата_проведения"))
                {
                    dgvEvents.Columns["дата_проведения"].HeaderText = "Дата и время";
                    dgvEvents.Columns["дата_проведения"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                }

                if (dgvEvents.Columns.Contains("место_проведения"))
                    dgvEvents.Columns["место_проведения"].HeaderText = "Место";

                if (dgvEvents.Columns.Contains("статус"))
                    dgvEvents.Columns["статус"].HeaderText = "Статус";


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки событий:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEventsPanel()
        {
            LoadEvents();
        }



        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            currentSearchText = txtSearch.Text.Trim();
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            debounceTimer.Stop();
            LoadEvents();
        }

        private void BtnCompleteEvent_Click(object sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите гонку для завершения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvEvents.SelectedRows[0];
            int eventId = Convert.ToInt32(row.Cells["идентификатор_события"].Value);
            string eventName = row.Cells["название"].Value?.ToString() ?? "Гонка";

            var confirm = MessageBox.Show(
                $"Завершить гонку «{eventName}»?\nРезультаты будут сгенерированы случайно.",
                "Подтвердить завершение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = OpenConnectionAs(_session);

                var cmd = new NpgsqlCommand(
                    "SELECT идентификатор_исхода, участник FROM get_event_outcomes(@eid)",
                    conn);
                cmd.Parameters.AddWithValue("@eid", eventId);

                var participants = new List<string>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        participants.Add(reader.GetString("участник"));
                    }
                }

                if (participants.Count == 0)
                {
                    MessageBox.Show("В гонке нет участников!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var random = new Random();
                var shuffled = participants.OrderBy(x => random.Next()).ToList();

                var results = new Dictionary<string, int>();
                for (int i = 0; i < shuffled.Count; i++)
                {
                    results[shuffled[i]] = i + 1;
                }

                string jsonResults = System.Text.Json.JsonSerializer.Serialize(results);

                using var cmdComplete = new NpgsqlCommand(
                    "SELECT complete_event(@eid, @results::jsonb)",
                    conn);
                cmdComplete.Parameters.AddWithValue("@eid", eventId);
                cmdComplete.Parameters.AddWithValue("@results", jsonResults);

                var message = cmdComplete.ExecuteScalar()?.ToString() ?? "Гонка завершена";

                var resultText = $"Гонка «{eventName}» завершена!\n\nПорядок мест:\n";
                for (int i = 0; i < shuffled.Count; i++)
                {
                    resultText += $"{i + 1}-е место: {shuffled[i]}\n";
                }

                MessageBox.Show(resultText + $"\n{message}", "Результаты гонки",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка завершения гонки:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddTestEvent_Click(object sender, EventArgs e)
        {

            var raceTypes = new[]
            {
                "F1", "Formula E", "F2", "NASCAR", "Dakar Rally", "Le Mans"
            };

            var random = new Random();
            string sport = raceTypes[random.Next(raceTypes.Length)];

            // Случайное название гонки
            var names = new[]
            {
                "Гран-при {0}",
                "24 часа {0}",
                "Ралли {0}",
                "Этап чемпионата {0}",
                "Гонка {0}",
                "Чемпионат {0}",
                "Суперприз {0}",
                "Кубок {0}"
            };

            // Случайное название
            string name = string.Format(
                names[random.Next(names.Length)],
                sport);

            string[] locations =
            {
                "Монако", "Сингапур", "Спа-Франкоршам", "Дейтона", "Париж-Дакар", "Сахара",
                "Мехико", "Лас-Вегас", "Нюрбургринг", "Сузука", "Ле-Ман", "Дубай"
            };

            string location = locations[random.Next(locations.Length)];

            DateTime eventDate = DateTime.UtcNow.AddHours(25 + random.Next(0, 48)); // от 25 до 73 часов вперёд


            var genericDrivers = new[]
            {
                "Red Horizon Racing",
                "Black Thunder Motorsport",
                "Silver Arrow Team",
                "Neon Velocity",
                "Stormbreaker Racing",
                "Phantom Speed",
                "Eclipse Motorsport",
                "Titan Racing",
                "Vortex Squad",
                "Ignite Performance",
                "Quantum Drift",
                "Apex Legion",
                "Nitro Phoenix",
                "Ravage Racing",
                "Blaze Circuit"
            };

            int participantCount = random.Next(5, 9);
            var participants = genericDrivers
                .OrderBy(x => random.Next())
                .Take(participantCount)
                .ToList();

            // Кэф. от 1.01
            var coeffs = new decimal[participantCount];
            for (int i = 0; i < participantCount; i++)
            {
                coeffs[i] = Math.Round((decimal)(1.01 + random.NextDouble() * random.Next(1, 5)), 2);
            }

            try
            {
                using var conn = OpenConnectionAs(_session);
                using var cmd = new NpgsqlCommand(
                    @"SELECT admin_insert_event_with_outcomes(
                    @name, @sport, @date, @location, @outcomes, @coeffs)",
                    conn);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@sport", sport);
                cmd.Parameters.AddWithValue("@date", eventDate);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@outcomes", participants.ToArray());
                cmd.Parameters.AddWithValue("@coeffs", coeffs);

                var newIdObj = cmd.ExecuteScalar();

                if (newIdObj is int newId && newId > 0)
                {
                    MessageBox.Show(
                        $"Добавлена новая гонка!\n" +
                        $"Тип: {sport}\n" +
                        $"Название: {name}\n" +
                        $"Дата: {eventDate:dd.MM.yyyy HH:mm}\n" +
                        $"Место: {location}\n" +
                        $"ID события: {newId}",
                        "Успех",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления гонки:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnAddEventManual_Click(object sender, EventArgs e)
        {
            using var inputForm = new Form
            {
                Text = "Добавление нового события",
                Size = new Size(400, 350),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblName = new Label { Text = "Название события:", Location = new Point(20, 20), AutoSize = true };
            var txtName = new TextBox { Location = new Point(20, 45), Width = 340 };
            var lblSport = new Label { Text = "Тип события (спорта):", Location = new Point(20, 80), AutoSize = true };
            var txtSport = new TextBox { Location = new Point(20, 105), Width = 340 };
            var lblLocation = new Label { Text = "Место проведения:", Location = new Point(20, 140), AutoSize = true };
            var txtLocation = new TextBox { Location = new Point(20, 165), Width = 340 };
            var lblDate = new Label { Text = "Дата и время:", Location = new Point(20, 200), AutoSize = true };
            var dtpDate = new DateTimePicker
            {
                Location = new Point(20, 225),
                Width = 340,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd.MM.yyyy HH:mm",
                MinDate = DateTime.UtcNow.AddHours(1)
            };
            var btnOk = new Button { Text = "Добавить", Location = new Point(20, 270), Width = 150, DialogResult = DialogResult.OK };
            var btnCancel = new Button { Text = "Отмена", Location = new Point(190, 270), Width = 150, DialogResult = DialogResult.Cancel };

            inputForm.Controls.Add(lblName);
            inputForm.Controls.Add(txtName);
            inputForm.Controls.Add(lblSport);
            inputForm.Controls.Add(txtSport);
            inputForm.Controls.Add(lblLocation);
            inputForm.Controls.Add(txtLocation);
            inputForm.Controls.Add(lblDate);
            inputForm.Controls.Add(dtpDate);
            inputForm.Controls.Add(btnOk);
            inputForm.Controls.Add(btnCancel);

            inputForm.AcceptButton = btnOk;
            inputForm.CancelButton = btnCancel;

            if (inputForm.ShowDialog(this) != DialogResult.OK)
                return;

            string name = txtName.Text.Trim();
            string sport = txtSport.Text.Trim();
            string location = txtLocation.Text.Trim();
            DateTime eventDate = dtpDate.Value.ToUniversalTime();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(sport) || string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var random = new Random();
            var genericDrivers = new[]
            {
                "Red Horizon Racing", "Black Thunder Motorsport", "Silver Arrow Team",
                "Neon Velocity", "Stormbreaker Racing", "Phantom Speed",
                "Eclipse Motorsport", "Titan Racing", "Vortex Squad",
                "Ignite Performance", "Quantum Drift", "Apex Legion",
                "Nitro Phoenix", "Ravage Racing", "Blaze Circuit"
            };

            int participantCount = random.Next(5, 9);
            var participants = genericDrivers
                .OrderBy(x => random.Next())
                .Take(participantCount)
                .ToList();

            var coeffs = new decimal[participantCount];
            for (int i = 0; i < participantCount; i++)
            {
                coeffs[i] = Math.Round((decimal)(1.01 + random.NextDouble() * random.Next(1, 5)), 2);
            }

            try
            {
                using var conn = OpenConnectionAs(_session);
                using var cmd = new NpgsqlCommand(
                    @"SELECT admin_insert_event_with_outcomes(
                @name, @sport, @date, @location, @outcomes, @coeffs)",
                    conn);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@sport", sport);
                cmd.Parameters.AddWithValue("@date", eventDate);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@outcomes", participants.ToArray());
                cmd.Parameters.AddWithValue("@coeffs", coeffs);

                var newIdObj = cmd.ExecuteScalar();

                if (newIdObj is int newId && newId > 0)
                {
                    MessageBox.Show(
                        $"Добавлена новая гонка!\n" +
                        $"Тип: {sport}\n" +
                        $"Название: {name}\n" +
                        $"Дата: {eventDate:dd.MM.yyyy HH:mm}\n" +
                        $"Место: {location}\n" +
                        $"ID события: {newId}",
                        "Успех",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadEvents();
                }
                else
                {
                    MessageBox.Show("Создание вернуло неожиданный результат", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления события:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Выйти из аккаунта?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();

                var loginForm = new LoginForm();
                loginForm.Show();

                loginForm.FormClosed += (s, args) =>
                {
                    if (loginForm.DialogResult != DialogResult.OK)
                        Application.Exit();
                };
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            debounceTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
