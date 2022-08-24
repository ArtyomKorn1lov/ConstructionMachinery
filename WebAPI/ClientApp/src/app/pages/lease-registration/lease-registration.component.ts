import { Component, OnInit } from '@angular/core';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';

@Component({
  selector: 'app-lease-registration',
  templateUrl: './lease-registration.component.html',
  styleUrls: ['./lease-registration.component.scss']
})
export class LeaseRegistrationComponent implements OnInit {

  public times: AvailableTimeModel[] = [];
  public currentTime: Date = new Date();

  constructor() { }

  public ngOnInit(): void {
    this.times.push(new AvailableTimeModel(1, new Date(), 1, 1))
  }

}
