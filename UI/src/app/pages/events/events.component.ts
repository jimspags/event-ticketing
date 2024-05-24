import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event-service/event.service';
import { EventModel } from '../../models/event-model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './events.component.html',
  styleUrl: './events.component.css'
})
export class EventsComponent implements OnInit {

  constructor(private eventService: EventService) {
  }

  events: EventModel[] | undefined;
  eventsCategory: string = 'all';
  eventsSearch: string = '';

  ngOnInit(): void {
    this.getEvents();
  }

  getEvents() {
    this.events = undefined;
    this.eventService.getFilteredEvents(this.eventsCategory, this.eventsSearch)
    .subscribe((result: EventModel[]) => {
      this.events = result;
    });
  }
}
