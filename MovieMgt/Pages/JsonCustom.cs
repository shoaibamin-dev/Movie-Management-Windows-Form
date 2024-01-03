using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMgt.Pages
{
    public  class JsonCustom
    {
        public  string current_id = "";
        public  string current_email = "";
        public  string current_role = "";
        public  int total_users = 0;
        public  int total_movies = 0;

        public JsonCustom GetJSON()
        {
            string parsed = "";
            parsed = File.ReadAllText(@"meta-data.json");
            return JsonConvert.DeserializeObject<JsonCustom>(parsed);
        }

        public  void SaveJSON()
        {
            string stringify = JsonConvert.SerializeObject(this);
            File.WriteAllText(@"meta-data.json", stringify);
        }

    }
}
