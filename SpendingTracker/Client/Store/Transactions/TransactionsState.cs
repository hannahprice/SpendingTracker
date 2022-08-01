using Fluxor;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Transactions;

[FeatureState]
public class TransactionsState
{
    public bool IsLoading { get; }
    public IEnumerable<Transaction>? Transactions { get; }
    
    private TransactionsState() { }

    public TransactionsState(bool isLoading, IEnumerable<Transaction>? transactions)
    {
        IsLoading = isLoading;
        Transactions = transactions ?? Array.Empty<Transaction>();
    }
}