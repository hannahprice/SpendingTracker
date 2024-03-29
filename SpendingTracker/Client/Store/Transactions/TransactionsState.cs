﻿using Fluxor;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Client.Store.Transactions;

[FeatureState]
public class TransactionsState
{
    public bool IsLoading { get; }
    public IEnumerable<Transaction>? Transactions { get; }
    public Transaction? TransactionDetail { get; }
    public bool MultiAddEnabled { get; }

    private TransactionsState() { }

    public TransactionsState(bool isLoading, IEnumerable<Transaction>? transactions, Transaction? transactionDetail, bool multiAddEnabled)
    {
        IsLoading = isLoading;
        Transactions = transactions ?? Array.Empty<Transaction>();
        TransactionDetail = transactionDetail;
        MultiAddEnabled = multiAddEnabled;
    }
}