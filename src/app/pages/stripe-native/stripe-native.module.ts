import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { StripeNativePageRoutingModule } from './stripe-native-routing.module';

import { StripeNativePage } from './stripe-native.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    StripeNativePageRoutingModule
  ],
  declarations: [StripeNativePage]
})
export class StripeNativePageModule {}
