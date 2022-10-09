using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Categories
{
    public partial class AddCategoryOrSubcategory
    {
        [Inject] public ICategoriesService CategoriesService { get; set; }

        [Inject] public ISubcategoriesService SubcategoriesService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] private ISnackbar Snackbar { get; set; }

        private Category Category { get; set; } = new Category();
        private Subcategory Subcategory { get; set; } = new Subcategory();
        private List<Category>? AvailableCategories { get; set; } = new List<Category>();

        private bool Success { get; set; }
        private MudForm CategoryForm { get; set; }
        private MudForm SubcategoryForm { get; set; }
        private bool AddingMultiple { get; set; } = false;
        private bool AddingSubcategory { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            AvailableCategories = await CategoriesService.GetAllCategories();
        }

        private async Task SubmitCategory()
        {
            await CategoryForm.Validate();

            if (CategoryForm.IsValid)
            {
                var createdId = await CategoriesService.AddCategory(Category);
                Success = createdId > 0;

                AddCategorySnackBarMessage();
                CategoryForm.Reset();

                if (!AddingMultiple)
                {
                    NavigationManager.NavigateTo("/categories", false);
                }
                else
                {
                    AvailableCategories = await CategoriesService.GetAllCategories();
                }
            }
        }

        private async Task SubmitSubcategory()
        {
            await SubcategoryForm.Validate();

            if (SubcategoryForm.IsValid)
            {
                var createdId = await SubcategoriesService.AddSubcategory(Subcategory);
                Success = createdId > 0;

                AddSubcategorySnackBarMessage();
                SubcategoryForm.Reset();

                if (!AddingMultiple)
                {
                    NavigationManager.NavigateTo("/categories", false);
                }
            }
        }

        private void AddSubcategorySnackBarMessage()
        {
            if (Success)
            {
                Snackbar.Add($"Subcategory added: {Subcategory.Description}", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Error adding subcategory: {Subcategory.Description}", Severity.Error);
            }
        }

        private void AddCategorySnackBarMessage()
        {
            if (Success)
            {
                Snackbar.Add($"Category added: {Category.Description}", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Error adding category: {Category.Description}", Severity.Error);
            }
        }
    }
}