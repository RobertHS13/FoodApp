using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FoodApp.StripeData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace FoodApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IConfiguration _config;

        public BillingController(IConfiguration config)
        {
            _config = config;
            StripeConfiguration.ApiKey = _config.GetSection("Stripe")["STRIPE_SECRET_KEY"];
        }

        [HttpPost("create-customer")]
        public async Task<Customer> CreateCustomer(string email, string name)
        {
            var options = new CustomerCreateOptions
            {
                Email = email,
                Name = name,
            };

            var service = new CustomerService();

            return await service.CreateAsync(options);
        }

        [HttpPost("create-subscription")]
        public async Task<ActionResult<Subscription>> CreateSubscription([FromBody] CreateSubscriptionRequest req)
        {
            // Attach payment method
            var options = new PaymentMethodAttachOptions
            {
                Customer = req.Customer,
            };
            var service = new PaymentMethodService();
            var paymentMethod = service.Attach(req.PaymentMethod, options);

            // Update customer's default invoice payment method
            var customerOptions = new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = paymentMethod.Id,
                },
            };
            var customerService = new CustomerService();
            customerService.Update(req.Customer, customerOptions);

            // Create subscription
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = req.Customer,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = req.Price,
                        //Environment.GetEnvironmentVariable(req.Price),
                    },
                },
            };
            subscriptionOptions.AddExpand("latest_invoice.payment_intent");
            var subscriptionService = new SubscriptionService();
            try
            {
                Subscription subscription = await subscriptionService.CreateAsync(subscriptionOptions);
                return subscription;
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Failed to create subscription.{e}");
                return BadRequest();
            }
        }

        [HttpPost("cancel-subscription")]
        public async Task<ActionResult<Subscription>> CancelSubscription([FromBody] CancelSubscriptionRequest req)
        {
            var service = new SubscriptionService();
            var subscription = service.CancelAsync(req.Subscription, null);
            return await subscription;
        }
    }
}
