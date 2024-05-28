import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateHelperService {

  constructor() { }

  isEventRecent(date: string): boolean {
    var dateTimeNow = new Date();
    var eventDate = new Date(date);

    var dateTimeLast30Days = new Date();
    dateTimeLast30Days.setDate(dateTimeLast30Days.getDate() - 30);

    return (eventDate <= dateTimeNow) && (eventDate >= dateTimeLast30Days)
  }

  isEventUpcoming(date: string): boolean {
    var dateTimeNow = new Date();
    var eventDate = new Date(date);

    return eventDate > dateTimeNow;
  }
}
