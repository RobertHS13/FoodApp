import { Component, OnInit } from '@angular/core';
import { StripeService } from '../../services/stripe.service';
import { Subscription} from '../../interfaces';

declare let Stripe;

@Component({
  selector: 'app-stripe-native',
  templateUrl: './stripe-native.page.html',
  styleUrls: ['./stripe-native.page.scss'],
})
export class StripeNativePage implements OnInit {

  email: string;
  fullName: string;

  stripe = Stripe('pk_test_51HSmsXCHwgHoVAgV5gzDya2PXqX6TGUlnPjnPtND1JMsyhdwareCIbm30xKnjcFGlMTG1yFU5720K5gFH5x8ZNNs003hznMoBe');
  
  elements = this.stripe.elements();

  cardElement: any;

  constructor(protected stripeService: StripeService) { }

  ngOnInit() {
    var style = {
      base: {
        color: "#32325d",
        fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
        fontSmoothing: "antialiased",
        fontSize: "16px",
        "::placeholder": {
          color: "#aab7c4"
        }
      },
      invalid: {
        color: "#fa755a",
        iconColor: "#fa755a"
      }
    };

    this.cardElement = this.elements.create("card", { style: style });
    this.cardElement.mount("#card-element");
    this.cardElement.on('change', this.showCardError);
  }

  mymethod(){this.stripe.createPaymentMethod({
      type: 'card',
      card: this.cardElement,
    }).then((result) => {
      if (result.error) {
        console.log(result.error);
      } else {
        this.stripeService.createCustomer(this.email, this.fullName).subscribe(r =>{
          console.log("ID Payment: ",result.paymentMethod.id);
          console.log("Customer: ", r.id);
          this.stripeService.CreateSubscription(result.paymentMethod.id, r.id, "price_1HYjMQCHwgHoVAgVw9Fv4sty");
        });
      }
    });
  }

  /*mymethod(){
    this.stripe.createPaymentMethod({ type: 'card', card: this.cardElement }).then(result => {
      if (result.error) {
        console.log(result.error);
        return;
      }

      this.stripeService.createCustomer("user5@gmail.com", "User 5").pipe(flatMap(r => {
        this.idCustomer = r.id;
        return this.stripeService.CreateSubscription(result.paymentMethod.id, this.idCustomer, "price_1HYjMQCHwgHoVAgVw9Fv4sty");
      }));
    });
  }*/
  
  showCardError(event) {
    let displayError = document.getElementById('card-errors');
    if (event.error) {
      displayError.textContent = event.error.message;
    } else {
      displayError.textContent = '';
    }
  }


  createPaymentMethod(cardElement, customerId, priceId) {
    return this.stripe
      .createPaymentMethod({
        type: 'card',
        card: cardElement,
      })
      .then((result) => {
        if (result.error) {
          console.log(result.error);
        } else {
          this.createSubscription(
            customerId,
            result.paymentMethod.id,
            priceId
          );
        }
      });
  }

  createSubscription(customerId, paymentMethodId, priceId) {
    return (
      fetch('/create-subscription', {
        method: 'post',
        headers: {
          'Content-type': 'application/json',
        },
        body: JSON.stringify({
          customerId: customerId,
          paymentMethodId: paymentMethodId,
          priceId: priceId,
        }),
      })
        .then((response) => {
          return response.json();
        })
        // If the card is declined, display an error to the user.
        .then((result) => {
          if (result.error) {
            // The card had an error when trying to attach it to a customer.
            throw result;
          }
          return result;
        })
        // Normalize the result to contain the object returned by Stripe.
        // Add the additional details we need.
        .then((result) => {
          return {
            paymentMethodId: paymentMethodId,
            priceId: priceId,
            subscription: result,
          };
        })
        // Some payment methods require a customer to be on session
        // to complete the payment process. Check the status of the
        // payment intent to handle these actions.
        .then(this.handlePaymentThatRequiresCustomerAction)
        // No more actions required. Provision your service for the user.
        .then(this.onSubscriptionComplete)
        .catch((error) => {
          // An error has happened. Display the failure to the user here.
          // We utilize the HTML element we created.
          this.showCardError(error);
        })
    );
  }

  onSubscriptionComplete(result) {
    // Payment was successful.
    if (result.subscription.status === 'active') {
      // Change your UI to show a success message to your customer.
      // Call your backend to grant access to your service based on
      // `result.subscription.items.data[0].price.product` the customer subscribed to.
    }
  }

  handlePaymentThatRequiresCustomerAction({
    subscription,
    invoice,
    priceId,
    paymentMethodId
  })
  {
    let setupIntent = subscription.pending_setup_intent;
  
    if (setupIntent && setupIntent.status === 'requires_action')
    {
      return this.stripe
        .confirmCardSetup(setupIntent.client_secret, {
          payment_method: paymentMethodId,
        })
        .then((result) => {
          if (result.error) {
            // start code flow to handle updating the payment details
            // Display error message in your UI.
            // The card was declined (i.e. insufficient funds, card has expired, etc)
            throw result;
          } else {
            if (result.setupIntent.status === 'succeeded') {
              // There's a risk of the customer closing the window before callback
              // execution. To handle this case, set up a webhook endpoint and
              // listen to setup_intent.succeeded.
              return {
                priceId: priceId,
                subscription: subscription,
                invoice: invoice,
                paymentMethodId: paymentMethodId,
              };
            }
          }
        });
    }
    else {
      // No customer action needed
      return { subscription, priceId, paymentMethodId };
    }
  }
  
  /*ionViewDidLoad() {
    this.stripe.setPublishableKey('pk_test_51HSmsXCHwgHoVAgV5gzDya2PXqX6TGUlnPjnPtND1JMsyhdwareCIbm30xKnjcFGlMTG1yFU5720K5gFH5x8ZNNs003hznMoBe');
  }*/

  /*validateCard(){
    let card = {
      number: this.cardNumber,
      expMonth: this.cardMonth,
      expYear: this.cardYear,
      cvc: this.cardCVV
     };

     console.log(card.number, card.expMonth, card.expYear, card.cvc);
     // Run card validation here and then attempt to tokenise
     
     this.stripe.createCardToken(card)
        .then(token => console.log(token))
        .catch(error => console.error(error));
  }*/


}
