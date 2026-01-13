using MyStore.Business;
using MyStore.Business.Entities;
using MyStore.Repositories;

namespace MyStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetProductsWithCategory();
        }

        public Product? GetProductById(int id)
        {
            return _productRepository.GetProductWithCategory(id);
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _productRepository.GetProductsByCategory(categoryId);
        }

        public IEnumerable<Product> SearchProducts(string name)
        {
            return _productRepository.SearchByName(name);
        }

        public void CreateProduct(Product product)
        {
            _productRepository.Add(product);
            _productRepository.Save();
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _productRepository.Save();
        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(product);
                _productRepository.Save();
            }
        }
    }
}
