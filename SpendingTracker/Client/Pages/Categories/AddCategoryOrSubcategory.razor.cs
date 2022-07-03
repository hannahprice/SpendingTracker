using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Categories
{
    public partial class AddCategoryOrSubcategory
    {
        [Inject]
        public ICategoriesService CategoriesService { get; set; }

        [Inject]
        public ISubcategoriesService SubcategoriesService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public Category Category { get; set; } = new Category();
        public Subcategory Subcategory { get; set; } = new Subcategory();
        public List<Category> AvailableCategories { get; set; } = new List<Category>();

        public bool? Success { get; set; } = null;
        public MudForm CategoryForm { get; set; }
        public MudForm SubcategoryForm { get; set; }
        public bool AddingMultiple { get; set; } = false;
        public bool AddingSubcategory { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            AvailableCategories = await CategoriesService.GetAllCategories();
        }

        public async Task SubmitCategory()
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

        public async Task SubmitSubcategory()
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
            if (Success.Value)
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
            if (Success.Value)
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