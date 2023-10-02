using BethanysPieShop.Models;
using BethanysPieShop.Repositories;

namespace BethanysPieShop.MockRepos
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories =>
            new List<Category> {
                new Category{CategoryId=1, CategoryName="Custom Made Pie", Description="Fruity Pies"},
                new Category{CategoryId=2, CategoryName="Cheese cakes", Description="Cheesy all the way"},
                new Category{CategoryId=3, CategoryName="Seasonal pies", Description="Get in the mood for a seasonal pie"}

        };
    }
}
