using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FoodApp.StripeData
{
    public class CreateCustomerRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
