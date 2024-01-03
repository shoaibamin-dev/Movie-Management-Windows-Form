using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieMgt.Pages.Forms
{
    public class EditMovieModel : PageModel
    {

        [BindProperty(SupportsGet =true)]
        public string title { get; set; }
        [BindProperty(SupportsGet = true)]
        public string description { get; set; }
        [BindProperty(SupportsGet = true)]
        public string image_url { get; set; }

        [BindProperty(SupportsGet = true)]
        public string id { get; set; }

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
            

            con.Open();
            cmd = new SqlCommand("select * from [Movie] where id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
         
           
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {

                    id = reader["id"].ToString().Trim();
                    title = reader["title"].ToString().Trim();
                    description = reader["description"].ToString().Trim();
                    image_url = reader["image_url"].ToString().Trim();

                }
            }

            con.Close();

            return Page();
        }

        public IActionResult OnPost()
        {
            cmd = new SqlCommand("update [Movie] set title = @title, description = @description, image_url = @image_url where id = @id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@image_url", image_url);

            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToPage("/Dashboard");

        }
    }
}
