using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvcblog.Data;
using mvcblog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Album.Areas.Admin.Pages.RoleClaims
{
 
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;

        public IndexModel(RoleManager<IdentityRole> roleManager, AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
            _roleManager = roleManager;
        }
        public List<IdentityRole> roles {set; get;}

        [BindProperty(SupportsGet = true)]
        public string roleid {set; get;}

        public IdentityRole role {set; get;}

        [TempData] // Sử dụng Session lưu thông báo
        public string StatusMessage { get; set; }


        public IList<EditClaim> claims { get;set; }
 
        public async Task<IActionResult> OnGet()
        {
            Console.WriteLine(roleid);    
            if (string.IsNullOrEmpty(roleid)) 
                return NotFound("Không có role");

            role  =  await _roleManager.FindByIdAsync(roleid);

            if (role == null)
                return NotFound("Không có role");

                

            claims = await (from c in _dbContext.RoleClaims
                    where c.RoleId == roleid
                    select new EditClaim() {
                        Id = c.Id,
                        ClaimType = c.ClaimType,
                        ClaimValue = c.ClaimValue
                    }).ToListAsync(); 

            return Page();
        }
    }
}
