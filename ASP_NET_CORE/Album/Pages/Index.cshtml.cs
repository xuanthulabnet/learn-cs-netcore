using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Album.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Album.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _dbContext;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext dbcontext)
        {
            _logger = logger;
            _dbContext = dbcontext;
        }

        public void OnGet()
        {

        }
        public void OnGetDeleteDb() {
            _logger.LogInformation("Xóa DB");
            _dbContext.Database.EnsureDeleted();
        }
    }
}
