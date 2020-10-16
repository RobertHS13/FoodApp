using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Models.ViewModels
{
    public class MenuItem : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] image {get; set;}

        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int MenuPackagedId { get; set; }
        public MenuPackage MenuPackage { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
