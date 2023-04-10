using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace THCSharp_KT.Pages.Models
{
    

    public class IndexModel : PageModel
    {
        public List<CongNhan> listCN = new List<CongNhan>();
        public void OnGet()
        {
            String conn = "Data Source=DESKTOP-KMNS09Q;Initial Catalog=CongNhan;User ID=sa;Password=12345";
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();

            String sql = "SELECT * FROM CongNhan";
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CongNhan cn = new CongNhan();
                cn.congnhan = reader.GetString(0);
                cn.calam = reader.GetString(1);
                cn.bandangki = reader.GetString(2);
                listCN.Add(cn);
            }
        }
    }

    public class CongNhan
    {
        public string congnhan;
        public string calam;
        public string bandangki;
    }
}
