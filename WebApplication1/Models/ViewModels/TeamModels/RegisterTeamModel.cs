using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModels.TeamModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Pease enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Verify password")]
        public string ConfirmPassword { get; set; }
    }
}
