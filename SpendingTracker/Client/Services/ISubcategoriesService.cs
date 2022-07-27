using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public interface ISubcategoriesService
    {
        Task<int> AddSubcategory(Subcategory subcategory);
        Task<Subcategory> GetSubcategory(int id);
    }
}