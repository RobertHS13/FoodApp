import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { StripeJavaScriptPage } from './stripe-java-script.page';

const routes: Routes = [
  {
    path: '',
    component: StripeJavaScriptPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StripeJavaScriptPageRoutingModule {}
