using System.Linq;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class HomeController : Controller
    {
       private readonly IPieRepository _pieRepository;

       public HomeController(IPieRepository pieRepository)
       {
          _pieRepository = pieRepository;
       }

       public IActionResult Index()
       {
          var pies = _pieRepository.GetAllPies().OrderBy(p => p.Name);
          var homeViewModel = new HomeViewModel
          {
             Title = "My Pie Store",
             Pies = pies.ToList()
          };

          return View(homeViewModel);
       }

       public IActionResult Details(int id)
       {
          var pie = _pieRepository.GetPieById(id);
          if (pie == null)
          {
             return NotFound();
          }

          return View(pie);
       }
    }
}
