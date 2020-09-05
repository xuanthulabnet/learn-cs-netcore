using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace razor07.uploadfiles.Pages {
  public class UploadMultiModel : PageModel {

    private IHostingEnvironment _environment;
    public UploadMultiModel (IHostingEnvironment environment) {
      _environment = environment;
    }

    [Required (ErrorMessage = "Chọn một file")]
    [DataType (DataType.Upload)]
    [FileExtensions (Extensions = "png,jpg,jpeg,gif")]
    [Display (Name = "Chọn file upload")]
    [BindProperty]
    public IFormFile[] FileUploads { get; set; }
    public async Task OnPostAsync () {
      if (FileUploads != null) {
         foreach (var FileUpload in FileUploads)
         {
              var file = Path.Combine (_environment.ContentRootPath, "uploads", FileUpload.FileName);
              using (var fileStream = new FileStream (file, FileMode.Create)) {
                await FileUpload.CopyToAsync (fileStream);
              }
         }
      }

    }

  }
}