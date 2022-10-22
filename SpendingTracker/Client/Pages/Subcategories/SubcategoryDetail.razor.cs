using System.Globalization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Subcategories;

public partial class SubcategoryDetail
{
    [Parameter] public string? Id { get; set; }
    [Inject] private ISubcategoriesService SubcategoriesService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    private bool DialogVisible { get; set; }
    private void ToggleDialog() => DialogVisible = !DialogVisible;

    private Subcategory? Subcategory { get; set; } = new Subcategory();

    protected override async Task OnInitializedAsync()
    {
        if (Id != null)
        {
            Subcategory = await SubcategoriesService.GetSubcategory(int.Parse(Id, CultureInfo.InvariantCulture));
        }
    }   
    private async Task DeleteSubcategory()
    {
        try
        {
            if (Id != null)
            {
                await SubcategoriesService.DeleteSubcategory(int.Parse(Id, CultureInfo.InvariantCulture));
                ToggleDialog();
                Snackbar.Add($"Subcategory removed", Severity.Success);
                
                NavigationManager.NavigateTo("/categories", false);
            }
        }
        catch
        {
            Snackbar.Add($"Error removing subcategory: {Subcategory!.Description}", Severity.Error);
        }
    }
}