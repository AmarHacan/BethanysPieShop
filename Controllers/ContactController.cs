using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class ContactController: Controller
    {

        public ContactController()
        {
                
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
