using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Album.Pages
{
    [Authorize(Policy="CanView")]
    public class TestAuthorize1Model : PageModel
    {

        public void OnGet()
        {
        }
        public void OnGetAbc() {

        }
    }
}
