using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Areas.Admin.Pages.RoleClaims;
using mvcblog.Data;
using mvcblog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Album.Pages.Blog {
    public class DeleteModel : PageModel {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityRole role { set; get; }

        [BindProperty (SupportsGet = true)]
        public string roleid { set; get; }

        public DeleteModel (AppDbContext context, RoleManager<IdentityRole> roleManager) {
            _context = context;
            _roleManager = roleManager;
        }

        async Task<IdentityRole> GetRole () {

            if (string.IsNullOrEmpty (roleid)) return null;
            return await _roleManager.FindByIdAsync (roleid);
        }

        [BindProperty]
        public IdentityRoleClaim<string> EditClaim { get; set; }

        public async Task<IActionResult> OnGetAsync (int? id) {
            role = await GetRole ();
            if (role == null)
                return NotFound ("Không thấy Role");

            if (id == null) {
                return NotFound ();
            }

            EditClaim = await _context.RoleClaims.FirstOrDefaultAsync (m => m.Id == id);

            if (EditClaim == null) {
                return NotFound ();
            }
            return Page ();
        }

        public async Task<IActionResult> OnPostAsync (int? id) {

            role = await GetRole ();
            if (role == null)
                return NotFound ("Không thấy Role");

            if (id == null) {
                return NotFound ();
            }

            EditClaim = await _context.RoleClaims.FindAsync (id);

            if (EditClaim != null) {
                _context.RoleClaims.Remove (EditClaim);
                await _context.SaveChangesAsync ();
            }

            return RedirectToPage ("./Index", new {roleid = roleid});
        }
    }
}