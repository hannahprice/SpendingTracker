using Fluxor;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Store.Transactions.Reducers;

public class LoadTransactionsActionsReducers
{
    [ReducerMethod(typeof(LoadTransactionsAction))]
    public static TransactionsState ReduceLoadTransactionsAction(TransactionsState state)
        => new TransactionsState(isLoading: true, transactions: null, transactionDetail: state.TransactionDetail, multiAddEnabled: state.MultiAddEnabled);

    [ReducerMethod]
    public static TransactionsState ReduceLoadTransactionsResultAction(TransactionsState state,
        LoadTransactionsResultAction action) =>
        new TransactionsState(isLoading: false, transactions: action.Transactions, transactionDetail: state.TransactionDetail, multiAddEnabled: state.MultiAddEnabled);
}