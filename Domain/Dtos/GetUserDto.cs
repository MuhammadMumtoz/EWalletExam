using System.ComponentModel.DataAnnotations;
using Domain.Entities;
namespace Domain.Dtos;
public class GetUserDto
{
    public int UserId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime Birthdate { get; set; }
    public string Gender { get; set; }

    public string Address { get; set; }
}