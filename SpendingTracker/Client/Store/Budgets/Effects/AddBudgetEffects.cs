﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SpendingTracker.Client.Services;
using SpendingTracker.Client.Store.Budgets.Actions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Budgets.Effects;

public class AddBudgetEffects
{
    private readonly IBudgetsService BudgetsService;
    private readonly ISnackbar Snackbar;
    private readonly NavigationManager NavigationManager;
    private readonly IState<BudgetsState> BudgetsState;

    public AddBudgetEffects(IBudgetsService budgetsService, ISnackbar snackbar, NavigationManager navigationManager, IState<BudgetsState> budgetsState)
    {
        BudgetsService = budgetsService;
        Snackbar = snackbar;
        NavigationManager = navigationManager;
        BudgetsState = budgetsState;
    }
    
    [EffectMethod]
    public async Task HandleAddBudgetAction(AddBudgetAction action, IDispatcher dispatcher)
    {
        var createdId = await BudgetsService.AddBudget(action.Budget);
        var addedBudget = await BudgetsService.GetBudget(createdId);
        dispatcher.Dispatch(new AddBudgetResultAction(createdId > 0, addedBudget));
    }

    [EffectMethod]
    public Task HandleAddBudgetResultAction(AddBudgetResultAction action, IDispatcher dispatcher)
    {
        if (action.Success)
        {
            Snackbar.Add($"Budget added", Severity.Success);
        }
        else
        {
            Snackbar.Add($"Error adding budget", Severity.Error);
        }

        if (!BudgetsState.Value.MultiAddEnabled)
        {
            NavigationManager.NavigateTo("/budgets", false);
        }
        
        return Task.CompletedTask;
    }
}