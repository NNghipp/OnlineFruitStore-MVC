using Microsoft.EntityFrameworkCore;
using MyStore.Business;
using MyStore.Business.Entities;

namespace MyStore.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyStoreContext context) : base(context) { }

        public IEnumerable<Product> GetProductsWithCategory()
        {
            return _dbSet.Include(p => p.Category).ToList();
        }

        public Product? GetProductWithCategory(int id)
        {
            return _dbSet.Include(p => p.Category).FirstOrDefault(p => p.ProductID == id);
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _dbSet.Where(p => p.CategoryID == categoryId).Include(p => p.Category).ToList();
        }

        public IEnumerable<Product> SearchByName(string name)
        {
            return _dbSet.Where(p => p.ProductName.Contains(name)).Include(p => p.Category).ToList();
        }
    }
}
