using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels.OrganizerModels
{
    public class RegisterOrganizerModel
    {
        [Required(ErrorMessage = "Pease enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter max count of teams")]
        public int MaxCountTeam { get; set; }

        [Required(ErrorMessage = "Please enter end date")]
        public string EndDate { get; set; }

        [Required(ErrorMessage = "Please enter start date")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Verify password")]
        public string ConfirmPassword { get; set; }
    }
}
