using Fluxor;
using SpendingTracker.Client.Store.Transactions.Actions;

namespace SpendingTracker.Client.Store.Transactions.Reducers;

public static class DeleteTransactionActionsReducers
{
    [ReducerMethod(typeof(DeleteTransactionAction))]
    public static TransactionsState ReduceDeleteTransactionAction(TransactionsState state)
        => new TransactionsState(isLoading: true, transactions: state.Transactions, transactionDetail: null);

    [ReducerMethod]
    public static TransactionsState ReduceDeleteTransactionResultAction(TransactionsState state,
        DeleteTransactionResultAction action)
    {
        if (action.Success)
        {
            var transactions = state.Transactions!.Where(x => x.Id != action.Id);   
            return new TransactionsState(isLoading: false, transactions: transactions, transactionDetail: null);
        }

        return new TransactionsState(isLoading: false, transactions: state.Transactions, transactionDetail: state.TransactionDetail);
    }
}