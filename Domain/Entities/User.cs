namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
public class User
{
    [Key]
    public int UserId { get; set; }
    [Required(ErrorMessage = "Please enter your First name"), MaxLength(30)]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Please enter your Last name"), MaxLength(30)]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Please enter your Phone number"), MaxLength(30)]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Please enter your E-mail"), MaxLength(30)]
    public string Email { get; set; }
    [Required(ErrorMessage = "Please enter your birthdate")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDate { get; set; }
    [Required(ErrorMessage = "Please select your gender")]
    public Gender Gender { get; set; }
    [Required(ErrorMessage = "Please enter your Address")]
    public string Address { get; set; }
    // public int WalletId { get; set; }
    public Wallet Wallet { get; set; }
}
public enum Gender
{
    Male,
    Female
}