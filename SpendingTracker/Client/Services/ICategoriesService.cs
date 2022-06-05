using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public interface ICategoriesService
    {
        Task<List<Category>> GetAllCategories();
    }
}
