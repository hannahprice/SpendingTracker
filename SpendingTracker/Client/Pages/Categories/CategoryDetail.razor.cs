﻿using System.Globalization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Categories;

public partial class CategoryDetail
{
    [Parameter] public string? Id { get; set; }
    [Inject] private ICategoriesService CategoriesService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    private bool DialogVisible { get; set; }
    private bool IsLoading { get; set; }
    private Category? Category { get; set; } = new Category();
    private void ToggleDialog() => DialogVisible = !DialogVisible;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        if (Id != null)
        {
            Category = await CategoriesService.GetCategory(int.Parse(Id, CultureInfo.InvariantCulture));
        }
        IsLoading = false;
    }
    
    private async Task DeleteCategory()
    {
        try
        {
            if (Id != null)
            {
                await CategoriesService.DeleteCategory(int.Parse(Id, CultureInfo.InvariantCulture));
                ToggleDialog();
                Snackbar.Add($"Category removed", Severity.Success);
                
                NavigationManager.NavigateTo("/categories", false);
            }
        }
        catch
        {
            Snackbar.Add($"Error removing category: {Category!.Description}", Severity.Error);
        }
    }
}