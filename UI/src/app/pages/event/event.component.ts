import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventModel } from '../../models/event-model';
import { EventService } from '../../services/event-service/event.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-event',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './event.component.html',
  styleUrl: './event.component.css'
})
export class EventComponent implements OnInit{

  eventId: string = '';
  event: EventModel = new EventModel();
  quantity: number = 0;

  constructor(private activatedRoute: ActivatedRoute, private eventService: EventService, private router: Router) {
    
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
    this.eventService.checkout(this.eventId, this.quantity)
    .subscribe((result: any) => {
      console.log(result);
      window.location.href = result.url;
    })
  }

}
