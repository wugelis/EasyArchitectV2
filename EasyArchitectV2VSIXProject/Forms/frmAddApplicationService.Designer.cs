namespace EasyArchitectV2VSIXProject.Forms
{
    partial class frmAddApplicationService
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpDomainLayer = new System.Windows.Forms.GroupBox();
            this.radioConcertTicket = new System.Windows.Forms.RadioButton();
            this.radioRentalCar = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpDomainLayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::EasyArchitectV2VSIXProject.Properties.Resources.Clean_Architecture_Infrastructuure_02;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 357);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // grpDomainLayer
            // 
            this.grpDomainLayer.Controls.Add(this.radioConcertTicket);
            this.grpDomainLayer.Controls.Add(this.radioRentalCar);
            this.grpDomainLayer.Location = new System.Drawing.Point(170, 12);
            this.grpDomainLayer.Name = "grpDomainLayer";
            this.grpDomainLayer.Size = new System.Drawing.Size(230, 285);
            this.grpDomainLayer.TabIndex = 9;
            this.grpDomainLayer.TabStop = false;
            this.grpDomainLayer.Text = "選擇使用的應用的類型（Preview 版本..";
            // 
            // radioConcertTicket
            // 
            this.radioConcertTicket.AutoSize = true;
            this.radioConcertTicket.Location = new System.Drawing.Point(38, 108);
            this.radioConcertTicket.Name = "radioConcertTicket";
            this.radioConcertTicket.Size = new System.Drawing.Size(115, 16);
            this.radioConcertTicket.TabIndex = 1;
            this.radioConcertTicket.Text = "X宏售票系統模型";
            this.radioConcertTicket.UseVisualStyleBackColor = true;
            // 
            // radioRentalCar
            // 
            this.radioRentalCar.AutoSize = true;
            this.radioRentalCar.Checked = true;
            this.radioRentalCar.Location = new System.Drawing.Point(38, 57);
            this.radioRentalCar.Name = "radioRentalCar";
            this.radioRentalCar.Size = new System.Drawing.Size(119, 16);
            this.radioRentalCar.TabIndex = 0;
            this.radioRentalCar.TabStop = true;
            this.radioRentalCar.Text = "線上租車系統模型";
            this.radioRentalCar.UseVisualStyleBackColor = true;
            this.radioRentalCar.CheckedChanged += new System.EventHandler(this.radioRentalCar_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(299, 315);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 52);
            this.button1.TabIndex = 12;
            this.button1.Text = "(&C) 取消";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(170, 315);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(101, 52);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "(&O) 確定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmAddApplicationService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(419, 379);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpDomainLayer);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmAddApplicationService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "建構六角架構的應用 UseCase 模型 (Application Services)";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpDomainLayer.ResumeLayout(false);
            this.grpDomainLayer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grpDomainLayer;
        private System.Windows.Forms.RadioButton radioConcertTicket;
        private System.Windows.Forms.RadioButton radioRentalCar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOk;
    }
}