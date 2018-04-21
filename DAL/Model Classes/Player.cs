using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DAL.Model_Classes
{
    public class Player
    {
        public Player()
        {
        }

        public Player(string name, string surname, string position, int age)
        {
            Name = name;
            Surname = surname;
            Position = position;
            Age = age;
        }

        public int PlayerId { get; set; }

        [Required(ErrorMessage = "Please enter a player name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a player surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter a position")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Please enter an age")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive number")]
        public int Age { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
