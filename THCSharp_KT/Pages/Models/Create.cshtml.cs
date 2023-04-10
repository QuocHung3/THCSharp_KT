using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace THCSharp_KT.Pages.Models
{
    public class Create1Model : PageModel
    {
        public CongNhan cnInfo = new CongNhan();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            cnInfo.congnhan = Request.Form["congnhan"];
            cnInfo.calam = Request.Form["calam"];
            cnInfo.bandangki = Request.Form["bandangki"];

            if (cnInfo.congnhan.Length == 0 || cnInfo.calam.Length == 0 || cnInfo.bandangki.Length == 0)
            {
                errorMessage = "All the field are required";
                return;
            }

            //save new student into database
            try
            {
                String connectString = "Data Source=DESKTOP-KMNS09Q;Initial Catalog=CongNhan;User ID=sa;Password=12345";
                SqlConnection connection = new SqlConnection(connectString);
                connection.Open();

                String sql = "insert into CongNhan "
                    + "values(@congnhan,@calam,@bandangki);";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@congnhan", cnInfo.congnhan);
                command.Parameters.AddWithValue("@calam", cnInfo.calam);
                command.Parameters.AddWithValue("@bandangki", cnInfo.bandangki);

                command.ExecuteNonQuery();
                Response.Redirect("/Models/Index");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            cnInfo.congnhan = "";
            cnInfo.calam = "";
            cnInfo.bandangki = "";
            successMessage = "New student added correctly";
        }
    }
}
