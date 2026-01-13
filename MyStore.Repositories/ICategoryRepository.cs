using MyStore.Business.Entities;

namespace MyStore.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesWithProducts();
        Category? GetByName(string name);
    }
}
