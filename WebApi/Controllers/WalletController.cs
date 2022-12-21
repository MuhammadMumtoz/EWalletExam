using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class WalletController
{
    private readonly WalletService _walletService;

    public WalletController(WalletService walletService)
    {
        _walletService = walletService;
    }
    [HttpGet("GetAllWallets")]
    public async Task<Response<List<GetWalletDto>>> GetAllWallets()
    {
        return await _walletService.GetAllWallets();
    }
    [HttpGet("GetWalletBalance")]
    public async Task<Response<GetWalletBalanceDto>> GetWalletBalance(int id)
    {
        return await _walletService.GetWalletBalance(id);
    }
    [HttpGet("GetWalletById")]
    public async Task<Response<GetWalletDto>> GetWalletById(int id)
    {
        return await _walletService.GetWalletById(id);
    }
    [HttpGet("GetFullWalletUserInfo")]
    public async Task<Response<GetUserWalletDto>> GetWalletFullInfo(int id)
    {
        return await _walletService.GetWalletFullInfo(id);
    }
    [HttpGet("GetWalletExistence")]
    public async Task<Response<string>> CheckWalletExistence(string phoneNumber)
    {
        return await _walletService.CheckWalletExistence(phoneNumber);
    }
    [HttpPost("AddWallet")]
    public async Task<Response<AddWalletDto>> AddWallet(AddWalletDto Wallet)
    {
        return await _walletService.AddWallet(Wallet);
    }
    [HttpPut("UpdateWallet")]
    public async Task<Response<AddWalletDto>> UpdateWallet(AddWalletDto Wallet)
    {
        return await _walletService.UpdateWallet(Wallet);
    }
    [HttpDelete("DeleteWallet")]
    public async Task<Response<string>> DeleteWallet(int id)
    {
        return await _walletService.DeleteWallet(id);
    }
}
