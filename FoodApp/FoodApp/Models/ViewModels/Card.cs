using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Models.ViewModels
{
    public class Card : Entity
    {
        public string Brand { get; set; }
        public string Country { get; set; }
        public string Cvc_Check { get; set; }
        public int Exp_Month { get; set; }
        public int Exp_Year { get; set; }
        public string Funding { get; set; }
        public string Last4 { get; set; }
        public string Name { get; set; }
        public string Three_D_Secure { get; set; }
        public string Tokenization_Method { get; set; }

        public int SourceId { get; set; }
        public Source Source { get; set; }
    }
}
