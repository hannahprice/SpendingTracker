using Bunit;
using FluentAssertions;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor;
using MudBlazor.Services;
using SpendingTracker.Client.Components;
using SpendingTracker.Client.Pages.Budgets;
using SpendingTracker.Client.Store.Budgets;
using SpendingTracker.Shared.Models;

namespace ClientTests;

public class AddBudgetTests : TestContext
{
    private Mock<IState<BudgetsState>> BudgetsState;
    private Mock<IDispatcher> Dispatcher;
    private Mock<IActionSubscriber> ActionSubscriber;
    
    public AddBudgetTests()
    {
        BudgetsState = new Mock<IState<BudgetsState>>();
        Dispatcher = new Mock<IDispatcher>();
        ActionSubscriber = new Mock<IActionSubscriber>();
        
        BudgetsState.Setup(x => x.Value).Returns(new BudgetsState(isLoading: false, budgets: new List<Budget>(),
            budgetDetail: null, multiAddEnabled: false));
        
        Services.AddSingleton(BudgetsState.Object);
        Services.AddSingleton(Dispatcher.Object);
        Services.AddSingleton(ActionSubscriber.Object);

        ComponentFactories.AddStub<MudNumericField<decimal>>();
        ComponentFactories.AddStub<MudSelect<Frequency>>();
        ComponentFactories.AddStub<CategorySelection>();
    }
    
    [Fact]
    public void AddBudgetSampleTest()
    {
        // Act
        var component = RenderComponent<AddBudget>();
        
        // Assert
        var header = component.Find("#AddBudget-Header");
        header.TextContent.Should().Be("Add new budget");
    }
}