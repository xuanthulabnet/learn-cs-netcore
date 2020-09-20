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

namespace Album.Areas.Admin.Pages.Role {
  public class DeleteModel : PageModel {
    private readonly RoleManager<IdentityRole> _roleManager;

    public DeleteModel (RoleManager<IdentityRole> roleManager) {
      _roleManager = roleManager;
    }

    public class InputModel {
      [Required]
      public string ID { set; get; }
      public string Name { set; get; }

    }

    [BindProperty]
    public InputModel Input { set; get; }

    [BindProperty]
    public bool isConfirmed { set; get; }

    [TempData] // Sử dụng Session
    public string StatusMessage { get; set; }

    public IActionResult OnGet () => NotFound ("Không thấy");

    public async Task<IActionResult> OnPost () {

      if (!ModelState.IsValid) {
        return NotFound ("Không xóa được");
      }

      var role = await _roleManager.FindByIdAsync (Input.ID);
      if (role == null) {
        return NotFound ("Không thấy role cần xóa");
      }

      ModelState.Clear ();

      if (isConfirmed) {
        //Xóa
        await _roleManager.DeleteAsync (role);
        StatusMessage = "Đã xóa " + role.Name;

        return RedirectToPage ("Index");
      } else {
        Input.Name = role.Name;
        isConfirmed = true;

      }

      return Page ();
    }
  }
}