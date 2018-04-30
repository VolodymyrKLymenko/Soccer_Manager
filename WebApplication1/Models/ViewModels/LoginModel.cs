using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public enum UserType: byte {Team, Organizer}

    public class LoginModel
    {
        [Required(ErrorMessage = "Enter your type")]
        public UserType UserType { get; set; }

        [Required(ErrorMessage = "Please set Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please set password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
