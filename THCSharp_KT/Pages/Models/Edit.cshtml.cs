using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace THCSharp_KT.Pages.Models
{
    public class Edit1Model : PageModel
    {
        public CongNhan cnInfo = new CongNhan();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectString = "Data Source=DESKTOP-KMNS09Q;Initial Catalog=CongNhan;User ID=sa;Password=12345";
                SqlConnection connection = new SqlConnection(connectString);
                connection.Open();

                String sql = "select * from CongNhan where congnhan = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cnInfo.congnhan = "" + reader.GetString(0);
                    cnInfo.calam = "" + reader.GetString(1);
                    cnInfo.bandangki = "" + reader.GetString(2);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            String id = Request.Query["id"];

            cnInfo.congnhan = Request.Form["congnhan"];
            cnInfo.calam = Request.Form["calam"];
            cnInfo.bandangki = Request.Form["bandangki"];

            if (cnInfo.congnhan.Length == 0 || cnInfo.calam.Length == 0 ||
                cnInfo.bandangki.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }


            try
            {
                String connectString = "Data Source=DESKTOP-KMNS09Q;Initial Catalog=CongNhan;User ID=sa;Password=12345";
                SqlConnection connection = new SqlConnection(connectString);
                connection.Open();

                String sql = "update CongNhan " +
                    "set calam = @calam ,bandangki = @bandangki " +
                    "where congnhan = @congnhan";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@calam", cnInfo.calam);
                command.Parameters.AddWithValue("@bandangki", cnInfo.bandangki);
                command.Parameters.AddWithValue("@congnhan", id);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Models/Index");
        }
    }
}
