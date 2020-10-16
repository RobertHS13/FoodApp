using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Models.ViewModels
{
    public class Cart : Entity
    {
        public string Type { get; set; }

        public List<MenuPackage> MenuPackages { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
