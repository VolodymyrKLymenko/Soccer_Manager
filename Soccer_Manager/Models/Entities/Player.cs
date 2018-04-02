using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Soccer_Manager.ModelClasses
{
    public class Player
    {
        public Player()
        {
        }

        public Player(string name, string surname, string position, int age, int id)
        {
            Name = name;
            Surname = surname;
            Position = position;
            Age = age;
            PlayerId = id;
        }

        [Key]
        public int PlayerId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }

}
