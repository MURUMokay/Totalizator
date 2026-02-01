namespace Totalizator
{
    partial class EventDetailsForm
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

        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.lblSport = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dgvOutcomes = new System.Windows.Forms.DataGridView();
            this.lblEventInfo = new System.Windows.Forms.Label();
            this.lblOutcomes = new System.Windows.Forms.Label();
            this.pnlBet = new System.Windows.Forms.Panel();
            this.btnPlaceBet = new System.Windows.Forms.Button();
            this.lblBetInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutcomes)).BeginInit();
            this.pnlBet.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Location = new System.Drawing.Point(12, 40);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 19);
            this.lblName.TabIndex = 0;
            // 
            // lblSport
            // 
            this.lblSport.AutoSize = true;
            this.lblSport.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSport.Location = new System.Drawing.Point(12, 65);
            this.lblSport.Name = "lblSport";
            this.lblSport.Size = new System.Drawing.Size(0, 19);
            this.lblSport.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.Location = new System.Drawing.Point(12, 90);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 19);
            this.lblDate.TabIndex = 2;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLocation.Location = new System.Drawing.Point(12, 115);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(0, 19);
            this.lblLocation.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(12, 140);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 19);
            this.lblStatus.TabIndex = 4;
            // 
            // dgvOutcomes
            // 
            this.dgvOutcomes.AllowUserToAddRows = false;
            this.dgvOutcomes.AllowUserToDeleteRows = false;
            this.dgvOutcomes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOutcomes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.dgvOutcomes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOutcomes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutcomes.Location = new System.Drawing.Point(12, 200);
            this.dgvOutcomes.Name = "dgvOutcomes";
            this.dgvOutcomes.ReadOnly = true;
            this.dgvOutcomes.RowHeadersVisible = false;
            this.dgvOutcomes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutcomes.Size = new System.Drawing.Size(776, 350);
            this.dgvOutcomes.TabIndex = 5;
            this.dgvOutcomes.Columns.Add("outcomeId", "ID");
            this.dgvOutcomes.Columns.Add("participant", "Участник");
            this.dgvOutcomes.Columns.Add("place", "Место");
            this.dgvOutcomes.Columns.Add("coef", "Коэффициент");
            this.dgvOutcomes.Columns.Add("result", "Результат");
            // 
            // lblEventInfo
            // 
            this.lblEventInfo.AutoSize = true;
            this.lblEventInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblEventInfo.Location = new System.Drawing.Point(12, 12);
            this.lblEventInfo.Name = "lblEventInfo";
            this.lblEventInfo.Size = new System.Drawing.Size(140, 21);
            this.lblEventInfo.TabIndex = 6;
            this.lblEventInfo.Text = "Информация о событии";
            // 
            // lblOutcomes
            // 
            this.lblOutcomes.AutoSize = true;
            this.lblOutcomes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblOutcomes.Location = new System.Drawing.Point(12, 165);
            this.lblOutcomes.Name = "lblOutcomes";
            this.lblOutcomes.Size = new System.Drawing.Size(100, 21);
            this.lblOutcomes.TabIndex = 7;
            this.lblOutcomes.Text = "Исходы";
            // 
            // pnlBet
            // 
            this.pnlBet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pnlBet.Controls.Add(this.btnPlaceBet);
            this.pnlBet.Controls.Add(this.lblBetInfo);
            this.pnlBet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBet.Location = new System.Drawing.Point(0, 550);
            this.pnlBet.Name = "pnlBet";
            this.pnlBet.Size = new System.Drawing.Size(800, 100);
            this.pnlBet.TabIndex = 8;
            // 
            // btnPlaceBet
            // 
            this.btnPlaceBet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnPlaceBet.FlatAppearance.BorderSize = 0;
            this.btnPlaceBet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlaceBet.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnPlaceBet.ForeColor = System.Drawing.Color.White;
            this.btnPlaceBet.Location = new System.Drawing.Point(20, 40);
            this.btnPlaceBet.Name = "btnPlaceBet";
            this.btnPlaceBet.Size = new System.Drawing.Size(200, 40);
            this.btnPlaceBet.TabIndex = 1;
            this.btnPlaceBet.Text = "Поставить ставку";
            this.btnPlaceBet.UseVisualStyleBackColor = false;
            this.btnPlaceBet.Click += new System.EventHandler(this.btnPlaceBet_Click);
            // 
            // lblBetInfo
            // 
            this.lblBetInfo.AutoSize = true;
            this.lblBetInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBetInfo.Location = new System.Drawing.Point(20, 10);
            this.lblBetInfo.Name = "lblBetInfo";
            this.lblBetInfo.Size = new System.Drawing.Size(0, 19);
            this.lblBetInfo.TabIndex = 0;
            this.lblBetInfo.Text = "Выберите исход и нажмите «Поставить ставку»";
            // 
            // EventDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pnlBet);
            this.Controls.Add(this.lblOutcomes);
            this.Controls.Add(this.lblEventInfo);
            this.Controls.Add(this.dgvOutcomes);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblSport);
            this.Controls.Add(this.lblName);
            this.Name = "EventDetailsForm";
            this.Text = "Детали события";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutcomes)).EndInit();
            this.pnlBet.ResumeLayout(false);
            this.pnlBet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSport;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dgvOutcomes;
        private System.Windows.Forms.Label lblEventInfo;
        private System.Windows.Forms.Label lblOutcomes;
        private System.Windows.Forms.Panel pnlBet;
        private System.Windows.Forms.Button btnPlaceBet;
        private System.Windows.Forms.Label lblBetInfo;
    }
}