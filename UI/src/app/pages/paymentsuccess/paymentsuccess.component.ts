import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-paymentsuccess',
  standalone: true,
  imports: [],
  templateUrl: './paymentsuccess.component.html',
  styleUrl: './paymentsuccess.component.css'
})
export class PaymentsuccessComponent implements OnInit {
  sessionId: string = '';

  constructor(private activatedRoute: ActivatedRoute) {
    
  }

  ngOnInit(): void {
    this.sessionId = this.activatedRoute.snapshot.paramMap.get('session_id')!;
  }

}
