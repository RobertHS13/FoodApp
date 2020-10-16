export class User{
    firstName: string;
    lastName: string;
    type: string;
    phone: string;
    email: string; 
    password: string;
}

export class Customer{
    id: string;
}

export class Subscription{
    paymentMethodId: string;
    customerId: string;
    priceId: string;
}