using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Identity;
using Album.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Album.Pages
{
    // [Authorize(Policy="MinimumAge")]
    public class TestAuthorize1Model : PageModel
    {

        private readonly IAuthorizationService _authorizationService;
        public TestAuthorize1Model(IAuthorizationService authorizationService) {

            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> OnGet()
        {
            // Post thực tế được nạp từ DB ... ở đây để kiểm tra tạo đối tượng
            // như sau
            var post = new Post() {
                UserID = "51d9d99d-85fe-411e-b36e-1bf981cb9db3", // thay bằng các ID khác nhau để kiểm tra
            };
            
            // Kiểm tra nhóm Admin hoặc chủ sở hữu Post thì có quyênn
            var rs = await _authorizationService.AuthorizeAsync(User, post, 
                                                                new CanUpdatePostRequirement(true, true));
            if (!rs.Succeeded) {
                return Forbid();
            }
            // Có quyền
            return Page();
        }
    }
}
