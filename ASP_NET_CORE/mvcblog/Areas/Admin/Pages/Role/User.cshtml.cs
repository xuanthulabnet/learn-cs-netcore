using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using mvcblog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Album.Areas.Admin.Pages.Role {
    public class UserModel : PageModel {
        const int USER_PER_PAGE = 10;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signManager;


        public UserModel (RoleManager<IdentityRole> roleManager,
                          UserManager<AppUser> userManager,
                          SignInManager<AppUser> signManager) {
            _roleManager = roleManager;
            _userManager = userManager;
            _signManager = signManager;
        }

        public class UserInList : AppUser {
            // Liệt kê các Role của User ví dụ: "Admin,Editor" ...
            public string listroles {set; get;}
        }

        public List<UserInList> users;
        public int totalPages {set; get;}

        [TempData] // Sử dụng Session
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet=true)]
        public int pageNumber {set;get;}

        public IActionResult OnPost() => NotFound("Cấm post");

        public async Task<IActionResult> OnGet() {
        
            var cuser = await _userManager.GetUserAsync(User);

        //    await _userManager.AddClaimAsync(cuser, new  System.Security.Claims.Claim("X", "G"));
           var roleeditor = await _roleManager.FindByNameAsync("Editor");
            // await _roleManager.AddClaimAsync(roleeditor, new System.Security.Claims.Claim("X", "Y"));
        //    await _roleManager.AddClaimAsync(roleeditor, new System.Security.Claims.Claim("X", "Z"));

            // var cls = await _userManager.GetClaimsAsync(cuser);
            // foreach(var cl in cls) {
            //     Console.WriteLine("User Claim" + cl.Type+ "       Value:" + cl.Value);
            // }

            // cls = await _roleManager.GetClaimsAsync(roleeditor);
            // foreach(var cl in cls) {
            //     Console.WriteLine("Role Claim" + cl.Type+ "       Value:" + cl.Value);
            // }   
            
            


            if (pageNumber == 0) 
                pageNumber = 1; 

            var lusers  = (from u in _userManager.Users
                          orderby u.UserName
                          select new UserInList() { 
                              Id = u.Id, UserName = u.UserName,
                          });


            int totalUsers = await lusers.CountAsync();
        

            totalPages = (int)Math.Ceiling((double)totalUsers / USER_PER_PAGE);  

            users = await lusers.Skip(USER_PER_PAGE * (pageNumber - 1)).Take(USER_PER_PAGE).ToListAsync();
        
            // users.ForEach(async (user) => {
            //     var roles = await _userManager.GetRolesAsync(user);
            //     user.listroles = string.Join(",", roles.ToList());
            // });

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.listroles = string.Join(",", roles.ToList());
            }

            return Page();
        }
    }
}