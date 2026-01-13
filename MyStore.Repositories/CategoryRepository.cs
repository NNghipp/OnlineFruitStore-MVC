using Microsoft.EntityFrameworkCore;
using MyStore.Business;
using MyStore.Business.Entities;

namespace MyStore.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyStoreContext context) : base(context) { }

        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            return _dbSet.Include(c => c.Products).ToList();
        }

        public Category? GetByName(string name)
        {
            return _dbSet.FirstOrDefault(c => c.CategoryName == name);
        }
    }
}
