using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Models.ViewModels
{
    public class Subscription : Entity
    {
        public string SubscriptionId { get; set; }
        public string Type { get; set; }
        public long Quantity { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
