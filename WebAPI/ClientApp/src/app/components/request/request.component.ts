import { Component, OnInit } from '@angular/core';
import { AvailabilityRequestModel } from 'src/app/models/AvailabilityRequestModel';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.scss']
})
export class RequestComponent implements OnInit {

  public requests: AvailabilityRequestModel[] = [];

  constructor() { }

  public ngOnInit(): void {
    this.requests.push(new AvailabilityRequestModel(1, "Автовышка АПТ-32"));
    this.requests.push(new AvailabilityRequestModel(2, "Кран ТТ-22"));
    this.requests.push(new AvailabilityRequestModel(3, "Камаз АН246"));
    this.requests.push(new AvailabilityRequestModel(4, "Ямобур 65-36"));
  }

}
