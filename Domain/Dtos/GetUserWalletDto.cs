namespace Domain.Dtos;
using Domain.Entities;
public class GetUserWalletDto
{
    public int UserId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime Birthdate { get; set; }
    public Gender Gender { get; set; }

    public string Address { get; set; }
    public int WalletId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Balance { get; set; }
    public Status Status { get; set; }
}