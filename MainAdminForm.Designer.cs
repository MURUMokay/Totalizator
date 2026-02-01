namespace Totalizator
{
    partial class MainAdminForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlUserHeader = new Panel();
            tlpHeader = new TableLayoutPanel();
            lblUserName = new Label();
            lblEmail = new Label();
            lblBalance = new Label();
            flpActionButtons = new FlowLayoutPanel();
            btnEvents = new Button();
            btnCompleteEvent = new Button();
            btnAddTestEvent = new Button();
            btnAddEventManual = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnLogout = new Button();
            btnAddBalance = new Button();
            pnlContent = new Panel();
            dgvEvents = new DataGridView();
            txtSearch = new TextBox();
            pnlUserHeader.SuspendLayout();
            tlpHeader.SuspendLayout();
            flpActionButtons.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();
            // 
            // pnlUserHeader
            // 
            pnlUserHeader.BackColor = Color.FromArgb(240, 244, 248);
            pnlUserHeader.Controls.Add(tlpHeader);
            pnlUserHeader.Controls.Add(flpActionButtons);
            pnlUserHeader.Controls.Add(flowLayoutPanel1);
            pnlUserHeader.Dock = DockStyle.Top;
            pnlUserHeader.Location = new Point(0, 0);
            pnlUserHeader.Name = "pnlUserHeader";
            pnlUserHeader.Size = new Size(1105, 120);
            pnlUserHeader.TabIndex = 1;
            // 
            // tlpHeader
            // 
            tlpHeader.ColumnCount = 3;
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpHeader.Controls.Add(lblUserName, 0, 0);
            tlpHeader.Controls.Add(lblEmail, 1, 0);
            tlpHeader.Controls.Add(lblBalance, 2, 0);
            tlpHeader.Dock = DockStyle.Fill;
            tlpHeader.Location = new Point(0, 0);
            tlpHeader.Name = "tlpHeader";
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpHeader.Size = new Size(979, 76);
            tlpHeader.TabIndex = 0;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUserName.Location = new Point(3, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(168, 28);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "Администратор";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(394, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(175, 23);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "admin@example.com";
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Font = new Font("Segoe UI", 10F);
            lblBalance.Location = new Point(785, 0);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(27, 23);
            lblBalance.TabIndex = 2;
            lblBalance.Text = "—";
            lblBalance.Visible = false;
            // 
            // flpActionButtons
            // 
            flpActionButtons.AutoSize = true;
            flpActionButtons.Controls.Add(btnEvents);
            flpActionButtons.Controls.Add(btnCompleteEvent);
            flpActionButtons.Controls.Add(btnAddTestEvent);
            flpActionButtons.Controls.Add(btnAddEventManual);
            flpActionButtons.Dock = DockStyle.Bottom;
            flpActionButtons.Location = new Point(0, 76);
            flpActionButtons.Name = "flpActionButtons";
            flpActionButtons.Size = new Size(979, 44);
            flpActionButtons.TabIndex = 1;
            // 
            // btnEvents
            // 
            btnEvents.BackColor = Color.White;
            btnEvents.FlatStyle = FlatStyle.Flat;
            btnEvents.Font = new Font("Segoe UI", 10F);
            btnEvents.Location = new Point(3, 3);
            btnEvents.Name = "btnEvents";
            btnEvents.Size = new Size(140, 38);
            btnEvents.TabIndex = 0;
            btnEvents.Text = "События";
            btnEvents.UseVisualStyleBackColor = false;
            // 
            // btnCompleteEvent
            // 
            btnCompleteEvent.BackColor = Color.FromArgb(0, 122, 204);
            btnCompleteEvent.FlatStyle = FlatStyle.Flat;
            btnCompleteEvent.Font = new Font("Segoe UI", 10F);
            btnCompleteEvent.ForeColor = Color.White;
            btnCompleteEvent.Location = new Point(149, 3);
            btnCompleteEvent.Name = "btnCompleteEvent";
            btnCompleteEvent.Size = new Size(180, 38);
            btnCompleteEvent.TabIndex = 1;
            btnCompleteEvent.Text = "Завершить событие";
            btnCompleteEvent.UseVisualStyleBackColor = false;
            // 
            // btnAddTestEvent
            // 
            btnAddTestEvent.BackColor = Color.FromArgb(0, 200, 81);
            btnAddTestEvent.FlatStyle = FlatStyle.Flat;
            btnAddTestEvent.Font = new Font("Segoe UI", 10F);
            btnAddTestEvent.ForeColor = Color.White;
            btnAddTestEvent.Location = new Point(335, 3);
            btnAddTestEvent.Name = "btnAddTestEvent";
            btnAddTestEvent.Size = new Size(220, 38);
            btnAddTestEvent.TabIndex = 2;
            btnAddTestEvent.Text = "Добавить тестовое событие";
            btnAddTestEvent.UseVisualStyleBackColor = false;
            // 
            // btnAddEventManual
            // 
            btnAddEventManual.BackColor = Color.FromArgb(255, 193, 7);
            btnAddEventManual.FlatAppearance.BorderSize = 0;
            btnAddEventManual.FlatStyle = FlatStyle.Flat;
            btnAddEventManual.Font = new Font("Segoe UI", 10F);
            btnAddEventManual.ForeColor = Color.White;
            btnAddEventManual.Location = new Point(558, 0);
            btnAddEventManual.Margin = new Padding(0, 0, 12, 0);
            btnAddEventManual.Name = "btnAddEventManual";
            btnAddEventManual.Size = new Size(254, 38);
            btnAddEventManual.TabIndex = 3;
            btnAddEventManual.Text = "Создать событие вручную";
            btnAddEventManual.UseVisualStyleBackColor = false;
            btnAddEventManual.Click += BtnAddEventManual_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(btnLogout);
            flowLayoutPanel1.Dock = DockStyle.Right;
            flowLayoutPanel1.Location = new Point(979, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(126, 120);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(220, 53, 69);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(3, 3);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(120, 38);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Выйти";
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // btnAddBalance
            // 
            btnAddBalance.Location = new Point(0, 0);
            btnAddBalance.Name = "btnAddBalance";
            btnAddBalance.Size = new Size(75, 23);
            btnAddBalance.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(dgvEvents);
            pnlContent.Controls.Add(txtSearch);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 120);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1105, 692);
            pnlContent.TabIndex = 0;
            // 
            // dgvEvents
            // 
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.BackgroundColor = Color.White;
            dgvEvents.ColumnHeadersHeight = 29;
            dgvEvents.Dock = DockStyle.Fill;
            dgvEvents.Location = new Point(0, 30);
            dgvEvents.Name = "dgvEvents";
            dgvEvents.ReadOnly = true;
            dgvEvents.RowHeadersVisible = false;
            dgvEvents.RowHeadersWidth = 51;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.Size = new Size(1105, 662);
            dgvEvents.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Top;
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(0, 0);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(1105, 30);
            txtSearch.TabIndex = 1;
            // 
            // MainAdminForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1105, 812);
            Controls.Add(pnlContent);
            Controls.Add(pnlUserHeader);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainAdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Тотализатор — Администрирование";
            pnlUserHeader.ResumeLayout(false);
            pnlUserHeader.PerformLayout();
            tlpHeader.ResumeLayout(false);
            tlpHeader.PerformLayout();
            flpActionButtons.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlUserHeader;
        private TableLayoutPanel tlpHeader;
        private Label lblUserName;
        private Label lblEmail;
        private Label lblBalance;
        private Button btnAddBalance;
        private FlowLayoutPanel flpActionButtons;
        private Button btnEvents;
        private Button btnLogout;
        private Panel pnlContent;
        private TextBox txtSearch;
        private Button btnCompleteEvent;
        private Button btnAddTestEvent;
        private DataGridView dgvEvents;
        private FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAddEventManual;
    }
}