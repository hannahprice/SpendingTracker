using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Categories
{
    public partial class CategoryManagement
    {
        [Inject]
        public ICategoriesService CategoriesService { get; set; }
        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
        public bool IsLoading { get; set; } = false;
        private List<Category> Categories { get; set; } = new List<Category>();

        private TableGroupDefinition<Subcategory> _groupDefinition = new()
        {
            GroupName = "Category",
            Indentation = false,
            Expandable = true,
            IsInitiallyExpanded = false,
            Selector = (c) => c.CategoryId
        };

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            Categories = await CategoriesService.GetAllCategories();
            PopulateSubcategories();

            IsLoading = false;
        }

        private void PopulateSubcategories()
        {
            foreach (var category in Categories)
            {
                if (category.Subcategories != null && category.Subcategories.Any())
                {
                    Subcategories.AddRange(category.Subcategories);
                }
            }
        }

        public string? GetGroupName(object categoryId)
        {
            if (categoryId != null)
            {
                var id = int.Parse(categoryId.ToString());
                return Categories?.SingleOrDefault(x => x?.Id == id)?.Description;
            }
            return String.Empty;
        }
    }
}
