import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelForCustomer } from 'src/app/models/AvailabilityRequestModelForCustomer';

@Component({
  selector: 'app-my-request-info',
  templateUrl: './my-request-info.component.html',
  styleUrls: ['./my-request-info.component.scss']
})
export class MyRequestInfoComponent implements OnInit {

  public request: AvailabilityRequestModelForCustomer = new AvailabilityRequestModelForCustomer(0, "", "", "", "", 0, 0, []);
  public date: Date = new Date();

  constructor(private accountService: AccountService, private requestService: RequestService) { }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
    await this.requestService.GetForCustomer(this.requestService.GetIdFromLocalStorage()).subscribe(data => {
      this.request = data;
      this.date = new Date(this.request.availableTimeModels[0].date);
    });
  }

}
