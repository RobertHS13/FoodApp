using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Models.ViewModels
{
    public class Menu : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string CartType { get; set; }

        public List<MenuPackage> MenuPackages { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
