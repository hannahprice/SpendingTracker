using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public interface ICategoriesService
    {
        Task<List<Category>?> GetAllCategories();

        Task<int> AddCategory(Category category);
        Task<Category?> GetCategory(int id);
        Task DeleteCategory(int id);
    }
}