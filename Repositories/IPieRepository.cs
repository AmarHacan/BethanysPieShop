using BethanysPieShop.Models;

namespace BethanysPieShop.Repositories
{
    public interface IPieRepository
    {
        //to decalre property only readonly we can specify it getter method
        IEnumerable<Pie> AllPies { get; }
       
        IEnumerable<Pie> PiesOfWeek { get; }
        Pie? GetPieById(int pieId);
        IEnumerable<Pie> SearchPies(string searchQuery);

    }

}
