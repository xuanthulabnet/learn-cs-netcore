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
    public class AddModel : PageModel {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddModel (RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;
        }

        [TempData] // Sử dụng Session
        public string StatusMessage { get; set; }

        public class InputModel {
            public string ID { set; get; }

            [Required (ErrorMessage = "Phải nhập tên role")]
            [Display (Name = "Tên của Role")]
            [StringLength (100, ErrorMessage = "{0} dài {2} đến {1} ký tự.", MinimumLength = 3)]
            public string Name { set; get; }

        }
        [BindProperty]
        public InputModel Input { set; get; }

        [BindProperty]
        public bool IsUpdate { set; get; }

        public IActionResult OnGet () => NotFound ("Không thấy");
        public IActionResult OnPost () => NotFound ("Không thấy");
        public IActionResult OnPostStartNewRole () {
            StatusMessage = "Hãy nhập thông tin để tạo role mới";
            IsUpdate = false;
            ModelState.Clear ();
            return Page ();
        }
        public async Task<IActionResult> OnPostStartUpdate () {
            StatusMessage = null;
            IsUpdate = true;
            if (Input.ID == null) {
                StatusMessage = "Error: Không có thông tin về Role";
                return Page ();
            }
            var result = await _roleManager.FindByIdAsync (Input.ID);
            if (result != null) {
                Input.Name = result.Name;
                ViewData["Title"] = "Cập nhật role : " + Input.Name;
                ModelState.Clear ();
            } else {
                StatusMessage = "Error: Không có thông tin về Role ID = " + Input.ID;
            }

            return Page ();
        }

        public async Task<IActionResult> OnPostAddOrUpdate () {

            if (!ModelState.IsValid) {
                StatusMessage = null;
                return Page ();
            }

            if (IsUpdate) {
                // CẬP NHẬT
                if (Input.ID == null) {
                    ModelState.Clear ();
                    StatusMessage = "Error: Không có thông tin về role";
                    return Page ();
                }
                var result = await _roleManager.FindByIdAsync (Input.ID);
                if (result != null) {
                    result.Name = Input.Name;
                    var roleUpdateRs = await _roleManager.UpdateAsync (result);
                    if (roleUpdateRs.Succeeded) {
                        StatusMessage = "Đã cập nhật role thành công";
                    } else {
                        StatusMessage = "Error: ";
                        foreach (var er in roleUpdateRs.Errors) {
                            StatusMessage += er.Description;
                        }
                    }
                } else {
                    StatusMessage = "Error: Không tìm thấy Role cập nhật";
                }

            } else {
                // TẠO MỚI
                var newRole = new IdentityRole (Input.Name);
                var rsNewRole = await _roleManager.CreateAsync (newRole);
                if (rsNewRole.Succeeded) {
                    StatusMessage = $"Đã tạo role mới thành công: {newRole.Name}";
                    return RedirectToPage("./Index");
                } else {
                    StatusMessage = "Error: ";
                    foreach (var er in rsNewRole.Errors) {
                        StatusMessage += er.Description;
                    }
                }
            }

            return Page ();

        }
    }
}