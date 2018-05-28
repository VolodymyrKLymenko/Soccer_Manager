using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Model_Classes
{
    public class Player
    {

        public Player()
        {
            Born = new DateTime(1998, 3, 14);
        }

        public Player(string name, string surname, string position)
        {
            Name = name;
            Surname = surname;
            Position = position;
            Born = new DateTime(1998, 3, 14);
        }

        public int PlayerId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public DateTime Born { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

        [NotMapped]
        public int Age_ { get; set; }
    }
}
