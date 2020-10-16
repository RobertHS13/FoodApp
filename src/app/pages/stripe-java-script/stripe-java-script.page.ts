import { Component, OnInit } from '@angular/core';
import { NgxStripeModule } from 'ngx-stripe';

declare let Stripe;

@Component({
  selector: 'app-stripe-java-script',
  templateUrl: './stripe-java-script.page.html',
  styleUrls: ['./stripe-java-script.page.scss'],
})
export class StripeJavaScriptPage implements OnInit {

  stripe = Stripe('pk_test_51HSmsXCHwgHoVAgV5gzDya2PXqX6TGUlnPjnPtND1JMsyhdwareCIbm30xKnjcFGlMTG1yFU5720K5gFH5x8ZNNs003hznMoBe');
  card: any;

  constructor() { }

  ngOnInit() {
  }

  ionViewDidLoad() {
    this.setupStripe();
  }

  setupStripe(){
    let elements = this.stripe.elements();
    var style = {
      base: {
        color: '#32325d',
        lineHeight: '24px',
        fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
        fontSmoothing: 'antialiased',
        fontSize: '16px',
        '::placeholder': {
          color: '#aab7c4'
        }
      },
      invalid: {
        color: '#fa755a',
        iconColor: '#fa755a'
      }
    };

    this.card = elements.create('card', { style: style });

    this.card.mount('#card-element');

    this.card.addEventListener('change', event => {
      var displayError = document.getElementById('card-errors');
      if (event.error) {
        displayError.textContent = event.error.message;
      } else {
        displayError.textContent = '';
      }
    });

    var form = document.getElementById('payment-form');
    form.addEventListener('submit', event => {
      event.preventDefault();

      // this.stripe.createToken(this.card)
      this.stripe.createSource(this.card).then(result => {
        if (result.error) {
          var errorElement = document.getElementById('card-errors');
          errorElement.textContent = result.error.message;
        } else {
          console.log(result);
          this.card.clear();
        }
      });
    });
  }


}
