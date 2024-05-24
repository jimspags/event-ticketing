import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { EventService } from '../../services/event-service/event.service';
import { PaymentSuccessOrderDetailsModel } from '../../models/payment-success-order-details-model';

@Component({
  selector: 'app-paymentsuccess',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './paymentsuccess.component.html',
  styleUrl: './paymentsuccess.component.css'
})
export class PaymentsuccessComponent implements OnInit {
  sessionId: string = '';
  paymentSuccessOrderDetails: PaymentSuccessOrderDetailsModel = new PaymentSuccessOrderDetailsModel();

  constructor(private activatedRoute: ActivatedRoute, private eventService: EventService) {
    
  }

  ngOnInit(): void {
    this.sessionId = this.activatedRoute.snapshot.paramMap.get('session_id')!;
    this.getOrderDetails();
  }

  getOrderDetails() {
    this.eventService.getOrderDetails(this.sessionId)
    .subscribe((result: PaymentSuccessOrderDetailsModel) => {
      console.log(result);
      this.paymentSuccessOrderDetails = result;
    })
  }

}
