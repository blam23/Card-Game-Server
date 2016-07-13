namespace CardGameTestUI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbRecieved = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lbl_static_status = new System.Windows.Forms.Label();
            this.lbSent = new System.Windows.Forms.ListBox();
            this.btnMain = new System.Windows.Forms.Button();
            this.tmrPing = new System.Windows.Forms.Timer(this.components);
            this.cmbIP = new System.Windows.Forms.ComboBox();
            this.lvCards = new System.Windows.Forms.ListView();
            this.lvCreatures = new System.Windows.Forms.ListView();
            this.lvOpponent = new System.Windows.Forms.ListView();
            this.btnAttack = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbl_static_name = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblHealth = new System.Windows.Forms.Label();
            this.lblCards = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbRecieved
            // 
            this.lbRecieved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRecieved.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecieved.FormattingEnabled = true;
            this.lbRecieved.Items.AddRange(new object[] {
            "Recieved"});
            this.lbRecieved.Location = new System.Drawing.Point(12, 515);
            this.lbRecieved.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbRecieved.Name = "lbRecieved";
            this.lbRecieved.Size = new System.Drawing.Size(897, 69);
            this.lbRecieved.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblStatus.Location = new System.Drawing.Point(1219, 12);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(142, 25);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Not Connected";
            // 
            // lbl_static_status
            // 
            this.lbl_static_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_static_status.AutoSize = true;
            this.lbl_static_status.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_static_status.ForeColor = System.Drawing.Color.White;
            this.lbl_static_status.Location = new System.Drawing.Point(1159, 12);
            this.lbl_static_status.Name = "lbl_static_status";
            this.lbl_static_status.Size = new System.Drawing.Size(66, 25);
            this.lbl_static_status.TabIndex = 2;
            this.lbl_static_status.Text = "Status:";
            // 
            // lbSent
            // 
            this.lbSent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSent.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSent.FormattingEnabled = true;
            this.lbSent.Items.AddRange(new object[] {
            "Sent:"});
            this.lbSent.Location = new System.Drawing.Point(12, 592);
            this.lbSent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbSent.Name = "lbSent";
            this.lbSent.Size = new System.Drawing.Size(897, 69);
            this.lbSent.TabIndex = 3;
            // 
            // btnMain
            // 
            this.btnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMain.Location = new System.Drawing.Point(1060, 131);
            this.btnMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(301, 69);
            this.btnMain.TabIndex = 4;
            this.btnMain.Text = "Connect";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // tmrPing
            // 
            this.tmrPing.Interval = 7000;
            this.tmrPing.Tick += new System.EventHandler(this.tmrPing_Tick);
            // 
            // cmbIP
            // 
            this.cmbIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIP.FormattingEnabled = true;
            this.cmbIP.Items.AddRange(new object[] {
            "localhost",
            "139.59.168.252"});
            this.cmbIP.Location = new System.Drawing.Point(1128, 99);
            this.cmbIP.Name = "cmbIP";
            this.cmbIP.Size = new System.Drawing.Size(233, 25);
            this.cmbIP.TabIndex = 7;
            this.cmbIP.Text = "localhost";
            // 
            // lvCards
            // 
            this.lvCards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvCards.Location = new System.Drawing.Point(12, 12);
            this.lvCards.Name = "lvCards";
            this.lvCards.Size = new System.Drawing.Size(273, 492);
            this.lvCards.TabIndex = 8;
            this.lvCards.UseCompatibleStateImageBehavior = false;
            // 
            // lvCreatures
            // 
            this.lvCreatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCreatures.Location = new System.Drawing.Point(291, 12);
            this.lvCreatures.Name = "lvCreatures";
            this.lvCreatures.Size = new System.Drawing.Size(285, 492);
            this.lvCreatures.TabIndex = 9;
            this.lvCreatures.UseCompatibleStateImageBehavior = false;
            // 
            // lvOpponent
            // 
            this.lvOpponent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvOpponent.Location = new System.Drawing.Point(603, 12);
            this.lvOpponent.Name = "lvOpponent";
            this.lvOpponent.Size = new System.Drawing.Size(306, 492);
            this.lvOpponent.TabIndex = 10;
            this.lvOpponent.UseCompatibleStateImageBehavior = false;
            // 
            // btnAttack
            // 
            this.btnAttack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAttack.Location = new System.Drawing.Point(553, 26);
            this.btnAttack.Name = "btnAttack";
            this.btnAttack.Size = new System.Drawing.Size(75, 464);
            this.btnAttack.TabIndex = 11;
            this.btnAttack.Text = "Attack =>";
            this.btnAttack.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(1127, 68);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(233, 25);
            this.txtName.TabIndex = 5;
            this.txtName.Text = "Blam";
            // 
            // lbl_static_name
            // 
            this.lbl_static_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_static_name.AutoSize = true;
            this.lbl_static_name.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_static_name.ForeColor = System.Drawing.Color.White;
            this.lbl_static_name.Location = new System.Drawing.Point(1055, 68);
            this.lbl_static_name.Name = "lbl_static_name";
            this.lbl_static_name.Size = new System.Drawing.Size(66, 25);
            this.lbl_static_name.TabIndex = 6;
            this.lbl_static_name.Text = "Name:";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatus.BackColor = System.Drawing.Color.LightSlateGray;
            this.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatus.Controls.Add(this.lblCards);
            this.pnlStatus.Controls.Add(this.lblHealth);
            this.pnlStatus.Controls.Add(this.label2);
            this.pnlStatus.Controls.Add(this.label1);
            this.pnlStatus.Location = new System.Drawing.Point(915, 45);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(446, 617);
            this.pnlStatus.TabIndex = 12;
            this.pnlStatus.Visible = false;
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Location = new System.Drawing.Point(64, 22);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(15, 17);
            this.lblHealth.TabIndex = 0;
            this.lblHealth.Text = "0";
            // 
            // lblCards
            // 
            this.lblCards.AutoSize = true;
            this.lblCards.Location = new System.Drawing.Point(64, 39);
            this.lblCards.Name = "lblCards";
            this.lblCards.Size = new System.Drawing.Size(15, 17);
            this.lblCards.TabIndex = 1;
            this.lblCards.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Health: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cards:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1366, 674);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.btnAttack);
            this.Controls.Add(this.lvOpponent);
            this.Controls.Add(this.lvCreatures);
            this.Controls.Add(this.lvCards);
            this.Controls.Add(this.cmbIP);
            this.Controls.Add(this.lbl_static_name);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.lbSent);
            this.Controls.Add(this.lbl_static_status);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lbRecieved);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.Text = "Game Window";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbRecieved;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lbl_static_status;
        private System.Windows.Forms.ListBox lbSent;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Timer tmrPing;
        private System.Windows.Forms.ComboBox cmbIP;
        private System.Windows.Forms.ListView lvCards;
        private System.Windows.Forms.ListView lvCreatures;
        private System.Windows.Forms.ListView lvOpponent;
        private System.Windows.Forms.Button btnAttack;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lbl_static_name;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblCards;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

