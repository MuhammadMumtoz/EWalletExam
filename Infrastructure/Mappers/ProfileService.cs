namespace Infrastructure.Mappers;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

public class ProfileService : Profile {
    public ProfileService(){
        CreateMap<GetUserDto, User>().ReverseMap();
        CreateMap<GetUserDto, AddUserDto>().ReverseMap();
        CreateMap<AddUserDto, User>().ReverseMap();
        CreateMap<GetWalletDto, Wallet>().ReverseMap();
        CreateMap<GetWalletDto, AddWalletDto>().ReverseMap();
        CreateMap<AddWalletDto, Wallet>().ReverseMap();
        CreateMap<GetTransactionDto, Transaction>().ReverseMap();
        CreateMap<GetTransactionDto, AddTransactionDto>().ReverseMap();
        CreateMap<AddTransactionDto, Transaction>().ReverseMap();
    }
}