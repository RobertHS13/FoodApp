using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Models.ViewModels
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public string CreatedByUser { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
