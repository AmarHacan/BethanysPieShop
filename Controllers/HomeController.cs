using BethanysPieShop.Models;
using BethanysPieShop.ModelViews;
using BethanysPieShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository= pieRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<Pie> piesofweek=_pieRepository.PiesOfWeek.ToList();
            HomeViewModel homeviewmodel = new HomeViewModel(piesofweek);
            return View(homeviewmodel);
        }
    }
}
