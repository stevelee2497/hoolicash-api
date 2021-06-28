using AutoMapper;
using HooliCash.Core.Models;
using HooliCash.DTOs.Category;
using HooliCash.DTOs.Transaction;
using HooliCash.DTOs.User;
using HooliCash.DTOs.Wallet;
using System.Linq;

namespace HooliCash.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDto>();

            CreateMap<Category, CategoryDto>()
                .ForMember(destination => destination.TransactionCount, map => map.MapFrom(source => source.Transactions.Count));

            CreateMap<Wallet, WalletDto>()
                .ForMember(destination => destination.TransactionCount, map => map.MapFrom(source => source.Transactions.Count));

            CreateMap<User, UserDto>()
                .ForMember(destination => destination.WalletCount, map => map.MapFrom(source => source.Wallets.Count))
                .ForMember(destination => destination.TransactionCount, map => map.MapFrom(source => source.Wallets.SelectMany(x => x.Transactions).Count()));
        }
    }
}
