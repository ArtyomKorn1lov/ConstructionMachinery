import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelForLandlord } from 'src/app/models/AvailabilityRequestModelForLandlord';

@Component({
  selector: 'app-confirm-list-info',
  templateUrl: './confirm-list-info.component.html',
  styleUrls: ['./confirm-list-info.component.scss']
})
export class ConfirmListInfoComponent implements OnInit {

  public request: AvailabilityRequestModelForLandlord = new AvailabilityRequestModelForLandlord(0, "", "", "", "", 0, []);
  public date: Date = new Date()

  constructor(private accountService: AccountService, private requestService: RequestService) { }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
    await this.requestService.GetForLandLord(this.requestService.GetIdFromLocalStorage()).subscribe(data => {
      this.request = data;
      this.date = new Date(this.request.availableTimeModels[0].date);
    })
  }

}
