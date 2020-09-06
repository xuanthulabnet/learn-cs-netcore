using Microsoft.EntityFrameworkCore;
using razor08.efcore.Models;

namespace razor08.efcore.Data
{
    public class ArticleContext : DbContext
    {
        public ArticleContext(DbContextOptions<ArticleContext> options) : base(options)
        {

        }
        public DbSet<Article> Article {set; get;}
    }
}