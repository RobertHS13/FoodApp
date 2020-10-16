import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { StripeJavaScriptPage } from './stripe-java-script.page';

describe('StripeJavaScriptPage', () => {
  let component: StripeJavaScriptPage;
  let fixture: ComponentFixture<StripeJavaScriptPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StripeJavaScriptPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(StripeJavaScriptPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
