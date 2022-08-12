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
        var jsApiService = new Mock<IJsApiService>();
        
        Services.AddSingleton(_categoriesService.Object);
        Services.AddSingleton(jsApiService.Object);
    }

    [Fact]
    public void WhenCategoriesAvailable_DisplaysCategories()
    {
        // Arrange
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(GetCategoriesWithNoSubcategories());
        
        // Act
        var component = RenderComponent();

        // Assert
        var chipset = component.FindAll(".mud-chip-content");
        chipset.Count.Should().Be(1);
    }

    [Fact]
    public void WhenNoCategorySelected_DoesNotDisplaySubcategories()
    {
        // Arrange
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(GetCategoriesAndSubcategories());
        
        // Act
        var component = RenderComponent();
        
        // Assert
        var chipsets = component.FindAll(".mud-chipset");
        var subcategoryChipset = chipsets[1];
        subcategoryChipset.MarkupMatches(@"<div class=""mud-chipset""></div>");
    }

    [Fact]
    public void WhenCategoryClicked_SelectsCategory()
    {
        // Arrange
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(GetCategoriesAndSubcategories());
        var callbackCalled = false;
        var component = RenderComponent<CategorySelection>(parameters => parameters
            .Add(p => p.SelectedCategory, new Category())
            .Add(p => p.SelectedSubcategories, new List<Subcategory>())
            .Add(p => p.SelectedCategoryChanged, () => { callbackCalled = true; })
            .Add(p => p.SelectedSubcategoriesChanged, () => { })
        );

        var chip = component.Find(".mud-chip");

        // Act
        chip.Click();

        // Assert
        chip.ClassList.Contains("mud-chip-selected");
        callbackCalled.Should().BeTrue();
        component.Instance.SelectedCategory!.Description.Should().Be("Bills");
    }

    [Fact]
    public void WhenCategoryClickedSecondTime_UnselectsCategory()
    {
        // Arrange
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(GetCategoriesAndSubcategories());
        var callbackCalledCount = 0;
        var component = RenderComponent<CategorySelection>(parameters => parameters
            .Add(p => p.SelectedCategory, new Category())
            .Add(p => p.SelectedSubcategories, new List<Subcategory>())
            .Add(p => p.SelectedCategoryChanged, () => { callbackCalledCount++; })
            .Add(p => p.SelectedSubcategoriesChanged, () => { })
        );

        var chip = component.Find(".mud-chip");

        // Act
        chip.Click();
        chip.Click();
        
        // Assert
        chip.ClassList.Should().NotContain("mud-chip-selected");
        callbackCalledCount.Should().Be(2);
        component.Instance.SelectedCategory!.Should().BeNull();
    }

    [Fact]
    public void WhenCategoryClicked_DisplaysSubcategories()
    {
        // Arrange
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(GetCategoriesAndSubcategories());
        var component = RenderComponent<CategorySelection>(parameters => parameters
            .Add(p => p.SelectedCategory, new Category())
            .Add(p => p.SelectedSubcategories, new List<Subcategory>())
            .Add(p => p.SelectedCategoryChanged, () => { })
            .Add(p => p.SelectedSubcategoriesChanged, () => { })
        );

        var chip = component.Find(".mud-chip");

        // Act
        chip.Click();
        
        // Assert
        var chips = component.FindAll(".mud-chip-content");
        var subcategoryChip = chips[1];
        subcategoryChip.InnerHtml.Should().Be("Council tax");
    }

    [Fact]
    public void WhenSecondCategoryClicked_ClearsSelectedSubcategory()
    {
        // Arrange
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(GetMultipleCategoriesAndSubcategories());
        var component = RenderComponent<CategorySelection>(parameters => parameters
            .Add(p => p.SelectedCategory, new Category())
            .Add(p => p.SelectedSubcategories, new List<Subcategory>())
            .Add(p => p.SelectedCategoryChanged, () => { })
            .Add(p => p.SelectedSubcategoriesChanged, () => { })
        );

        var categoryChips = component.FindAll(".mud-chip");
        categoryChips.EnableAutoRefresh = true;
        
        categoryChips[0].Click();
        categoryChips[2].Click();

        // Act
        categoryChips[1].Click();

        // Assert
        component.Instance.SelectedSubcategories.Should().BeEmpty();
        categoryChips[2].ClassList.Should().NotContain("mud-chip-selected");
    }

    [Fact]
    public void WhenSubcategoryClicked_SelectsSubcategory()
    {
        // Arrange
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(GetCategoriesAndSubcategories());
        bool callbackCalled = false;
        var component = RenderComponent<CategorySelection>(parameters => parameters
            .Add(p => p.SelectedCategory, new Category())
            .Add(p => p.SelectedSubcategories, new List<Subcategory>())
            .Add(p => p.SelectedCategoryChanged, () => { })
            .Add(p => p.SelectedSubcategoriesChanged, () => { callbackCalled = true;})
        );

        var categoryChips = component.FindAll(".mud-chip");
        categoryChips.EnableAutoRefresh = true;
        
        categoryChips[0].Click();
        
        // Act
        categoryChips[1].Click();
        
        // Assert
        component.Instance.SelectedSubcategories.First().Description.Should().Be("Council tax");
        categoryChips[1].ClassList.Contains("mud-chip-selected");
        callbackCalled.Should().BeTrue();
    }

    [Fact]
    public void WhenCategoryClicked_EmitsCategoryAndSubcategoryChangedCallbacks()
    {
        // Arrange
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(GetCategoriesAndSubcategories());
        bool categoryCallbackCalled = false;
        bool subcategoryCallbackCalled = false;
        var component = RenderComponent<CategorySelection>(parameters => parameters
            .Add(p => p.SelectedCategory, new Category())
            .Add(p => p.SelectedSubcategories, new List<Subcategory>())
            .Add(p => p.SelectedCategoryChanged, () => { categoryCallbackCalled = true; })
            .Add(p => p.SelectedSubcategoriesChanged, () => { subcategoryCallbackCalled = true;})
        );
        
        var categoryChips = component.FindAll(".mud-chip");
        
        // Act
        categoryChips[0].Click();
        
        // Assert
        categoryCallbackCalled.Should().BeTrue();
        subcategoryCallbackCalled.Should().BeTrue();
    }

    [Fact]
    public void WhenSubcategoryClicked_EmitsSubcategoryChangedCallback()
    {
        // Arrange
        _categoriesService.Setup(x => x.GetAllCategories()).ReturnsAsync(GetCategoriesAndSubcategories());
        bool categoryCallbackCalled = false;
        bool subcategoryCallbackCalled = false;
        var component = RenderComponent<CategorySelection>(parameters => parameters
            .Add(p => p.SelectedCategory, new Category())
            .Add(p => p.SelectedSubcategories, new List<Subcategory>())
            .Add(p => p.SelectedCategoryChanged, () => { categoryCallbackCalled = true; })
            .Add(p => p.SelectedSubcategoriesChanged, () => { subcategoryCallbackCalled = true;})
        );
        
        var categoryChips = component.FindAll(".mud-chip");
        categoryChips.EnableAutoRefresh = true;
        categoryChips[0].Click();
        categoryCallbackCalled = false;

        // Act
        categoryChips[1].Click();
        
        // Assert
        categoryCallbackCalled.Should().BeFalse();
        subcategoryCallbackCalled.Should().BeTrue();
    }

    #region TestHelpers

        private List<Category> GetCategoriesWithNoSubcategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1, Description = "Bills"
                }
            };
        }
    
        private List<Category> GetCategoriesAndSubcategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1, Description = "Bills",
                    Subcategories = new List<Subcategory> { new Subcategory{Id = 1, Description = "Council tax", CategoryId = 1}}
                }
            };
        }

        private List<Category> GetMultipleCategoriesAndSubcategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1, Description = "Bills",
                    Subcategories = new List<Subcategory> { new Subcategory{Id = 1, Description = "Council tax", CategoryId = 1}}
                },
                new Category
                {
                    Id = 2, Description = "Shopping",
                    Subcategories = new List<Subcategory> { new Subcategory{Id = 2, Description = "Groceries", CategoryId = 2}}
                }
            };   
        }

        private IRenderedComponent<CategorySelection> RenderComponent()
        {
            return RenderComponent<CategorySelection>(parameters => parameters
                .Add(p => p.SelectedCategory, new Category())
                .Add(p => p.SelectedSubcategories, new List<Subcategory>())
                .Add(p => p.SelectedCategoryChanged, () => { })
                .Add(p => p.SelectedSubcategoriesChanged, () => { })
            );
        }

        #endregion
}