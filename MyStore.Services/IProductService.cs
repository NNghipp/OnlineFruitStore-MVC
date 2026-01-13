using MyStore.Business.Entities;

namespace MyStore.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetProductById(int id);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        IEnumerable<Product> SearchProducts(string name);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
