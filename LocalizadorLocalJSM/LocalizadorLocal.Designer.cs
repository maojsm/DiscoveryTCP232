namespace LocalizadorLocalJSM
{
    partial class LocalizadorLocal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnDiscoverServers = new Button();
            txtLog = new TextBox();
            btnLimparLog = new Button();
            dataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnDiscoverServers
            // 
            btnDiscoverServers.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDiscoverServers.Location = new Point(523, 329);
            btnDiscoverServers.Name = "btnDiscoverServers";
            btnDiscoverServers.Size = new Size(118, 42);
            btnDiscoverServers.TabIndex = 0;
            btnDiscoverServers.Text = "Localizar";
            btnDiscoverServers.UseVisualStyleBackColor = true;
            btnDiscoverServers.Click += btnDiscoverServers_Click;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLog.Location = new Point(12, 209);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(503, 162);
            txtLog.TabIndex = 1;
            // 
            // btnLimparLog
            // 
            btnLimparLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLimparLog.Location = new Point(521, 281);
            btnLimparLog.Name = "btnLimparLog";
            btnLimparLog.Size = new Size(118, 42);
            btnLimparLog.TabIndex = 2;
            btnLimparLog.Text = "Limpar Log";
            btnLimparLog.UseVisualStyleBackColor = true;
            btnLimparLog.Click += btnLimparLog_Click;
            // 
            // dataGridView
            // 
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 12);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(503, 191);
            dataGridView.TabIndex = 3;
            // 
            // LocalizadorLocal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(653, 383);
            Controls.Add(dataGridView);
            Controls.Add(btnLimparLog);
            Controls.Add(txtLog);
            Controls.Add(btnDiscoverServers);
            Name = "LocalizadorLocal";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDiscoverServers;
        private TextBox txtLog;
        private Button btnLimparLog;
        private DataGridView dataGridView;
    }
}
