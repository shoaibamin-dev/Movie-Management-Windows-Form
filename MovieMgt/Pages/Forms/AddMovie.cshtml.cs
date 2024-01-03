using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieMgt.Pages.Forms
{

 


    public class AddMovieModel : PageModel
    {

        [BindProperty]
        public string title { get; set; }
        [BindProperty]
        public string description { get; set; }
        [BindProperty]
        public string image_url { get; set; }

        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=moviemgt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public IActionResult OnGet()
        {

            string id = PublicMethods.GetString(HttpContext.Session, "user-id");
            if (string.IsNullOrEmpty(id))
            {
                 return RedirectToPage("/Forms/Signin");

            }
            return Page();
        }

        public IActionResult OnPost()
        {



            cmd = new SqlCommand("insert into [Movie](title,description,image_url) values(@title,@description,@image_url)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@image_url", image_url);
          
                cmd.ExecuteNonQuery();
            con.Close();

            JsonCustom jc = new JsonCustom();

            jc.GetJSON();

           
            jc.total_movies = jc.total_movies+1;
           

            jc.SaveJSON();

            return RedirectToPage("/Dashboard");

        }
    }
}
