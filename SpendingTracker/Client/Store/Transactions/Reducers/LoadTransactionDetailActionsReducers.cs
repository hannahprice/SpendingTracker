using Fluxor;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Store.Transactions.Reducers;

public static class LoadTransactionDetailActionsReducers
{
    [ReducerMethod(typeof(LoadTransactionDetailAction))]
    public static TransactionsState ReduceLoadTransactionDetailAction(TransactionsState state) =>
        new TransactionsState(isLoading: true, transactions: state.Transactions, transactionDetail: null, multiAddEnabled: state.MultiAddEnabled);

    [ReducerMethod]
    public static TransactionsState ReduceLoadTransactionDetailResultAction(TransactionsState state,
        LoadTransactionDetailResultAction action) =>
        new TransactionsState(isLoading: false, transactions: state.Transactions,
            transactionDetail: action.Transaction, multiAddEnabled: state.MultiAddEnabled);
}