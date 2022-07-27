using Microsoft.AspNetCore.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Pages.Categories;

public partial class CategoryDetail
{
    [Inject] public ICategoriesService CategoriesService { get; set; }
    [Parameter] public string Id { get; set; }

    public bool IsLoading { get; set; } = false;
    public Category Category { get; set; } = new Category();

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Category = await CategoriesService.GetCategory(int.Parse(Id));
        IsLoading = false;
    }

}