import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelForLandlord } from 'src/app/models/AvailabilityRequestModelForLandlord';
import { Router } from '@angular/router';
import { ConfirmModel } from 'src/app/models/ConfirmModel';
import { ImageModel } from 'src/app/models/ImageModel';

@Component({
  selector: 'app-confirm-list-info',
  templateUrl: './confirm-list-info.component.html',
  styleUrls: ['./confirm-list-info.component.scss']
})
export class ConfirmListInfoComponent implements OnInit {

  public request: AvailabilityRequestModelForLandlord = new AvailabilityRequestModelForLandlord(0, "", "", "", "", 0, [ new ImageModel(0, "", "", 0) ], []);
  public date: Date = new Date();
  private targetRoute: string = "/confirm-list";

  constructor(private accountService: AccountService, private requestService: RequestService, private router: Router) { }

  public confirm(state: number): void {
    let model: ConfirmModel = new ConfirmModel(this.request.id, state);
    this.requestService.Confirm(model).subscribe(data => {
      if (data == "success") {
        console.log(data);
        alert(data);
        this.router.navigateByUrl(this.targetRoute);
        return;
      }
      alert("Ошибка подтверждения запроса");
      console.log(data);
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
    await this.requestService.GetForLandLord(this.requestService.GetIdFromLocalStorage()).subscribe(data => {
      this.request = data;
      this.date = new Date(this.request.availableTimeModels[0].date);
    })
  }

}
