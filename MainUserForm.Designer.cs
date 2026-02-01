namespace Totalizator

{

    partial class MainUserForm

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
            btnAddBalance = new Button();
            flpActionButtons = new FlowLayoutPanel();
            btnEvents = new Button();
            btnMyBets = new Button();
            btnMyPayouts = new Button();
            pnlContent = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnLogout = new Button();
            pnlUserHeader.SuspendLayout();
            tlpHeader.SuspendLayout();
            flpActionButtons.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
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
            pnlUserHeader.Margin = new Padding(3, 4, 3, 4);
            pnlUserHeader.Name = "pnlUserHeader";
            pnlUserHeader.Padding = new Padding(12, 10, 12, 10);
            pnlUserHeader.Size = new Size(1105, 134);
            pnlUserHeader.TabIndex = 0;
            // 
            // tlpHeader
            // 
            tlpHeader.AutoSize = true;
            tlpHeader.ColumnCount = 3;
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.2708359F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.625F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpHeader.Controls.Add(lblUserName, 0, 0);
            tlpHeader.Controls.Add(lblEmail, 0, 1);
            tlpHeader.Controls.Add(lblBalance, 2, 0);
            tlpHeader.Controls.Add(btnAddBalance, 1, 0);
            tlpHeader.Dock = DockStyle.Top;
            tlpHeader.Location = new Point(12, 10);
            tlpHeader.Margin = new Padding(3, 4, 3, 4);
            tlpHeader.Name = "tlpHeader";
            tlpHeader.RowCount = 2;
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tlpHeader.Size = new Size(1081, 60);
            tlpHeader.TabIndex = 0;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Dock = DockStyle.Fill;
            lblUserName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblUserName.Location = new Point(3, 3);
            lblUserName.Margin = new Padding(3, 3, 3, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(797, 36);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "Иванов Иван Иванович";
            lblUserName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Dock = DockStyle.Fill;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(3, 39);
            lblEmail.Margin = new Padding(3, 0, 3, 3);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(797, 18);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "user@example.com";
            lblEmail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Dock = DockStyle.Fill;
            lblBalance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblBalance.Location = new Point(866, 3);
            lblBalance.Margin = new Padding(3, 3, 3, 0);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(212, 36);
            lblBalance.TabIndex = 2;
            lblBalance.Text = "1 250,00 ₽";
            lblBalance.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnAddBalance
            // 
            btnAddBalance.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddBalance.FlatStyle = FlatStyle.Flat;
            btnAddBalance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddBalance.Location = new Point(823, 4);
            btnAddBalance.Margin = new Padding(4);
            btnAddBalance.Name = "btnAddBalance";
            btnAddBalance.Size = new Size(36, 31);
            btnAddBalance.TabIndex = 3;
            btnAddBalance.Text = "+";
            btnAddBalance.UseVisualStyleBackColor = true;
            btnAddBalance.Click += btnAddBalance_Click;
            // 
            // flpActionButtons
            // 
            flpActionButtons.AutoSize = true;
            flpActionButtons.Controls.Add(btnEvents);
            flpActionButtons.Controls.Add(btnMyBets);
            flpActionButtons.Controls.Add(btnMyPayouts);
            flpActionButtons.Location = new Point(8, 74);
            flpActionButtons.Margin = new Padding(3, 4, 3, 4);
            flpActionButtons.Name = "flpActionButtons";
            flpActionButtons.Padding = new Padding(0, 8, 0, 0);
            flpActionButtons.Size = new Size(870, 60);
            flpActionButtons.TabIndex = 1;
            // 
            // btnEvents
            // 
            btnEvents.Location = new Point(3, 12);
            btnEvents.Margin = new Padding(3, 4, 12, 4);
            btnEvents.Name = "btnEvents";
            btnEvents.Size = new Size(140, 32);
            btnEvents.TabIndex = 0;
            btnEvents.Text = "События";
            btnEvents.UseVisualStyleBackColor = true;
            btnEvents.Click += btnEvents_Click;
            // 
            // btnMyBets
            // 
            btnMyBets.Location = new Point(155, 12);
            btnMyBets.Margin = new Padding(0, 4, 12, 4);
            btnMyBets.Name = "btnMyBets";
            btnMyBets.Size = new Size(140, 32);
            btnMyBets.TabIndex = 1;
            btnMyBets.Text = "Мои ставки";
            btnMyBets.UseVisualStyleBackColor = true;
            btnMyBets.Click += btnMyBets_Click;
            // 
            // btnMyPayouts
            // 
            btnMyPayouts.Location = new Point(307, 12);
            btnMyPayouts.Margin = new Padding(0, 4, 12, 4);
            btnMyPayouts.Name = "btnMyPayouts";
            btnMyPayouts.Size = new Size(140, 32);
            btnMyPayouts.TabIndex = 2;
            btnMyPayouts.Text = "Мои выплаты";
            btnMyPayouts.UseVisualStyleBackColor = true;
            btnMyPayouts.Click += btnMyPayouts_Click;
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 134);
            pnlContent.Margin = new Padding(3, 4, 3, 4);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1105, 678);
            pnlContent.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(btnLogout);
            flowLayoutPanel1.Location = new Point(953, 78);
            flowLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(0, 8, 0, 0);
            flowLayoutPanel1.Size = new Size(140, 48);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Location = new Point(3, 12);
            btnLogout.Margin = new Padding(3, 4, 12, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(125, 32);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "Выйти";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MainUserForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1105, 812);
            Controls.Add(pnlContent);
            Controls.Add(pnlUserHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainUserForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Тотализатор — Личный кабинет";
            pnlUserHeader.ResumeLayout(false);
            pnlUserHeader.PerformLayout();
            tlpHeader.ResumeLayout(false);
            tlpHeader.PerformLayout();
            flpActionButtons.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);

        }



        #endregion



        private System.Windows.Forms.Panel pnlUserHeader;

        private System.Windows.Forms.TableLayoutPanel tlpHeader;

        private System.Windows.Forms.Label lblUserName;

        private System.Windows.Forms.Label lblEmail;

        private System.Windows.Forms.Label lblBalance;

        private System.Windows.Forms.FlowLayoutPanel flpActionButtons;

        private System.Windows.Forms.Button btnEvents;

        private System.Windows.Forms.Button btnMyBets;

        private System.Windows.Forms.Button btnMyPayouts;

        private System.Windows.Forms.Button btnAddBalance;

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnLogout;
    }

}