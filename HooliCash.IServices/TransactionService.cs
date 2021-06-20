using HooliCash.DTOs.Transaction;
using System;
using System.Collections.Generic;

namespace HooliCash.IServices
{
    public interface ITransactionService
    {
        TransactionDto CreateTransaction(CreateTransactionDto createTransactionDto);
        IEnumerable<TransactionDto> GetTransactions(Guid userId);
        TransactionDto GetTransaction(Guid transactionId);
        TransactionDto UpdateTransaction(UpdateTransactionDto updateTransactionDto);
        bool DeleteTransaction(Guid transactionId);
    }
}
