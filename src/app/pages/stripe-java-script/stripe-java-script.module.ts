import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { StripeJavaScriptPageRoutingModule } from './stripe-java-script-routing.module';

import { StripeJavaScriptPage } from './stripe-java-script.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    StripeJavaScriptPageRoutingModule
  ],
  declarations: [StripeJavaScriptPage]
})
export class StripeJavaScriptPageModule {}
