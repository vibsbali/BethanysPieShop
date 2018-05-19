using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class AppDbContext : DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> options)
       :base(options)
       {
          
       }

       public DbSet<Pie> Pies { get; set; }
    }
}
