using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Models.ViewModels
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Menu> Menus { get; set; }
        public Cart Cart { get; set; }
        public List<Source> Sources { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
