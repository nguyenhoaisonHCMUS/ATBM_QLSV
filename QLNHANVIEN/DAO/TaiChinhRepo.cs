using Oracle.ManagedDataAccess.Client;
using QLNHANVIEN.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNHANVIEN.DAO
{
    public class TaiChinhRepo
    {
        private EntitiesDB1 db = new EntitiesDB1();
        private OracleConnection conn;

        public TaiChinhRepo() { }
        public TaiChinhRepo(OracleConnection conn)
        {
            this.conn = conn;
        }

        public OracleConnection getConnection() { return conn; }
        public void setConnection(OracleConnection conn) { this.conn = conn; }

        public void CapNhatLuongVaPhuCap(int maNV, string luong, string phucap)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "ad.TG_CAPNHATXEPHANG";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("manvv", OracleDbType.Int32).Value = maNV;
                cmd.Parameters.Add("dauvaoLUONG", OracleDbType.Varchar2).Value = luong;
                cmd.Parameters.Add("dauvaoPC", OracleDbType.Varchar2).Value = phucap;

                try
                {
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        MessageBox.Show("Cập nhật lương, phụ cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        throw (new Exception("Có lỗi xảy ra"));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
