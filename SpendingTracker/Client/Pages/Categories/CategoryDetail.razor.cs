using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Categories;

public partial class CategoryDetail
{
    [Parameter] public string Id { get; set; }
    [Inject] private ICategoriesService CategoriesService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    private bool DialogVisible { get; set; } = false;
    private bool IsLoading { get; set; } = false;
    private Category Category { get; set; } = new Category();
    private void ToggleDialog() => DialogVisible = !DialogVisible;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Category = await CategoriesService.GetCategory(int.Parse(Id));
        IsLoading = false;
    }
    
    private async Task DeleteCategory()
    {
        try
        {
            await CategoriesService.DeleteCategory(int.Parse(Id));
                
            ToggleDialog();
            Snackbar.Add($"Category removed", Severity.Success);
                
            NavigationManager.NavigateTo("/categories", false);
        }
        catch
        {
            Snackbar.Add($"Error removing category: {Category.Description}", Severity.Error);
        }
    }
}