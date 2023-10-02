using BethanysPieShop.Data;
using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly ApplicationDbContext _context;
        public PieRepository(ApplicationDbContext context) {
            _context = context;
        }
        public IEnumerable<Pie> AllPies { get { return _context.Pies.Include(c=>c.Category).ToList(); } }

        public IEnumerable<Pie> PiesOfWeek { get { return _context.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek).ToList(); } }

        public Pie? GetPieById(int pieId)
        {
            return _context.Pies.FirstOrDefault(p=>p.PieId==pieId);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
           return _context.Pies.Where(p=>p.Name.Contains(searchQuery));    
        }
    }
}
