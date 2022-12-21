namespace Infrastructure.Services;
using AutoMapper;
using Domain.Dtos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Services;
using Domain.Wrapper;
using System.Net;

public class TransactionService{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private WalletService _walletService; // For updating Wallet Balance
    public TransactionService(DataContext context,IMapper mapper,WalletService walletService)
    {
        _context = context;
        _mapper = mapper;
        _walletService = walletService;
    }

    public async Task<Response<List<GetTransactionDto>>> GetAllTransactions(){
        var list =  await _context.Transactions.ToListAsync();
        var response = _mapper.Map<List<GetTransactionDto>>(list);
        return new Response<List<GetTransactionDto>>(response);
    }
    public async Task<Response<List<GetTransactionDto>>> GetAllReplenishment(){
        var list =  (from tr in _context.Transactions
                    where (int)tr.TransactionType == 0
                    select new GetTransactionDto(){
                      TransactionId = tr.TransactionId,
                      Amount = tr.Amount,
                      Recipient = tr.Recipient,
                      TransactionType = tr.TransactionType,
                      TransactionStatus = tr.TransactionStatus,
                      TransactionDate = tr.TransactionDate,
                      WalletId = tr.WalletId  
                    }).ToListAsync();
        // list =  await _context.Transactions.ToListAsync();
        var response = await list;
        return new Response<List<GetTransactionDto>>(response);
    }
    public async Task<Response<List<GetTransactionDto>>> GetAllReplenishmentByDate(DateTime start,DateTime end){
        var list =  (from tr in _context.Transactions
                    where (int)tr.TransactionType == 0 && tr.TransactionDate<end && tr.TransactionDate>start
                    select new GetTransactionDto(){
                      TransactionId = tr.TransactionId,
                      Amount = tr.Amount,
                      Recipient = tr.Recipient,
                      TransactionType = tr.TransactionType,
                      TransactionStatus = tr.TransactionStatus,
                      TransactionDate = tr.TransactionDate,
                      WalletId = tr.WalletId  
                    }).ToListAsync();
        var response =  await list;
        return new Response<List<GetTransactionDto>>(response);
    }
    public async Task<Response<TotalSumAndAmountDto>> GetAllReplenishmentByDateWithTotalCountAndAmount(DateTime start,DateTime end){
        //Successful Replenishments only.
        var totalCount = (from tr in _context.Transactions 
                     where (int)tr.TransactionStatus == 0 && tr.TransactionDate<end && tr.TransactionDate>start
                     select (int)tr.TransactionType == 0).Count();
        var totalSum = (from tr in _context.Transactions 
                     where (int)tr.TransactionType == 0 && (int)tr.TransactionStatus == 0 && tr.TransactionDate<end && tr.TransactionDate>start
                     select tr.Amount).Sum();            
        var response = new TotalSumAndAmountDto(){
            TotalCount = totalCount,
            TotalAmount = totalSum
        };
        return new Response<TotalSumAndAmountDto>(response);
    }
    public async Task<Response<GetTransactionDto>> GetById(int id){
        var Transaction = await _context.Transactions.FindAsync(id);
        var response = _mapper.Map<GetTransactionDto>(Transaction);
        return new Response<GetTransactionDto>(response);
    }
    public async Task<Response<GetWalletTransactionDto>> GetTransactionFullInfo(int id){
        var transaction = (from tr in _context.Transactions
                    join wl in _context.Wallets on tr.WalletId equals wl.WalletId
                    where tr.TransactionId == id
                    select new GetWalletTransactionDto()
                    {
                        WalletId = wl.WalletId,
                        Balance = wl.Balance,
                        Status = wl.Status,
                        TransactionId = tr.TransactionId,
                        Amount = tr.Amount,
                        TransactionType = tr.TransactionType,
                        TransactionStatus = tr.TransactionStatus,
                        TransactionDate = tr.TransactionDate,
                        Recipient = tr.Recipient
                    }).FirstOrDefaultAsync();
        var response =  await transaction;
        return new Response<GetWalletTransactionDto>(response);
    }
    public async Task<Response<AddTransactionDto>> AddReplenishment(AddTransactionDto transaction){
        var newTransaction = _mapper.Map<Transaction>(transaction);
        //Updating Wallet Balance
        var updateWalletBalance = await _context.Wallets.FindAsync(newTransaction.WalletId);
        updateWalletBalance.Balance = updateWalletBalance.Balance + newTransaction.Amount;
        //If replenishment exceed balance maxLimit
        if (((int)updateWalletBalance.Status == 0 && updateWalletBalance.Balance >10000)||((int)updateWalletBalance.Status == 1 && updateWalletBalance.Balance >100000))
        {
            updateWalletBalance.Balance = updateWalletBalance.Balance - newTransaction.Amount;
            newTransaction.TransactionStatus = TransactionStatus.Declined;
        }
        //Map and add to dB
        var updatedBalance = _mapper.Map<AddWalletDto>(updateWalletBalance);
        await _walletService.UpdateWallet(updatedBalance);
        //Change type to Replenishment
        newTransaction.TransactionType = TransactionType.Replenishment;
        await _context.Transactions.AddAsync(newTransaction);
        await _context.SaveChangesAsync();
        var response = transaction;
        return new Response<AddTransactionDto>(response);
    }
    public async Task<Response<AddTransactionDto>> AddWithdrawal(AddTransactionDto transaction){
        var newTransaction = _mapper.Map<Transaction>(transaction);
        //Updating Wallet Balance
        var updateWalletBalance = await _context.Wallets.FindAsync(newTransaction.WalletId);
        updateWalletBalance.Balance = updateWalletBalance.Balance - newTransaction.Amount;
        //If replenishment exceed balance maxLimit
        if (updateWalletBalance.Balance <0)
        {
            updateWalletBalance.Balance = updateWalletBalance.Balance + newTransaction.Amount;
            newTransaction.TransactionStatus = TransactionStatus.Declined;
        }
        //Map and add to dB
        var updatedBalance = _mapper.Map<AddWalletDto>(updateWalletBalance);
        await _walletService.UpdateWallet(updatedBalance);
        // Change to Type to Withdrawal
        newTransaction.TransactionType = TransactionType.Withdrawal;
        await _context.Transactions.AddAsync(newTransaction);
        await _context.SaveChangesAsync();
        var response = transaction;
        return new Response<AddTransactionDto>(response);
    }
    public async Task<Response<AddTransactionDto>> UpdateTransaction(AddTransactionDto transaction){
        var newTransaction = await _context.Transactions.FindAsync(transaction.TransactionId);
        newTransaction.Amount = transaction.Amount;
        newTransaction.TransactionType = transaction.TransactionType;
        newTransaction.TransactionStatus = transaction.TransactionStatus;
        newTransaction.TransactionDate = transaction.TransactionDate;
        newTransaction.WalletId = transaction.WalletId;
        newTransaction.Recipient = transaction.Recipient;
        await _context.SaveChangesAsync();
        var response = transaction;
        return new Response<AddTransactionDto>(response);
    }
    public async Task<Response<string>> DeleteTransaction(int id){
        var find = await _context.Transactions.FindAsync(id);
        _context.Transactions.Remove(find);
        var response = await _context.SaveChangesAsync();
        if(response>0)
                return new Response<string>("Category deleted successfully");
                return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
    }
}