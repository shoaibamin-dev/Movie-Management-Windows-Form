using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieMgt.Pages.Forms
{
   
    public  class SigninModel : PageModel
    {

        [BindProperty]
        public string email { get; set; }
        [BindProperty]

        public string password { get; set; }


        public string alert = "";

        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=moviemgt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {


           
            con.Open();
            cmd = new SqlCommand("select * from [User] where email = @email and password = @password", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            string id = "";
            string role = "";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {

                    id = reader["id"].ToString().Trim();
                    role = reader["role"].ToString().Trim();

                }
            }

            con.Close();
          
            if(id == "")
            {
                alert = "Invalid Credentials, Please Log In Again";
                ViewData["Message"] = ("Invalid Credentials, Please Log In Again");
                return RedirectToPage("/Forms/Signin");
            }
            else
            {


                PublicMethods.SetString(HttpContext.Session, "user-email", email);
                PublicMethods.SetString(HttpContext.Session, "user-role", role);
                PublicMethods.SetString(HttpContext.Session, "user-id", id);



                return RedirectToPage("/Dashboard");

            }


        }

       

       

    }
}
