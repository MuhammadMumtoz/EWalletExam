namespace Infrastructure.Services;
using AutoMapper;
using Domain.Dtos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Wrapper;
using System.Net;

public class WalletService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public WalletService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<List<GetWalletDto>>> GetAllWallets()
    {
        var list = await _context.Wallets.ToListAsync();
        var response = _mapper.Map<List<GetWalletDto>>(list);
        return new Response<List<GetWalletDto>>(response);
    }
    public async Task<Response<GetWalletDto>> GetWalletById(int id)
    {
        var user = await _context.Wallets.FindAsync(id);
        var response = _mapper.Map<GetWalletDto>(user);
        return new Response<GetWalletDto>(response);
    }
    public async Task<Response<GetWalletBalanceDto>> GetWalletBalance(int id)
    {
        var find = await _context.Wallets.FindAsync(id);
        var response = new GetWalletBalanceDto()
        {
            Balance = find.Balance
        };
        return new Response<GetWalletBalanceDto>(response);
    }

    public async Task<Response<GetUserWalletDto>> GetWalletFullInfo(int id)
    {
        var wallet = (from us in _context.Users
                      join wl in _context.Wallets on us.UserId equals wl.UserId
                      where wl.WalletId == id
                      select new GetUserWalletDto
                      {
                          WalletId = wl.WalletId,
                          StartDate = wl.StartDate,
                          EndDate = wl.EndDate,
                          Balance = wl.Balance,
                          Status = wl.Status,
                          UserId = us.UserId,
                          Firstname = us.FirstName,
                          Lastname = us.LastName,
                          PhoneNumber = us.PhoneNumber,
                          Email = us.Email,
                          Birthdate = us.BirthDate,
                          Gender = us.Gender,
                          Address = us.Address
                      }).FirstOrDefaultAsync();
        var response = await wallet;
        return new Response<GetUserWalletDto>(response);
    }
    public async Task<Response<string>> CheckWalletExistence(string phoneNumber)
    {
        var newUser = (from wl in _context.Wallets
                       join us in _context.Users on wl.UserId equals us.UserId
                       where us.PhoneNumber == phoneNumber
                       select new GetWalletDto
                       {
                           WalletId = wl.WalletId,
                           StartDate = wl.StartDate,
                           EndDate = wl.EndDate,
                           Balance = wl.Balance,
                           Status = wl.Status,
                           UserId = wl.UserId
                       }).FirstOrDefault();
        if (newUser != null)
        { return new Response<string>("Account exists"); }
        else
        { return new Response<string>("Account does not exist"); }
    }
    public async Task<Response<AddWalletDto>> AddWallet(AddWalletDto wallet)
    {
        var newWallet = _mapper.Map<Wallet>(wallet);
        await _context.Wallets.AddAsync(newWallet);
        await _context.SaveChangesAsync();
        var response = wallet;
        return new Response<AddWalletDto>(wallet);
    }
    public async Task<Response<AddWalletDto>> UpdateWallet(AddWalletDto wallet)
    {
        var newWallet = await _context.Wallets.FindAsync(wallet.WalletId);
        newWallet.StartDate = wallet.StartDate;
        newWallet.EndDate = wallet.EndDate;
        newWallet.Balance = wallet.Balance;
        newWallet.Status = wallet.Status;
        newWallet.UserId = wallet.UserId;
        await _context.SaveChangesAsync();
        var response = wallet;
        return new Response<AddWalletDto>(wallet);
    }
    public async Task<Response<string>> DeleteWallet(int id)
    {
        var find = await _context.Wallets.FindAsync(id);
        _context.Wallets.Remove(find);
        var response = await _context.SaveChangesAsync();
        if (response > 0)
            return new Response<string>("Category deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest, "Category not found");
    }
}