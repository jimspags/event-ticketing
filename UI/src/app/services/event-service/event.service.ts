import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { EventModel } from '../../models/event-model';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient) { }

  apiUrl: string = 'https://localhost:7220';

  getEvents() : Observable<EventModel[]> {
    return this.http.get<EventModel[]>(`${this.apiUrl}/api/events`);
  }
}