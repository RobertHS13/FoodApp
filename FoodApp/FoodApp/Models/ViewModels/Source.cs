using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Models.ViewModels
{
    public class Source : Entity
    {
        public double Amount { get; set; }
        public string Client_Secret { get; set; }
        public bool Created { get; set; }
        public string Currency { get; set; }
        public string Flow { get; set; }
        public string SourceId { get; set; }
        public bool Livemode { get; set; }
        public string Object { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Usage { get; set; }

        public Card Card { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
