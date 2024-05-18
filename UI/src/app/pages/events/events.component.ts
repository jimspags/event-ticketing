import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event-service/event.service';
import { EventModel } from '../../models/event-model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './events.component.html',
  styleUrl: './events.component.css'
})
export class EventsComponent implements OnInit {

  constructor(private eventService: EventService) {
  }

  events: EventModel[] = [];

  ngOnInit(): void {
    this.getEvents();
  }

  getEvents() {
    this.eventService.getEvents()
    .subscribe((result: EventModel[]) => {
      this.events = result;
    });
  }
}
