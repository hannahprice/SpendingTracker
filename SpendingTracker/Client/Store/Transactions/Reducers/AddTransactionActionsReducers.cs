using Fluxor;
using SpendingTracker.Client.Store.Transactions.Actions;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Transactions.Reducers;

public static class AddTransactionActionsReducers
{
    [ReducerMethod(typeof(AddTransactionAction))]
    public static TransactionsState ReduceAddTransactionAction(TransactionsState state)
        => new TransactionsState(isLoading: true, transactions: state.Transactions,
            transactionDetail: state.TransactionDetail, multiAddEnabled: state.MultiAddEnabled);

    [ReducerMethod]
    public static TransactionsState ReduceAddTransactionResultAction(TransactionsState state,
        AddTransactionResultAction action)
    {
        var transactions = new List<Transaction>();
        transactions.AddRange(state.Transactions!);
        transactions.Add(action.Transaction!);
        return new TransactionsState(isLoading: false, transactions: transactions, transactionDetail: state.TransactionDetail, multiAddEnabled: state.MultiAddEnabled);
    }
    
    [ReducerMethod(typeof(ToggleMultiAddAction))]
    public static TransactionsState ReduceToggleMultiAddAction(TransactionsState state) =>
        new TransactionsState(isLoading: state.IsLoading, transactions: state.Transactions, transactionDetail: state.TransactionDetail,
            multiAddEnabled: !state.MultiAddEnabled);
}