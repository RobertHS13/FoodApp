using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace FoodApp.StripeData
{
    public class CreateSubscriptionRequest
    {
        [JsonProperty("paymentMethodId")]
        public string PaymentMethod { get; set; }

        [JsonProperty("customerId")]
        public string Customer { get; set; }

        [JsonProperty("priceId")]
        public string Price { get; set; }
    }
}
