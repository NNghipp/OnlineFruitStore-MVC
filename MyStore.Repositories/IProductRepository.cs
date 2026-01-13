using MyStore.Business.Entities;

namespace MyStore.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsWithCategory();
        Product? GetProductWithCategory(int id);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        IEnumerable<Product> SearchByName(string name);
    }
}
