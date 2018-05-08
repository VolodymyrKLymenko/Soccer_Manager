using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Model_Classes
{
    public class Reward
    {
        public Reward()
        { }

        public Reward(string name, string date)
        {
            Name = name;
            Date = date;
        }

        public int RewardId { get; set; }

        public string Name { get; set; }
        public string Date { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
