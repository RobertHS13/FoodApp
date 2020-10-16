import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { StripeNativePage } from './stripe-native.page';

describe('StripeNativePage', () => {
  let component: StripeNativePage;
  let fixture: ComponentFixture<StripeNativePage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StripeNativePage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(StripeNativePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
