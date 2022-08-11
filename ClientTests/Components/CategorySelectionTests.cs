using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor;
using SpendingTracker.Client.Components;
using SpendingTracker.Client.Services;
using SpendingTracker.Shared.Models;

namespace ClientTests.Components;

public class CategorySelectionTests : TestContext
{
    private readonly Mock<ICategoriesService> _categoriesService;

    public CategorySelectionTests()
    {
        _categoriesService = new Mock<ICategoriesService>();

        Services.AddSingleton(_categoriesService.Object);
        
        ComponentFactories.AddStub<MudChipSet>();
        ComponentFactories.AddStub<MudChip>();
    }

    [Fact]
    public void RendersSuccess()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category
            {
                Id = 1, Description = "Bills",
                Subcategories = new List<Subcategory> { new Subcategory{Id = 1, Description = "Council tax", CategoryId = 1}}
            }
        };
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(categories);
        
        // Act
        var component = RenderComponent<CategorySelection>(parameters => parameters
            .Add(p => p.SelectedCategory, new Category())
            .Add(p => p.SelectedSubcategories, new List<Subcategory>())
            .Add(p => p.SelectedCategoryChanged, () => { })
            .Add(p => p.SelectedSubcategoriesChanged, () => { })
        );

        // Assert
        var text = component.Find("#CategorySelection-Header");
        text.InnerHtml.Should().Be("Categories");
    }
}