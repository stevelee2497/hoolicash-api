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

        public WalletService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public WalletDto CreateWallet(CreateWalletDto createWalletDto)
        {
            var model = new Wallet
            {
                Name = createWalletDto.Name,
                IconUrl = createWalletDto.IconUrl,
                UserId = createWalletDto.UserId
            };
            _unitOfWork.Wallets.Add(model);
            _unitOfWork.Complete();
            return _mapper.Map<WalletDto>(model);
        }

        public IEnumerable<WalletDto> GetWallets(Guid userId)
        {
            var wallets = _unitOfWork.Wallets.All().Select(_mapper.Map<WalletDto>);
            var total = new WalletDto
            {
                Balance = wallets.Sum(x => x.Balance),
                TransactionCount = wallets.Sum(x => x.TransactionCount),
                Name = "Total"
            };
            return Enumerable.Empty<WalletDto>().Append(total).Concat(wallets);
        }

        public WalletDto GetWallet(Guid walletId)
        {
            var model = _unitOfWork.Wallets.Find(walletId);
            return _mapper.Map<WalletDto>(model);
        }

        public WalletDto UpdateWallet(UpdateWalletDto dto)
        {
            var model = _unitOfWork.Wallets.Find(dto.Id);
            model.Name = dto.Name;
            model.IconUrl = dto.IconUrl;
            _unitOfWork.Wallets.Update(model);
            _unitOfWork.Complete();
            return _mapper.Map<WalletDto>(model);
        }

        public bool DeleteWallet(Guid id)
        {
            var model = _unitOfWork.Wallets.Find(id);
            _unitOfWork.Wallets.Remove(model);
            var transactions = _unitOfWork.Transactions.Where(x => x.Wallet.Id == model.Id);
            _unitOfWork.Transactions.RemoveRange(transactions);
            _unitOfWork.Complete();
            return true;
        }
    }
}
