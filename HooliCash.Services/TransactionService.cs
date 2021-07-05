using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using HooliCash.Core.Models;
using HooliCash.DTOs.Transaction;
using HooliCash.IRepositories;
using HooliCash.IServices;
using HooliCash.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HooliCash.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWalletRepository _walletRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _walletRepository = _unitOfWork.Wallets;
            _categoryRepository = _unitOfWork.Categories;
            _transactionRepository = _unitOfWork.Transactions;
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
            _transactionRepository.Add(model);
            _unitOfWork.Complete();
            return _mapper.Map<TransactionDto>(model);
        }

        public IEnumerable<TransactionDto> GetTransactions(TransactionQuery transactionQuery)
        {
            var transactions = _transactionRepository.All();
            if (transactionQuery.UserId != null)
            {
                transactions = transactions.Where(x => x.Wallet.UserId == transactionQuery.UserId);
            }

            if (transactionQuery.WalletId != null)
            {
                transactions = transactions.Where(x => x.WalletId == transactionQuery.WalletId);
            }

            if (transactionQuery.From != null)
            {
                transactions = transactions.Where(x => x.TransactionDate >= transactionQuery.From);
            }

            if (transactionQuery.To != null)
            {
                transactions = transactions.Where(x => x.TransactionDate <= transactionQuery.To);
            }

            return transactions.Select(_mapper.Map<TransactionDto>);
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
            _unitOfWork.Complete();
            return _mapper.Map<TransactionDto>(model);
        }

        public bool DeleteTransaction(Guid id)
        {
            var model = _unitOfWork.Transactions.Find(id);
            _transactionRepository.Remove(model);
            _unitOfWork.Complete();
            return true;
        }

        public IEnumerable<TransactionDto> ImportTransactions(StreamReader stream, Guid userId)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter = ";",
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim
            };
            using var csv = new CsvReader(stream, config);
            var transactions = csv.GetRecords<ImportTransactionDto>();
            var result = new List<TransactionDto>();

            using (var dbContextTransaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var transaction in transactions)
                    {
                        var category = _categoryRepository.FirstOrDefault(x => x.Name == transaction.Category);
                        if (category == null)
                        {
                            category = _categoryRepository.Add(new Category
                            {
                                Name = transaction.Category,
                                TransactionType = transaction.Amount <= 0 ? TransactionType.Expense : TransactionType.Income
                            });
                        }

                        var wallet = _walletRepository.FirstOrDefault(x => x.Name == transaction.Wallet);
                        if (wallet == null)
                        {
                            wallet = _walletRepository.Add(new Wallet
                            {
                                Name = transaction.Wallet,
                                UserId = userId
                            });
                        }

                        var transactionEntity = _transactionRepository.Add(new Transaction
                        {
                            Title = transaction.Category,
                            Detail = transaction.Note,
                            Amount = transaction.Amount,
                            TransactionType = transaction.Amount <= 0 ? TransactionType.Expense : TransactionType.Income,
                            TransactionDate = DateTimeOffset.ParseExact(transaction.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            CategoryId = category.Id,
                            WalletId = wallet.Id
                        });
                        _unitOfWork.Complete();

                        result.Add(_mapper.Map<TransactionDto>(transactionEntity));
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
    }
}
