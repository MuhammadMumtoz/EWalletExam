using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class TransactionController
{
    private readonly TransactionService _transactionService;

    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    [HttpGet("GetAllTransactions")]
    public async Task<Response<List<GetTransactionDto>>> GetAllTransactions()
    {
        return await _transactionService.GetAllTransactions();
    }
    [HttpGet("GetAllReplenishments")]
    public async Task<Response<List<GetTransactionDto>>> GetAllReplenishment()
    {
        return await _transactionService.GetAllReplenishment();
    }
    [HttpGet("GetAllReplenishmentByDate")]
    public async Task<Response<List<GetTransactionDto>>> GetAllReplenishmentByDate(DateTime start, DateTime end)
    {
        return await _transactionService.GetAllReplenishmentByDate(start,end);
    }
    [HttpGet("GetAllReplenishmentCountAndSum")]
    public async Task<Response<TotalSumAndAmountDto>> GetAllReplenishmentByDateWithTotalCountAndAmount(DateTime start, DateTime end)
    {
        return await _transactionService.GetAllReplenishmentByDateWithTotalCountAndAmount(start,end);
    }
    [HttpGet("GetTransactionById")]
    public async Task<Response<GetTransactionDto>> GetById(int id)
    {
        return await _transactionService.GetById(id);
    }
    [HttpGet("GetFullTransactionWalletInfo")]
    public async Task<Response<GetWalletTransactionDto>> GetTransactionFullInfo(int id)
    {
        return await _transactionService.GetTransactionFullInfo(id);
    }
    [HttpPost("AddReplenishment")]
    public async Task<Response<AddTransactionDto>> AddReplenishment(AddTransactionDto Transaction)
    {
        return await _transactionService.AddReplenishment(Transaction);
    }
    [HttpPost("AddWithdrawal")]
    public async Task<Response<AddTransactionDto>> AddWithdrawal(AddTransactionDto Transaction)
    {
        return await _transactionService.AddWithdrawal(Transaction);
    }
    [HttpPut("UpdateTransaction")]
    public async Task<Response<AddTransactionDto>> UpdateTransaction(AddTransactionDto Transaction)
    {
        return await _transactionService.UpdateTransaction(Transaction);
    }
    [HttpDelete("DeleteTransaction")]
    public async Task<Response<string>> DeleteTransaction(int id)
    {
        return await _transactionService.DeleteTransaction(id);
    }
}
