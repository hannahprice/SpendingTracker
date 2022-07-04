using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Categories
{
    public partial class CategoryList
    {
        [Inject]
        public ICategoriesService CategoriesService { get; set; }

        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
        public bool IsLoading { get; set; } = false;
        private List<Category> Categories { get; set; } = new List<Category>();

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            Categories = await CategoriesService.GetAllCategories();

            IsLoading = false;
        }
    }
}