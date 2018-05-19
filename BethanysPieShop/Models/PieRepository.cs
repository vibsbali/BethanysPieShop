using System.Collections.Generic;
using System.Linq;

namespace BethanysPieShop.Models
{
   public class PieRepository : IPieRepository
   {
      private readonly AppDbContext _dbContext;

      public PieRepository(AppDbContext dbContext)
      {
         _dbContext = dbContext;
      }

      public IEnumerable<Pie> GetAllPies()
      {
         return _dbContext.Pies.AsEnumerable();
      }

      public Pie GetPieById(int pieId)
      {
         return _dbContext.Pies.SingleOrDefault(p => p.Id == pieId);
      }
   }
}