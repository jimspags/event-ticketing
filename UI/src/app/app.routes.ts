import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { EventComponent } from './pages/event/event.component';
import { CheckoutComponent } from './pages/checkout/checkout.component';
import { EventsComponent } from './pages/events/events.component';
import { PaymentsuccessComponent } from './pages/paymentsuccess/paymentsuccess.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'event/:id',
    component: EventComponent,
  },
  {
    path: 'events',
    component: EventsComponent,
  },
  {
    path: 'checkout',
    component: CheckoutComponent,
  },
  {
    path: "payment-success/:session_id",
    component: PaymentsuccessComponent
  }
];
