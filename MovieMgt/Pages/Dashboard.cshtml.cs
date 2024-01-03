using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieMgt.Pages
{

    public class Movie
    {
       public string id;
        public string title;
        public string description;
        public string image_url;

        public Movie(string _id, string t, string d, string i)
        {
            id = _id;
            title = t;
            description = d;
            image_url = i;
        }
    }

    public class DashboardModel : PageModel
    {

        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=moviemgt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        [BindProperty(SupportsGet = true)]
        public List<Movie> list { get; set; }
            [BindProperty(SupportsGet = true)]
        public string email { get; set; }
        [BindProperty(SupportsGet = true)]
        public string role { get; set; }
    [BindProperty(SupportsGet = true)]
        public string id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int total_movies_ { get; set; }

        [BindProperty(SupportsGet = true)]
        public int total_users_ { get; set; }


        public IActionResult OnGet()
        {

           

            email = PublicMethods.GetString(HttpContext.Session, "user-email");
             role = PublicMethods.GetString(HttpContext.Session, "user-role");
             id = PublicMethods.GetString(HttpContext.Session, "user-id");

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("/Forms/Signin");

            }

            ViewData["user-email"] = email;
            ViewData["user-role"] = role;
            ViewData["user-id"] = id;


            con.Open();
            cmd = new SqlCommand("select * from [Movie]", con);

            list= new List<Movie>();

         

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

               
               while (reader.Read())
                {

                    list.Add(new Movie(reader["id"].ToString().Trim(), reader["title"].ToString().Trim(), reader["description"].ToString().Trim(), reader["image_url"].ToString().Trim()));


                }
            }

            con.Close();

            con.Open();
            cmd = new SqlCommand("select * from [User]", con);

            int total_users = 0;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {


                while (reader.Read())
                {

                    total_users++;

                }
            }

            con.Close();


            JsonCustom jc = new JsonCustom();

            jc.GetJSON();

            jc.current_id = id;
            jc.current_email = email;
            jc.current_role = role;
            jc.total_movies = list.Count;
            total_movies_ = jc.total_movies;

            jc.total_users = total_users;
            total_users_ = jc.total_users;

            jc.SaveJSON();

            return Page();


        }




        public IActionResult OnPostDeleteMovie(int sessionCount)
        {


            cmd = new SqlCommand("delete [Movie] where id = @id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", sessionCount);
          

            cmd.ExecuteNonQuery();
            con.Close();

            JsonCustom jc = new JsonCustom();
            jc.GetJSON();
            jc.total_movies = jc.total_movies - 1;
            jc.SaveJSON();

            return RedirectToPage("/Dashboard");

        }

    }
}
