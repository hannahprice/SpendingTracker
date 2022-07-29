using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Categories
{
    public partial class CategoryList
    {
        [Inject] public ICategoriesService CategoriesService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
        public bool IsLoading { get; set; } = false;
        private List<Category> Categories { get; set; } = new List<Category>();

        private TableGroupDefinition<Subcategory> _groupDefinition = new()
        {
            Indentation = false,
            Expandable = true,
            IsInitiallyExpanded = false,
            Selector = (e) => e.CategoryId
        };
        
        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            Categories = await CategoriesService.GetAllCategories();
            Subcategories = Categories.Where(x => x.Subcategories != null && x.Subcategories.Any())
                .SelectMany(x => x.Subcategories!).ToList();

            AddCategoriesWithNoSubcategories();

            IsLoading = false;
        }

        private void AddCategoriesWithNoSubcategories()
        {
            var categoriesWithNoSubcategories =
                Categories.Where(x => x.Subcategories is null || !x.Subcategories.Any()).ToList();

            foreach (var category in categoriesWithNoSubcategories)
            {
                var subcategory = new Subcategory
                {
                    CategoryId = category.Id,
                    Description = string.Empty
                };
                Subcategories.Add(subcategory);
            }
        }

        private void NavigateToCategoryDetail(object categoryId)
        {
            NavigationManager.NavigateTo($"categories/{categoryId}");
        }

        private void NavigateToSubcategoryDetail(object subcategoryId)
        {
            NavigationManager.NavigateTo($"subcategories/{subcategoryId}");
        }

        private string GetGroupName(object categoryId)
        {
            var id = int.Parse(categoryId.ToString());
            return Categories.First(x => x.Id == id).Description;
        }
    }
}