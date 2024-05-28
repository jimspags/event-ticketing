import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { EventService } from './services/event-service/event.service';
import { DateHelperService } from './Helpers/date-helper/date-helper.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [EventService, DateHelperService]
})
export class AppComponent {
  title = 'UI';
}
