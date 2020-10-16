import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { StripeNativePage } from './stripe-native.page';

const routes: Routes = [
  {
    path: '',
    component: StripeNativePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StripeNativePageRoutingModule {}
