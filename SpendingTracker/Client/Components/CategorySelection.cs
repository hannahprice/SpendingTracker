using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Components;

public partial class CategorySelection
{
    public bool IsLoading { get; set; } = false;
    public List<Category> AvailableCategories { get; set; } = new List<Category>();
    public List<Subcategory> AvailableSubcategories { get; set; } = new List<Subcategory>();
    public MudChip[]? SelectedCategoryChips { get; set; } 
    public MudChip[]? SelectedSubcategoryChips { get; set; }
    
    [Parameter]
    public List<Category> SelectedCategories { get; set; }
    
    [Parameter]
    public EventCallback<List<Category>> SelectedCategoriesChanged { get; set; }
    
    [Parameter]
    public List<Subcategory> SelectedSubcategories { get; set; }
    
    [Parameter]
    public EventCallback<List<Subcategory>> SelectedSubcategoriesChanged { get; set; }

    [Inject]
    public ICategoriesService CategoriesService { get; set; }

    public string Test { get; set; } = null;
    
    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        AvailableCategories = await CategoriesService.GetAllCategories();
        IsLoading = false;
    }
    
    private async Task CategoryClicked()
    {
        var selectedCategoryDescriptions = SelectedCategoryChips?.Select(c => c.Text).ToList();
        var selectedCategories = AvailableCategories.Where(x => selectedCategoryDescriptions.Contains(x.Description)).ToList();
            
        AvailableSubcategories = selectedCategories.SelectMany(x => x.Subcategories).ToList();

        SelectedCategories = GetSelectedCategories();
        SelectedSubcategories = GetSelectedSubcategories();
        
        await SelectedCategoriesChanged.InvokeAsync(SelectedCategories);
        await SelectedSubcategoriesChanged.InvokeAsync(SelectedSubcategories);
    }

    private async Task SubcategoryClicked()
    {
        SelectedSubcategories = GetSelectedSubcategories();
        await SelectedSubcategoriesChanged.InvokeAsync(SelectedSubcategories);
    }

    public List<Category> GetSelectedCategories()
    {
        var selectedCategoryDescriptions = SelectedCategoryChips?.Select(c => c.Text).ToList();
        return AvailableCategories.Where(x => selectedCategoryDescriptions.Contains(x.Description)).ToList();
    }
    
    private List<Subcategory> GetSelectedSubcategories()
    {
        if (SelectedSubcategoryChips != null && SelectedSubcategoryChips.Length > 0)
        {
            var selectedSubcategoryDescriptions = SelectedSubcategoryChips?.Select(c => c.Text).ToList();
            Test = string.Join(',', selectedSubcategoryDescriptions);

            return AvailableSubcategories.Where(x => selectedSubcategoryDescriptions.Contains(x.Description)).ToList();   
        }
        return new List<Subcategory>(0);
    }
}