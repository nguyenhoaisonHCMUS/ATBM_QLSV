using MaterialSkin;
using Oracle.ManagedDataAccess.Client;
using QLNHANVIEN.BUS;
using QLNHANVIEN.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNHANVIEN.gui
{
    public partial class QLTTHomeGUI : MaterialSkin.Controls.MaterialForm
    {
        NhanVienService nhanVienService;
        PhanCongService phanCongService;
        string myusername;
        public QLTTHomeGUI(OracleConnection conn, string user)
        {
            InitializeComponent();
            this.myusername = user;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            nhanVienService = new NhanVienService(conn);
            phanCongService = new PhanCongService(conn);
            loadDefault();
            loadTTNV();    
        }

        private void loadDefault()
        {
            dgvDSNhanVien.DataSource = nhanVienService.getAll();
            dgvDSPhanCong.DataSource = phanCongService.layDSPhanCong();
            dgvPhongBan.DataSource = nhanVienService.getDSPhongBan();
            dgvDeAn.DataSource = nhanVienService.getDSDeAn();
        }
        private void loadTTNV()
        {
            dataGridView1.DataSource = nhanVienService.GetNVByUserTCTan(myusername);
        }

        private void ntThayDoiTT_Click(object sender, EventArgs e)
        {
            if (txtSDTtd.Text != "" && txtDCtd.Text != "")
            {
                int manv = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value.ToString());
                try
                {
                    nhanVienService.updateMyInfo(manv, dateTimePicker1.Value.Date, txtDCtd.Text, txtSDTtd.Text);
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
    }
}
