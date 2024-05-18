import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventModel } from '../../models/event-model';
import { EventService } from '../../services/event-service/event.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-event',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './event.component.html',
  styleUrl: './event.component.css'
})
export class EventComponent implements OnInit{

  eventId: string = '';
  event: EventModel = new EventModel();

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
}
