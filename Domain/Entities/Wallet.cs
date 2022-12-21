namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class Wallet
{
    [Key]
    public int WalletId { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; } = DateTime.Now;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime EndDate {get; set;}
    [Range(0,100000)]
    public double Balance { get; set; }
    public Status Status { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Transaction> Transactions { get; set; }
    public Wallet()
    {
        Transactions = new List<Transaction>();
    }
}
public enum Status
{
    identified,
    unidentified
}