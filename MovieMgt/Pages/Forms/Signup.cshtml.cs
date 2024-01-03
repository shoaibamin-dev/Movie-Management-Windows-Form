using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieMgt.Pages.Forms
{
   
    public class SignupModel : PageModel
    {
        [BindProperty]
        public string email  { get; set; }
        [BindProperty]

        public string password   { get; set; }
        [BindProperty]

        public string role { get; set; }

        public string alert = "";

        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=moviemgt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {

            cmd = new SqlCommand("insert into [User](email,password,role) values(@email,@password,@role)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@role", role);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                ViewData["Message"] = "User Already Registered";
                alert = "User Already Registered";
                return RedirectToPage("/Forms/Signup");
            }
            con.Close();

            JsonCustom jc = new JsonCustom();
            jc.GetJSON();
              jc.total_users = jc.total_users + 1;
             jc.SaveJSON();

            alert = "User  Registered";
            ViewData["Message"] = ("User Registered");
            return RedirectToPage("/Forms/Signin");
        }
    }
}
