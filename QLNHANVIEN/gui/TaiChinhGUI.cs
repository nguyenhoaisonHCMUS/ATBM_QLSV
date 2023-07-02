using MaterialSkin;
using Oracle.ManagedDataAccess.Client;
using QLNHANVIEN.BUS;
using QLNHANVIEN.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNHANVIEN.gui
{
    public partial class TaiChinhGUI : MaterialSkin.Controls.MaterialForm
    {
        TaiChinhService taiChinhService;
        NhanVienService nhanVienService;
        PhanCongService phanCongService;
        private DataGridViewRow row = new DataGridViewRow();
        private int maNV;
        private string luong ;
        private string phuCap ;
        string user;
        public TaiChinhGUI(OracleConnection conn, string user)
        {
            InitializeComponent();
            this.user = user;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            taiChinhService = new TaiChinhService(conn);
            nhanVienService = new NhanVienService(conn);
            phanCongService = new PhanCongService(conn);
            loadDefault();
            loadTTNV();
            loadMyInf();
        }

        private void loadDefault()
        {
            dgvNhanVien.DataSource = nhanVienService.getAll();
            dgvPhanCong.DataSource = phanCongService.layDSPhanCong();
            datagvDeAn.DataSource = nhanVienService.getDSDeAn();
            dgvPhongBan.DataSource = nhanVienService.getDSPhongBan();
        }
        private void loadTTNV()
        {
            dgvNhanVien.DataSource = nhanVienService.getAll();
        }

        private void loadMyInf()
        {
            dgvMyinf.DataSource = nhanVienService.getMyInfor();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            luong = txtLuong.Text.ToString();
            phuCap = txtPhuCap.Text.ToString();
            try
            {
                taiChinhService.capNhatLuongVaPhuCap(maNV, luong, phuCap);
                loadDefault();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvNhanVien.CurrentRow.Selected = true;
            row = dgvNhanVien.Rows[e.RowIndex];
            maNV = Convert.ToInt32(row.Cells["MANV"].Value);
            luong = row.Cells["LUONG"].Value.ToString();
            phuCap = row.Cells["PHUCAP"].Value.ToString();
            txtLuong.Text = luong;
            txtPhuCap.Text = phuCap;
            txtMaNV.Text = maNV.ToString();
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            if (txtSDT.Text != "" && txtDiaChi.Text != "")
            {
                int manv = int.Parse(nhanVienService.getNVByUser(this.user).MANV.ToString());
                NHANVIEN nv = new NHANVIEN();
                nv.MANV = (short)manv;
                nv.NGAYSINH = dtpngaySinh.Value.Date;
                nv.DIACHI = txtDiaChi.Text;
                nv.SODT = txtSDT.Text;
                try
                {
                    nhanVienService.updateTTNV(nv);
                    MessageBox.Show("thành công");
                    loadTTNV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("không được để trống thông tin!!");
            }
        }

        private void btnThayDoiMyInfor_Click(object sender, EventArgs e)
        {
            int p_manv = Convert.ToInt32(txbMaTC.Text);
            DateTime p_ngaysinh = Convert.ToDateTime(dtpngaySinh.Text);
            string p_diachi = txtDiaChi.Text;
            string p_sodt = txtSDT.Text;
            nhanVienService.updateMyInfo(p_manv, p_ngaysinh, p_diachi, p_sodt);
            loadMyInf();
        }

        private void dgvMyinf_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvMyinf.CurrentRow.Selected = true;
            txbMaTC.Text = dgvMyinf.Rows[e.RowIndex].Cells[0].Value.ToString();
            dtpngaySinh.Text = dgvMyinf.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDiaChi.Text = dgvMyinf.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSDT.Text = dgvMyinf.Rows[e.RowIndex].Cells[5].Value.ToString();
        }
    }
}
