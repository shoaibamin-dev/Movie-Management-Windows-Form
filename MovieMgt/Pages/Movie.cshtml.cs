using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieMgt.Pages
{

    public class Comment
    {
        public string id;
        public string text;
        public string email;
     

        public Comment(string _id, string t, string e)
        {
            id = _id;
            text = t;
            email = e;
        }
    }
    public class MovieModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string title { get; set; }
        [BindProperty(SupportsGet = true)]
        public string description { get; set; }
        [BindProperty(SupportsGet = true)]
        public string image_url { get; set; }

        [BindProperty(SupportsGet = true)]
        public string id { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<Comment> list { get; set; }

        [BindProperty]
        public string add_comment { get; set; }

        [BindProperty(SupportsGet = true)]
        public string c_role { get; set; }

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



            c_role = PublicMethods.GetString(HttpContext.Session, "user-role");


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

            PublicMethods.SetString(HttpContext.Session, "movie-id", id.ToString());

            cmd = new SqlCommand("select c.Id as id, u.email as email, c.text as text  from [Comment] as c left join [User] as u on c.uid = u.id where c.mid = @mid", con);
            cmd.Parameters.AddWithValue("@mid", id);


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {


                    list.Add(new Comment(id = reader["id"].ToString().Trim(), reader["text"].ToString().Trim(), reader["email"].ToString().Trim()));
                  

                }
            }


            con.Close();


            return Page();
        }

        public IActionResult OnPost()
        {



            string c_id = PublicMethods.GetString(HttpContext.Session, "user-id");



            cmd = new SqlCommand("insert into [Comment](uid,mid,text,active) values(@uid,@mid,@text,1)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@uid", c_id);
            cmd.Parameters.AddWithValue("@mid", id);
            cmd.Parameters.AddWithValue("@text", add_comment);

            cmd.ExecuteNonQuery();
            con.Close();

            return Redirect("~/Movie?id="+id);

        }

        public IActionResult OnPostDeleteComment(int sessionCount)
        {


            cmd = new SqlCommand("delete [Comment] where id = @id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", sessionCount);


            cmd.ExecuteNonQuery();
            con.Close();


            string m_id = PublicMethods.GetString(HttpContext.Session, "movie-id");

            return Redirect("~/Movie?id=" + m_id);


        }
    }
}
