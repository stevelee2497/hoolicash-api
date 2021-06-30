using AutoMapper;
using HooliCash.Core.Models;
using HooliCash.DTOs.Transaction;
using HooliCash.IRepositories;
using HooliCash.IServices;
using HooliCash.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HooliCash.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public TransactionDto CreateTransaction(CreateTransactionDto createTransactionDto)
        {
            var model = new Transaction
            {
                Title = createTransactionDto.Title,
                Detail = createTransactionDto.Detail,
                Amount = createTransactionDto.Amount,
                TransactionType = Enum.Parse<TransactionType>(createTransactionDto.TransactionType),
                WalletId = createTransactionDto.WalletId,
                CategoryId = createTransactionDto.CategoryId,
                TransactionDate = createTransactionDto.TransactionDate
            };
            _unitOfWork.Transactions.Add(model);
            _unitOfWork.Complete();
            return _mapper.Map<TransactionDto>(model);
        }

        public IEnumerable<TransactionDto> GetTransactions(Guid userId)
        {
            return _unitOfWork.Transactions.All().Select(_mapper.Map<TransactionDto>);
        }

        public TransactionDto GetTransaction(Guid transactionId)
        {
            var model = _unitOfWork.Transactions.Find(transactionId);
            return _mapper.Map<TransactionDto>(model);
        }

        public TransactionDto UpdateTransaction(UpdateTransactionDto dto)
        {
            var model = _unitOfWork.Transactions.Find(dto.Id);
            model.Title = dto.Title;
            model.Detail = dto.Detail;
            model.Amount = dto.Amount;
            model.TransactionType = Enum.Parse<TransactionType>(dto.TransactionType);
            _unitOfWork.Transactions.Update(model);
            _unitOfWork.Complete();
            return _mapper.Map<TransactionDto>(model);
        }

        public bool DeleteTransaction(Guid id)
        {
            var model = _unitOfWork.Transactions.Find(id);
            _unitOfWork.Transactions.Remove(model);
            _unitOfWork.Complete();
            return true;
        }
    }
}
