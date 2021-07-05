using HooliCash.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.IO;

namespace HooliCash.IServices
{
    public interface ITransactionService
    {
        TransactionDto CreateTransaction(CreateTransactionDto createTransactionDto);
        IEnumerable<TransactionDto> GetTransactions(TransactionQuery userId);
        TransactionDto GetTransaction(Guid transactionId);
        TransactionDto UpdateTransaction(UpdateTransactionDto updateTransactionDto);
        bool DeleteTransaction(Guid transactionId);
        IEnumerable<TransactionDto> ImportTransactions(StreamReader stream, Guid userId);
    }
}
