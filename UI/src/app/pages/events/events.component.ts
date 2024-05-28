import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event-service/event.service';
import { EventModel } from '../../models/event-model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DateHelperService } from '../../Helpers/date-helper/date-helper.service';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './events.component.html',
  styleUrl: './events.component.css'
})
export class EventsComponent implements OnInit {

  constructor(private eventService: EventService, private dateHelperService: DateHelperService) {
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

  isEventRecent = (date: string): boolean => this.dateHelperService.isEventRecent(date);

  isEventUpcoming = (date: string): boolean => this.dateHelperService.isEventUpcoming(date);
}
