namespace QuanLyNhanSu
{
    partial class fTimKiem
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
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnTim = new System.Windows.Forms.Button();
            this.gbLuong = new System.Windows.Forms.GroupBox();
            this.txtDen = new System.Windows.Forms.TextBox();
            this.lblDen = new System.Windows.Forms.Label();
            this.txtTu = new System.Windows.Forms.TextBox();
            this.lblTu = new System.Windows.Forms.Label();
            this.cboLoaiLuong = new System.Windows.Forms.ComboBox();
            this.lblLoaiLuong = new System.Windows.Forms.Label();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.lblPhongBan = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.gbLuong.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(158, 228);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 26;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(239, 228);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 23);
            this.btnTim.TabIndex = 25;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // gbLuong
            // 
            this.gbLuong.Controls.Add(this.txtDen);
            this.gbLuong.Controls.Add(this.lblDen);
            this.gbLuong.Controls.Add(this.txtTu);
            this.gbLuong.Controls.Add(this.lblTu);
            this.gbLuong.Controls.Add(this.cboLoaiLuong);
            this.gbLuong.Controls.Add(this.lblLoaiLuong);
            this.gbLuong.Location = new System.Drawing.Point(14, 102);
            this.gbLuong.Name = "gbLuong";
            this.gbLuong.Size = new System.Drawing.Size(307, 108);
            this.gbLuong.TabIndex = 24;
            this.gbLuong.TabStop = false;
            this.gbLuong.Text = "Lọc theo lương";
            // 
            // txtDen
            // 
            this.txtDen.Location = new System.Drawing.Point(75, 77);
            this.txtDen.Name = "txtDen";
            this.txtDen.Size = new System.Drawing.Size(226, 20);
            this.txtDen.TabIndex = 6;
            this.txtDen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDen_KeyPress_1);
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Location = new System.Drawing.Point(7, 80);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(30, 13);
            this.lblDen.TabIndex = 5;
            this.lblDen.Text = "Đến:";
            // 
            // txtTu
            // 
            this.txtTu.Location = new System.Drawing.Point(75, 47);
            this.txtTu.Name = "txtTu";
            this.txtTu.Size = new System.Drawing.Size(226, 20);
            this.txtTu.TabIndex = 4;
            this.txtTu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTu_KeyPress_1);
            // 
            // lblTu
            // 
            this.lblTu.AutoSize = true;
            this.lblTu.Location = new System.Drawing.Point(10, 50);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(23, 13);
            this.lblTu.TabIndex = 2;
            this.lblTu.Text = "Từ:";
            // 
            // cboLoaiLuong
            // 
            this.cboLoaiLuong.FormattingEnabled = true;
            this.cboLoaiLuong.Items.AddRange(new object[] {
            "Lương CB, Phụ cấp, Tổng"});
            this.cboLoaiLuong.Location = new System.Drawing.Point(75, 17);
            this.cboLoaiLuong.Name = "cboLoaiLuong";
            this.cboLoaiLuong.Size = new System.Drawing.Size(226, 21);
            this.cboLoaiLuong.TabIndex = 1;
            // 
            // lblLoaiLuong
            // 
            this.lblLoaiLuong.AutoSize = true;
            this.lblLoaiLuong.Location = new System.Drawing.Point(7, 20);
            this.lblLoaiLuong.Name = "lblLoaiLuong";
            this.lblLoaiLuong.Size = new System.Drawing.Size(30, 13);
            this.lblLoaiLuong.TabIndex = 0;
            this.lblLoaiLuong.Text = "Loại:";
            // 
            // cboPhongBan
            // 
            this.cboPhongBan.FormattingEnabled = true;
            this.cboPhongBan.Location = new System.Drawing.Point(89, 69);
            this.cboPhongBan.Name = "cboPhongBan";
            this.cboPhongBan.Size = new System.Drawing.Size(226, 21);
            this.cboPhongBan.TabIndex = 23;
            // 
            // lblPhongBan
            // 
            this.lblPhongBan.AutoSize = true;
            this.lblPhongBan.Location = new System.Drawing.Point(21, 72);
            this.lblPhongBan.Name = "lblPhongBan";
            this.lblPhongBan.Size = new System.Drawing.Size(62, 13);
            this.lblPhongBan.TabIndex = 22;
            this.lblPhongBan.Text = "Phòng ban:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(89, 39);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(226, 20);
            this.txtHoTen.TabIndex = 21;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(21, 42);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(42, 13);
            this.lblHoTen.TabIndex = 20;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(89, 9);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(226, 20);
            this.txtMaNV.TabIndex = 19;
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Location = new System.Drawing.Point(21, 12);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(43, 13);
            this.lblMaNV.TabIndex = 18;
            this.lblMaNV.Text = "Mã NV:";
            // 
            // fTimKiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 261);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.gbLuong);
            this.Controls.Add(this.cboPhongBan);
            this.Controls.Add(this.lblPhongBan);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.txtMaNV);
            this.Controls.Add(this.lblMaNV);
            this.Name = "fTimKiem";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.fTimKiem_Load_1);
            this.gbLuong.ResumeLayout(false);
            this.gbLuong.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.GroupBox gbLuong;
        private System.Windows.Forms.TextBox txtDen;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.TextBox txtTu;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.ComboBox cboLoaiLuong;
        private System.Windows.Forms.Label lblLoaiLuong;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.Label lblPhongBan;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label lblMaNV;
    }
}