namespace Domain.Dtos;
using Domain.Entities;

public class GetWalletTransactionDto
{
    public int WalletId { get; set; }
    public double Balance { get; set; }
    public Status Status { get; set; }
    public int TransactionId { get; set; }
    public double Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Recipient { get; set; }

}