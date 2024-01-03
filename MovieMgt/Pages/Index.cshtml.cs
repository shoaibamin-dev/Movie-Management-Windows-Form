using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMgt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            ViewData["user-email"] = "";
            ViewData["user-role"] = "";
            ViewData["user-id"] = "";

            JsonCustom jc = new JsonCustom();
            jc.GetJSON();
            jc.current_id = "";
            jc.current_email = "";
            jc.current_role = "";
            jc.SaveJSON();
        }
    }
}
