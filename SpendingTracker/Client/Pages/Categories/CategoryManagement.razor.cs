using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;
using static MudBlazor.CategoryTypes;

namespace SpendingTracker.Client.Pages.Categories
{
    public partial class CategoryManagement
    {
        [Inject]
        public ICategoriesService CategoriesService { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public bool IsLoading { get; set; } = false;

        //private TableGroupDefinition<Element> _groupDefinition = new ()
        //{
        //    GroupName = "Group",
        //    Indentation = false,
        //    Expandable = true,
        //    IsInitiallyExpanded = false,
        //    Selector = (e) => e.Group
        //};

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            Categories = await CategoriesService.GetAllCategories();

            IsLoading = false;
        }
    }
}
