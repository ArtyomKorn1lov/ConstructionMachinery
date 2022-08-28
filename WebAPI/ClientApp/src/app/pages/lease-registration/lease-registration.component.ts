import { Component, OnInit } from '@angular/core';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-lease-registration',
  templateUrl: './lease-registration.component.html',
  styleUrls: ['./lease-registration.component.scss']
})
export class LeaseRegistrationComponent implements OnInit {

  public times: AvailableTimeModel[] = [];
  public currentTime: Date = new Date();

  constructor(private accountService: AccountService) { }

  public async ngOnInit(): Promise<void> {
    this.times.push(new AvailableTimeModel(1, new Date(), 1, 1));
    await this.accountService.GetAuthoriseModel();
  }

}
