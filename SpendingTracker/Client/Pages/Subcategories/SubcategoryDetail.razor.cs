using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Subcategories;

public partial class SubcategoryDetail
{
    [Parameter] public string Id { get; set; }
    [Inject] private ISubcategoriesService SubcategoriesService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    private bool IsLoading { get; set; } = false;
    private bool DialogVisible { get; set; } = false;
    private void ToggleDialog() => DialogVisible = !DialogVisible;

    private Subcategory Subcategory { get; set; } = new Subcategory();

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Subcategory = await SubcategoriesService.GetSubcategory(int.Parse(Id));
        IsLoading = false;
    }   
    private async Task DeleteSubcategory()
    {
        try
        {
            await SubcategoriesService.DeleteSubcategory(int.Parse(Id));
                
            ToggleDialog();
            Snackbar.Add($"Subcategory removed", Severity.Success);
                
            NavigationManager.NavigateTo("/categories", false);
        }
        catch
        {
            Snackbar.Add($"Error removing subcategory: {Subcategory.Description}", Severity.Error);
        }
    }
}