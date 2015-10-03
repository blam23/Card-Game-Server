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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbl_static_name = new System.Windows.Forms.Label();
            this.tmrPing = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lbRecieved
            // 
            this.lbRecieved.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRecieved.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecieved.FormattingEnabled = true;
            this.lbRecieved.Items.AddRange(new object[] {
            "Recieved"});
            this.lbRecieved.Location = new System.Drawing.Point(12, 16);
            this.lbRecieved.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbRecieved.Name = "lbRecieved";
            this.lbRecieved.Size = new System.Drawing.Size(836, 680);
            this.lbRecieved.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblStatus.Location = new System.Drawing.Point(917, 12);
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
            this.lbl_static_status.Location = new System.Drawing.Point(857, 12);
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
            this.lbSent.Location = new System.Drawing.Point(12, 703);
            this.lbSent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbSent.Name = "lbSent";
            this.lbSent.Size = new System.Drawing.Size(836, 329);
            this.lbSent.TabIndex = 3;
            // 
            // btnMain
            // 
            this.btnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMain.Location = new System.Drawing.Point(862, 964);
            this.btnMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(542, 68);
            this.btnMain.TabIndex = 4;
            this.btnMain.Text = "Connect";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(922, 64);
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
            this.lbl_static_name.Location = new System.Drawing.Point(857, 63);
            this.lbl_static_name.Name = "lbl_static_name";
            this.lbl_static_name.Size = new System.Drawing.Size(66, 25);
            this.lbl_static_name.TabIndex = 6;
            this.lbl_static_name.Text = "Name:";
            // 
            // tmrPing
            // 
            this.tmrPing.Interval = 7000;
            this.tmrPing.Tick += new System.EventHandler(this.tmrPing_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1414, 1045);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbRecieved;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lbl_static_status;
        private System.Windows.Forms.ListBox lbSent;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lbl_static_name;
        private System.Windows.Forms.Timer tmrPing;
    }
}

