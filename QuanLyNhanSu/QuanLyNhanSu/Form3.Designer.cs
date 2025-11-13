namespace QuanLyNhanSu
{
    partial class Form3
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
            this.lblMaNV = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblPhong = new System.Windows.Forms.Label();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.lblCB = new System.Windows.Forms.Label();
            this.txtLuongCB = new System.Windows.Forms.TextBox();
            this.lblPC = new System.Windows.Forms.Label();
            this.txtPhuCap = new System.Windows.Forms.TextBox();
            this.lblTong = new System.Windows.Forms.Label();
            this.txtTong = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Location = new System.Drawing.Point(11, 10);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(43, 13);
            this.lblMaNV.TabIndex = 0;
            this.lblMaNV.Text = "Mã NV:";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(11, 40);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(42, 13);
            this.lblTen.TabIndex = 1;
            this.lblTen.Text = "Họ tên:";
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(84, 6);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.ReadOnly = true;
            this.txtMaNV.Size = new System.Drawing.Size(121, 20);
            this.txtMaNV.TabIndex = 2;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(84, 37);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.ReadOnly = true;
            this.txtHoTen.Size = new System.Drawing.Size(121, 20);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblPhong
            // 
            this.lblPhong.AutoSize = true;
            this.lblPhong.Location = new System.Drawing.Point(11, 70);
            this.lblPhong.Name = "lblPhong";
            this.lblPhong.Size = new System.Drawing.Size(62, 13);
            this.lblPhong.TabIndex = 4;
            this.lblPhong.Text = "Phòng ban:";
            // 
            // cboPhongBan
            // 
            this.cboPhongBan.FormattingEnabled = true;
            this.cboPhongBan.Location = new System.Drawing.Point(84, 67);
            this.cboPhongBan.Name = "cboPhongBan";
            this.cboPhongBan.Size = new System.Drawing.Size(121, 21);
            this.cboPhongBan.TabIndex = 5;
            // 
            // lblCB
            // 
            this.lblCB.AutoSize = true;
            this.lblCB.Location = new System.Drawing.Point(11, 100);
            this.lblCB.Name = "lblCB";
            this.lblCB.Size = new System.Drawing.Size(57, 13);
            this.lblCB.TabIndex = 6;
            this.lblCB.Text = "Lương CB:";
            // 
            // txtLuongCB
            // 
            this.txtLuongCB.Location = new System.Drawing.Point(84, 97);
            this.txtLuongCB.Name = "txtLuongCB";
            this.txtLuongCB.Size = new System.Drawing.Size(121, 20);
            this.txtLuongCB.TabIndex = 7;
            this.txtLuongCB.TextChanged += new System.EventHandler(this.txtLuongCB_TextChanged);
            this.txtLuongCB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLuongCB_KeyPress);
            // 
            // lblPC
            // 
            this.lblPC.AutoSize = true;
            this.lblPC.Location = new System.Drawing.Point(12, 130);
            this.lblPC.Name = "lblPC";
            this.lblPC.Size = new System.Drawing.Size(50, 13);
            this.lblPC.TabIndex = 8;
            this.lblPC.Text = "Phụ cấp:";
            // 
            // txtPhuCap
            // 
            this.txtPhuCap.Location = new System.Drawing.Point(84, 127);
            this.txtPhuCap.Name = "txtPhuCap";
            this.txtPhuCap.Size = new System.Drawing.Size(121, 20);
            this.txtPhuCap.TabIndex = 9;
            this.txtPhuCap.TextChanged += new System.EventHandler(this.txtPhuCap_TextChanged);
            this.txtPhuCap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhuCap_KeyPress);
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Location = new System.Drawing.Point(11, 160);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(35, 13);
            this.lblTong.TabIndex = 10;
            this.lblTong.Text = "Tổng:";
            // 
            // txtTong
            // 
            this.txtTong.Location = new System.Drawing.Point(84, 157);
            this.txtTong.Name = "txtTong";
            this.txtTong.ReadOnly = true;
            this.txtTong.Size = new System.Drawing.Size(121, 20);
            this.txtTong.TabIndex = 11;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(188, 190);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 12;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 221);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtTong);
            this.Controls.Add(this.lblTong);
            this.Controls.Add(this.txtPhuCap);
            this.Controls.Add(this.lblPC);
            this.Controls.Add(this.txtLuongCB);
            this.Controls.Add(this.lblCB);
            this.Controls.Add(this.cboPhongBan);
            this.Controls.Add(this.lblPhong);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.txtMaNV);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.lblMaNV);
            this.Name = "Form3";
            this.Text = "Phòng và Lương";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.Label lblCB;
        private System.Windows.Forms.TextBox txtLuongCB;
        private System.Windows.Forms.Label lblPC;
        private System.Windows.Forms.TextBox txtPhuCap;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.TextBox txtTong;
        private System.Windows.Forms.Button btnLuu;
    }
}