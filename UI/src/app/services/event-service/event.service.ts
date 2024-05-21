import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { EventModel } from '../../models/event-model';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient) { }

  apiUrl: string = 'https://localhost:7220/api/events';

  getEvents() : Observable<EventModel[]> {
    return this.http.get<EventModel[]>(`${this.apiUrl}/`);
  }

  getFilteredEvents(category: string, search: string) : Observable<EventModel[]> {
    return this.http.get<EventModel[]>(`${this.apiUrl}/filter/${category}?search=${search}`);
  }

  getEvent(id: string) : Observable<EventModel> {
    return this.http.get<EventModel>(`${this.apiUrl}/${id}`)
  }

  checkout(id: string, quantity: number) : Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/checkout/${id}/${quantity}`);
  }
  
}
