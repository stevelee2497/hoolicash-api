using AutoMapper;
using HooliCash.Core.Models;
using HooliCash.DTOs.Category;
using HooliCash.DTOs.Transaction;
using HooliCash.DTOs.User;
using HooliCash.DTOs.Wallet;

namespace HooliCash.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDto>();

            CreateMap<Category, CategoryDto>()
                .ForMember(destination => destination.Transactions, map => map.MapFrom(source => source.Transactions.Count));

            CreateMap<Wallet, WalletDto>();

            CreateMap<User, UserDto>();
        }
    }
}
