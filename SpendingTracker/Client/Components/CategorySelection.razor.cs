using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Components;

public partial class CategorySelection
{
    [Parameter] public Category? SelectedCategory { get; set; }
    [Parameter] public EventCallback<Category> SelectedCategoryChanged { get; set; }
    [Parameter] public List<Subcategory> SelectedSubcategories { get; set; }
    [Parameter] public EventCallback<List<Subcategory>> SelectedSubcategoriesChanged { get; set; }
    [Inject] public ICategoriesService CategoriesService { get; set; }
    private bool IsLoading { get; set; } = false;
    private List<Category> AvailableCategories { get; set; } = new List<Category>();
    private List<Subcategory> AvailableSubcategories { get; set; } = new List<Subcategory>();
    private MudChip? SelectedCategoryChip { get; set; } 
    private MudChip[]? SelectedSubcategoryChips { get; set; }
    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        AvailableCategories = await CategoriesService.GetAllCategories();
        IsLoading = false;
    }
    
    private async Task CategoryClicked()
    {
        SelectedCategory = GetSelectedCategory();
        AvailableSubcategories = SelectedCategory is null ? new List<Subcategory>() : SelectedCategory.Subcategories;

        SelectedSubcategories.Clear();
        SelectedSubcategoryChips = Array.Empty<MudChip>();
        
        await SelectedCategoryChanged.InvokeAsync(SelectedCategory);
        await SelectedSubcategoriesChanged.InvokeAsync(SelectedSubcategories);
    }

    private async Task SubcategoryClicked()
    {
        SelectedSubcategories = GetSelectedSubcategories();
        await SelectedSubcategoriesChanged.InvokeAsync(SelectedSubcategories);
    }

    private Category? GetSelectedCategory()
    {
        var selectedCategoryDescription = SelectedCategoryChip?.Text;
        return AvailableCategories.SingleOrDefault(x => selectedCategoryDescription == x.Description);
    }
    
    private List<Subcategory> GetSelectedSubcategories()
    {
        if (SelectedSubcategoryChips != null && SelectedSubcategoryChips.Length > 0)
        {
            var selectedSubcategoryDescriptions = SelectedSubcategoryChips?.Select(c => c.Text).ToList();
            return AvailableSubcategories.Where(x => selectedSubcategoryDescriptions.Contains(x.Description)).ToList();   
        }
        return new List<Subcategory>(0);
    }
}