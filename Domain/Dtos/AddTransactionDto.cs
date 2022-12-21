namespace Domain.Dtos;
using Domain.Entities;
public class AddTransactionDto
{
    public int TransactionId { get; set; }
    public double Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
    public string Recipient { get; set; }

    public DateTime TransactionDate { get; set; }
    public int WalletId { get; set; }
}