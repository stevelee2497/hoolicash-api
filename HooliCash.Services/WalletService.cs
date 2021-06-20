using AutoMapper;
using HooliCash.Core.Models;
using HooliCash.DTOs.Wallet;
using HooliCash.IRepositories;
using HooliCash.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HooliCash.Services
{
    public class WalletService : IWalletService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Wallet> _walletRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public WalletService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _walletRepository = unitOfWork.Repository<Wallet>();
            _transactionRepository = unitOfWork.Repository<Transaction>();
            _mapper = mapper;
        }

        public WalletDto CreateWallet(CreateWalletDto createWalletDto)
        {
            var model = new Wallet
            {
                Name = createWalletDto.Name,
                IconUrl = createWalletDto.IconUrl,
                User = new User { Id = createWalletDto.UserId }
            };
            _walletRepository.Add(model);
            _unitOfWork.Complete();
            return _mapper.Map<WalletDto>(model);
        }

        public IEnumerable<WalletDto> GetWallets(Guid userId)
        {
            return _walletRepository.All().Select(_mapper.Map<WalletDto>);
        }

        public WalletDto GetWallet(Guid walletId)
        {
            var model = _walletRepository.Find(walletId);
            return _mapper.Map<WalletDto>(model);
        }

        public WalletDto UpdateWallet(UpdateWalletDto dto)
        {
            var model = _walletRepository.Find(dto.Id);
            model.Name = dto.Name;
            model.IconUrl = dto.IconUrl;
            _walletRepository.Update(model);
            _unitOfWork.Complete();
            return _mapper.Map<WalletDto>(model);
        }

        public bool DeleteWallet(Guid id)
        {
            var model = _walletRepository.Find(id);
            _walletRepository.Remove(model);
            var transactions = _transactionRepository.Where(x => x.Wallet.Id == model.Id);
            _transactionRepository.RemoveRange(transactions);
            _unitOfWork.Complete();
            return true;
        }
    }
}
