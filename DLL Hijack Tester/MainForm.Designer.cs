namespace DllHijackTesterApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSaveReport;
        private System.Windows.Forms.ListBox lstResults;

        private void InitializeComponent()
        {
            this.txtExePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSaveReport = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtExePath
            // 
            this.txtExePath.BackColor = System.Drawing.Color.FromArgb(11, 32, 11);
            this.txtExePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExePath.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            this.txtExePath.ForeColor = System.Drawing.Color.Lime;
            this.txtExePath.Location = new System.Drawing.Point(12, 12);
            this.txtExePath.Name = "txtExePath";
            this.txtExePath.Size = new System.Drawing.Size(360, 23);
            this.txtExePath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowse.ForeColor = System.Drawing.Color.Lime;
            this.btnBrowse.Location = new System.Drawing.Point(378, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.btnBrowse.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnTest
            // 
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.FlatAppearance.BorderSize = 0;
            this.btnTest.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.btnTest.ForeColor = System.Drawing.Color.Lime;
            this.btnTest.Location = new System.Drawing.Point(12, 41);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(441, 30);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "Run DLL Hijack Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.btnTest.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSaveReport
            // 
            this.btnSaveReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveReport.FlatAppearance.BorderSize = 0;
            this.btnSaveReport.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveReport.ForeColor = System.Drawing.Color.Lime;
            this.btnSaveReport.Location = new System.Drawing.Point(12, 295);
            this.btnSaveReport.Name = "btnSaveReport";
            this.btnSaveReport.Size = new System.Drawing.Size(441, 30);
            this.btnSaveReport.TabIndex = 4;
            this.btnSaveReport.Text = "Save Report";
            this.btnSaveReport.UseVisualStyleBackColor = true;
            this.btnSaveReport.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.btnSaveReport.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            this.btnSaveReport.Click += new System.EventHandler(this.btnSaveReport_Click);
            // 
            // lstResults
            // 
            this.lstResults.BackColor = System.Drawing.Color.FromArgb(11, 32, 11);
            this.lstResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular);
            this.lstResults.ForeColor = System.Drawing.Color.Lime;
            this.lstResults.FormattingEnabled = true;
            this.lstResults.ItemHeight = 14;
            this.lstResults.Location = new System.Drawing.Point(12, 77);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(441, 214);
            this.lstResults.TabIndex = 3;
            this.lstResults.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstResults.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstResults_DrawItem);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(11, 32, 11);
            this.ClientSize = new System.Drawing.Size(465, 337);
            this.Controls.Add(this.btnSaveReport);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtExePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DLL Hijack Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
