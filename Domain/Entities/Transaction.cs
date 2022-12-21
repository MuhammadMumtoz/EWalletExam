namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
public class Transaction
{
    [Key]
    public int TransactionId { get; set; }
    public double Amount { get; set; }
    public TransactionType TransactionType {get; set;}
    public string Recipient {get; set;}
    public TransactionStatus TransactionStatus { get; set; }
    public DateTime TransactionDate { get; set; }
    public int WalletId { get; set; }
    public Wallet Wallet {get; set;}

}
public enum TransactionType{
    Replenishment,
    Withdrawal
}
public enum TransactionStatus{
    Successful,
    Pending,
    Declined
}