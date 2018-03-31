using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelClasses
{
    public class Player
    {
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
