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
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public Category Category { get; set; } = new Category();

        public bool? Success { get; set; } = null;
        public MudForm Form { get; set; }
        public bool AddingMultiple { get; set; } = false;

        public async Task Submit()
        {
            await Form.Validate();

            if (Form.IsValid)
            {
                var createdId = await CategoriesService.AddCategory(Category);
                Success = createdId > 0;

                AddSnackBarMessage();
                Form.Reset();

                if (!AddingMultiple)
                {
                    NavigationManager.NavigateTo("/categories", false);
                }
            }
        }

        private void AddSnackBarMessage()
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