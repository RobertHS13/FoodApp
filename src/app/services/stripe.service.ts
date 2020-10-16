import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Customer } from '../interfaces';
import { Subscription} from '../interfaces';

@Injectable({
  providedIn: 'root'
})

export class StripeService {

  baseUrl = "https://localhost:44365/api/Billing";

  

  constructor(private http: HttpClient) { }

  createCustomer(email: string, name: string){
    return this.http.post<Customer>(`${this.baseUrl + "/create-customer"}`, {
      "email": email,
      "name": name
    });
  }

  CreateSubscription(paymentMethodId: string, customerId: string, priceId: string){
    this.http.post(`${this.baseUrl + "/create-subscription"}`, {
      "paymentMethodId": paymentMethodId,
      "customerId": customerId,
      "priceId": priceId
    }).subscribe(
      result =>{
        console.log("JJJJjRRRRRR: ",result);
      }
    );
  }
}
