﻿using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Services
{
    public interface IBudgetsService
    {
        Task<List<Budget>?> GetAllBudgets();

        Task<int> AddBudget(Budget budget);

        Task<Budget?> GetBudget(int id);
        Task<bool> DeleteBudget(int id);
    }
}