import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { EventModel } from '../../models/event-model';
import { EventService } from '../../services/event-service/event.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-event',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './event.component.html',
  styleUrl: './event.component.css'
})
export class EventComponent implements OnInit{

  eventId: string = '';
  event: EventModel = new EventModel();
  quantity: number = 0;
  isCheckout: boolean = false;

  constructor(private activatedRoute: ActivatedRoute, private eventService: EventService) {
    
  }

  ngOnInit(): void {
    this.eventId = this.activatedRoute.snapshot.paramMap.get('id')!;
    this.getEvent();
  }

  getEvent(): void {
    this.eventService.getEvent(this.eventId)
    .subscribe((result: EventModel) => {
      this.event = result;
    })
  }

  checkout() {
    if (this.quantity == 0) return;

    this.isCheckout = true;
    this.eventService.checkout(this.eventId, this.quantity)
    .subscribe((result: any) => {
      window.location.href = result.url;
    })
  }

}
