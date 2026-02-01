namespace Totalizator
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel table;

        private System.Windows.Forms.Label labelTitle;

        private System.Windows.Forms.GroupBox groupMode;
        private System.Windows.Forms.FlowLayoutPanel flowMode;
        private System.Windows.Forms.RadioButton radioModeLogin;
        private System.Windows.Forms.RadioButton radioModeRegister;

        private System.Windows.Forms.GroupBox groupRole;
        private System.Windows.Forms.FlowLayoutPanel flowRole;
        private System.Windows.Forms.RadioButton radioUser;
        private System.Windows.Forms.RadioButton radioAdmin;

        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox textBoxLastName;

        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.TextBox textBoxFirstName;

        private System.Windows.Forms.Label labelMiddleName;
        private System.Windows.Forms.TextBox textBoxMiddleName;

        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxEmail;

        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;

        private System.Windows.Forms.FlowLayoutPanel panelBottom;
        private System.Windows.Forms.Button buttonAction;
        private System.Windows.Forms.LinkLabel linkClear;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.table = new System.Windows.Forms.TableLayoutPanel();

            this.labelTitle = new System.Windows.Forms.Label();


            this.groupMode = new System.Windows.Forms.GroupBox();
            this.flowMode = new System.Windows.Forms.FlowLayoutPanel();
            this.radioModeLogin = new System.Windows.Forms.RadioButton();
            this.radioModeRegister = new System.Windows.Forms.RadioButton();

            this.groupRole = new System.Windows.Forms.GroupBox();
            this.flowRole = new System.Windows.Forms.FlowLayoutPanel();
            this.radioUser = new System.Windows.Forms.RadioButton();
            this.radioAdmin = new System.Windows.Forms.RadioButton();

            this.labelLastName = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();

            this.labelFirstName = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();

            this.labelMiddleName = new System.Windows.Forms.Label();
            this.textBoxMiddleName = new System.Windows.Forms.TextBox();

            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();

            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();

            this.panelBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAction = new System.Windows.Forms.Button();
            this.linkClear = new System.Windows.Forms.LinkLabel();

            this.SuspendLayout();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(600, 650);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 650);
            this.MaximumSize = new System.Drawing.Size(600, 650);

            // table
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Padding = new System.Windows.Forms.Padding(24);
            this.table.AutoSize = false;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Clear();
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.labelTitle.Text = "Вход";

            // groupMode
            this.groupMode.Text = "Действие";
            this.groupMode.AutoSize = true;
            this.groupMode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupMode.Padding = new System.Windows.Forms.Padding(10);
            this.groupMode.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);

            // flowMode
            this.flowMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMode.AutoSize = true;
            this.flowMode.WrapContents = false;
            this.flowMode.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowMode.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);

            // radioModeLogin
            this.radioModeLogin.AutoSize = true;
            this.radioModeLogin.Text = "Вход";
            this.radioModeLogin.Checked = true;
            this.radioModeLogin.Margin = new System.Windows.Forms.Padding(4, 4, 20, 4);
            this.radioModeLogin.CheckedChanged += new System.EventHandler(this.radioModeLogin_CheckedChanged);

            // radioModeRegister
            this.radioModeRegister.AutoSize = true;
            this.radioModeRegister.Text = "Регистрация";
            this.radioModeRegister.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioModeRegister.CheckedChanged += new System.EventHandler(this.radioModeRegister_CheckedChanged);

            this.flowMode.Controls.Add(this.radioModeLogin);
            this.flowMode.Controls.Add(this.radioModeRegister);
            this.groupMode.Controls.Add(this.flowMode);

            // groupRole
            this.groupRole.Text = "Вход как";
            this.groupRole.AutoSize = true;
            this.groupRole.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupRole.Padding = new System.Windows.Forms.Padding(10);
            this.groupRole.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);

            // flowRole
            this.flowRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowRole.AutoSize = true;
            this.flowRole.WrapContents = false;
            this.flowRole.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowRole.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);

            // radioUser
            this.radioUser.AutoSize = true;
            this.radioUser.Text = "Пользователь";
            this.radioUser.Checked = true;
            this.radioUser.Margin = new System.Windows.Forms.Padding(4, 4, 20, 4);

            // radioAdmin
            this.radioAdmin.AutoSize = true;
            this.radioAdmin.Text = "Администратор";
            this.radioAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);

            this.flowRole.Controls.Add(this.radioUser);
            this.flowRole.Controls.Add(this.radioAdmin);
            this.groupRole.Controls.Add(this.flowRole);

            // helpers
            void ConfigureLabel(System.Windows.Forms.Label lbl, string text)
            {
                lbl.AutoSize = true;
                lbl.Text = text;
                lbl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            }
            void ConfigureTextBox(System.Windows.Forms.TextBox tb)
            {
                tb.Dock = System.Windows.Forms.DockStyle.Top;
                tb.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);

            }

            ConfigureLabel(this.labelLastName, "Фамилия *");
            ConfigureTextBox(this.textBoxLastName);

            ConfigureLabel(this.labelFirstName, "Имя *");
            ConfigureTextBox(this.textBoxFirstName);

            ConfigureLabel(this.labelMiddleName, "Отчество");
            ConfigureTextBox(this.textBoxMiddleName);

            ConfigureLabel(this.labelEmail, "Электронная почта *");
            ConfigureTextBox(this.textBoxEmail);

            ConfigureLabel(this.labelPassword, "Пароль *");
            ConfigureTextBox(this.textBoxPassword);
            this.textBoxPassword.UseSystemPasswordChar = true;

            // panelBottom
            this.panelBottom.AutoSize = true;
            this.panelBottom.WrapContents = false;
            this.panelBottom.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelBottom.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panelBottom.Padding = new System.Windows.Forms.Padding(0);

            // buttonAction
            this.buttonAction.Text = "Войти";
            this.buttonAction.Width = 360;
            this.buttonAction.Height = 32;
            this.buttonAction.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.buttonAction.Click += new System.EventHandler(this.buttonAction_Click);

            // linkClear
            this.linkClear.AutoSize = true;
            this.linkClear.Text = "Очистить поля";
            this.linkClear.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.linkClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClear_Click);

            this.panelBottom.Controls.Add(this.buttonAction);
            this.panelBottom.Controls.Add(this.linkClear);

            // Add to table
            this.table.Controls.Add(this.labelTitle);
            this.table.Controls.Add(this.groupMode);
            this.table.Controls.Add(this.groupRole);

            this.table.Controls.Add(this.labelLastName);
            this.table.Controls.Add(this.textBoxLastName);

            this.table.Controls.Add(this.labelFirstName);
            this.table.Controls.Add(this.textBoxFirstName);

            this.table.Controls.Add(this.labelMiddleName);
            this.table.Controls.Add(this.textBoxMiddleName);

            this.table.Controls.Add(this.labelEmail);
            this.table.Controls.Add(this.textBoxEmail);

            this.table.Controls.Add(this.labelPassword);
            this.table.Controls.Add(this.textBoxPassword);

            this.table.Controls.Add(this.panelBottom);

            this.Controls.Add(this.table);

            this.ResumeLayout(false);
        }
    }
}
