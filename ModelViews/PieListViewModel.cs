using BethanysPieShop.Models;

namespace BethanysPieShop.ModelViews
{
    public class PieListViewModel
    {

        public IEnumerable<Pie> Pies { get; }
        public string? CurrentCategory { get; }
        public PieListViewModel(IEnumerable<Pie> pie, string curCateg) {

            Pies = pie;
            CurrentCategory = curCateg;
        }
    }
}
