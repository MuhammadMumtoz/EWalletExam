namespace Domain.Dtos;

using System.ComponentModel.DataAnnotations;
using Domain.Entities;
public class AddUserDto
{
    public int UserId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime Birthdate { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
}