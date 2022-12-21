namespace Domain.Dtos;
using Domain.Entities;
public class AddWalletDto{
    public int WalletId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate {get; set;}
    public double Balance { get; set; }
    public Status Status { get; set; }
    public int UserId { get; set; }
}