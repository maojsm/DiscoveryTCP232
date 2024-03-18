namespace LocalizadorLocalJSM
{
    partial class FrmLocalizadorTcp232
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLocalizadorTcp232));
            btnDiscoverServers = new Button();
            txtLog = new TextBox();
            btnLimparLog = new Button();
            dataGridView = new DataGridView();
            deviceNameColumn = new DataGridViewTextBoxColumn();
            ipColumn = new DataGridViewTextBoxColumn();
            macAddress = new DataGridViewTextBoxColumn();
            pictureBox1 = new PictureBox();
            lblInfo = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnDiscoverServers
            // 
            btnDiscoverServers.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDiscoverServers.Location = new Point(12, 145);
            btnDiscoverServers.Name = "btnDiscoverServers";
            btnDiscoverServers.Size = new Size(147, 42);
            btnDiscoverServers.TabIndex = 0;
            btnDiscoverServers.Text = "Buscar Modulos";
            btnDiscoverServers.UseVisualStyleBackColor = true;
            btnDiscoverServers.Click += btnDiscoverServers_Click;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLog.Location = new Point(12, 193);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(629, 133);
            txtLog.TabIndex = 1;
            // 
            // btnLimparLog
            // 
            btnLimparLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLimparLog.Location = new Point(165, 145);
            btnLimparLog.Name = "btnLimparLog";
            btnLimparLog.Size = new Size(145, 42);
            btnLimparLog.TabIndex = 2;
            btnLimparLog.Text = "Limpar Log";
            btnLimparLog.UseVisualStyleBackColor = true;
            btnLimparLog.Click += btnLimparLog_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { deviceNameColumn, ipColumn, macAddress });
            dataGridView.Location = new Point(12, 12);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(476, 127);
            dataGridView.TabIndex = 3;
            // 
            // deviceNameColumn
            // 
            deviceNameColumn.FillWeight = 23.37589F;
            deviceNameColumn.HeaderText = "Device Name";
            deviceNameColumn.MaxInputLength = 20;
            deviceNameColumn.Name = "deviceNameColumn";
            deviceNameColumn.ReadOnly = true;
            deviceNameColumn.Width = 150;
            // 
            // ipColumn
            // 
            ipColumn.FillWeight = 48.19772F;
            ipColumn.HeaderText = "IP";
            ipColumn.MaxInputLength = 20;
            ipColumn.Name = "ipColumn";
            ipColumn.ReadOnly = true;
            // 
            // macAddress
            // 
            macAddress.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            macAddress.FillWeight = 228.426392F;
            macAddress.HeaderText = "MAC";
            macAddress.MaxInputLength = 25;
            macAddress.Name = "macAddress";
            macAddress.ReadOnly = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.Tcp232;
            pictureBox1.Location = new Point(494, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(147, 126);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.DoubleClick += pictureBox1_DoubleClick;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(347, 142);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(141, 15);
            lblInfo.TabIndex = 5;
            lblInfo.Text = "by Márcio da Silva @2024";
            // 
            // FrmLocalizadorTcp232
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(653, 338);
            Controls.Add(lblInfo);
            Controls.Add(pictureBox1);
            Controls.Add(dataGridView);
            Controls.Add(btnLimparLog);
            Controls.Add(txtLog);
            Controls.Add(btnDiscoverServers);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(966, 590);
            MinimumSize = new Size(669, 377);
            Name = "FrmLocalizadorTcp232";
            Text = "Localizardor para Modulo TCP232-E2 da USR";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDiscoverServers;
        private TextBox txtLog;
        private Button btnLimparLog;
        private DataGridView dataGridView;
        private PictureBox pictureBox1;
        private DataGridViewTextBoxColumn deviceNameColumn;
        private DataGridViewTextBoxColumn ipColumn;
        private DataGridViewTextBoxColumn macAddress;
        private Label lblInfo;
    }
}
