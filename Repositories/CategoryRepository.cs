using BethanysPieShop.Data;
using BethanysPieShop.Models;

namespace BethanysPieShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) {
            _context=context;
        }
        public IEnumerable<Category> AllCategories {get { return _context.Categories.OrderBy(c=>c.CategoryName).ToList(); } }

    }
}
