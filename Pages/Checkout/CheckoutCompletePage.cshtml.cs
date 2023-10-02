using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BethanysPieShop.Pages.Checkout
{
    public class CheckoutCompletePageModel : PageModel
    {
        public string Message { get; set; } = "Checkout Complete Page";
        public void OnGet()
        {
        }
    }
}
