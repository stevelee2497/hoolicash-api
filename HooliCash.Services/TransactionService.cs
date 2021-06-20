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
        private readonly IRepository<Transaction> _transactionRepository;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = unitOfWork.Repository<Transaction>();
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
                Wallet = new Wallet { Id = createTransactionDto.WalletId },
                Category = new Category { Id = createTransactionDto.CategoryId },
            };
            _transactionRepository.Add(model);
            _unitOfWork.Complete();
            return _mapper.Map<TransactionDto>(model);
        }

        public IEnumerable<TransactionDto> GetTransactions(Guid userId)
        {
            return _transactionRepository.All().Select(_mapper.Map<TransactionDto>);
        }

        public TransactionDto GetTransaction(Guid transactionId)
        {
            var model = _transactionRepository.Find(transactionId);
            return _mapper.Map<TransactionDto>(model);
        }

        public TransactionDto UpdateTransaction(UpdateTransactionDto dto)
        {
            var model = _transactionRepository.Find(dto.Id);
            model.Title = dto.Title;
            model.Detail = dto.Detail;
            model.Amount = dto.Amount;
            model.TransactionType = Enum.Parse<TransactionType>(dto.TransactionType);
            _transactionRepository.Update(model);
            return _mapper.Map<TransactionDto>(model);
        }

        public bool DeleteTransaction(Guid id)
        {
            var model = _transactionRepository.Find(id);
            _transactionRepository.Remove(model);
            _unitOfWork.Complete();
            return true;
        }
    }
}
