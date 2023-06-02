import { Component, EventEmitter, OnInit, ViewChild } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AdvertListComponent } from './pages/advert-list/advert-list.component';
import { ContentChild } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [AdvertListComponent]
})
export class AppComponent implements OnInit {

  public executeAction: EventEmitter<any> = new EventEmitter();

  constructor(private router: Router) { }

  title = 'ConstructionMachineryApp';

  public async findAdvert(): Promise<void> {
    this.executeAction.emit();
  }

  ngOnInit() {
    this.router.events.subscribe((event) => {
      if (!(event instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0)
    });
  }
}
