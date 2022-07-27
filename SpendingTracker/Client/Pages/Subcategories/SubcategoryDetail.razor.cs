using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Subcategories;

public partial class SubcategoryDetail
{
    [Inject] public ISubcategoriesService SubcategoriesService { get; set; }
    [Parameter] public string Id { get; set; }

    public bool IsLoading { get; set; } = false;
    public Subcategory Subcategory { get; set; } = new Subcategory();

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Subcategory = await SubcategoriesService.GetSubcategory(int.Parse(Id));
        IsLoading = false;
    }   
}