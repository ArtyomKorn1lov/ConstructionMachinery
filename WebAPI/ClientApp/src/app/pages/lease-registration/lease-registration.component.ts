import { Component, OnInit } from '@angular/core';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';
import { AccountService } from 'src/app/services/account.service';
import { AdvertService } from 'src/app/services/advert.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelCreate } from 'src/app/models/AvailabilityRequestModelCreate';
import { AvailableTimeModelForCreateRequest } from 'src/app/models/AvailableTimeModelForCreateRequest';
import { Router } from '@angular/router';

@Component({
  selector: 'app-lease-registration',
  templateUrl: './lease-registration.component.html',
  styleUrls: ['./lease-registration.component.scss']
})
export class LeaseRegistrationComponent implements OnInit {

  public times: AvailableTimeModel[] = [];
  public address: string = "";
  public currentTimeId: number = 0;
  public request: AvailabilityRequestModelCreate = new AvailabilityRequestModelCreate("", 0, 0, []);
  private targerRoute: string = "/advert-info";

  constructor(private accountService: AccountService, private advertService: AdvertService, private requestService: RequestService, private router: Router) { }

  public createRequest(): void {
    if (this.address == undefined || this.address.trim() == '') {
      alert("Введите адрес доставки");
      this.address = '';
      return;
    }
    if (this.currentTimeId == 0 || this.currentTimeId == undefined) {
      alert("Выберете время доставки");
      return;
    }
    let modelTime: AvailableTimeModelForCreateRequest[] = [];
    modelTime.push(new AvailableTimeModelForCreateRequest(this.currentTimeId, 3));
    this.request = new AvailabilityRequestModelCreate(this.address, 3, 0, modelTime);
    this.requestService.create(this.request).subscribe(data => {
      if (data == "success") {
        console.log(data);
        alert(data);
        this.router.navigateByUrl(this.targerRoute);
        return;
      }
      alert("Ошибка создания запроса на аренду");
      console.log(data);
      this.address = '';
      return;
    });
  }

  public convertToNormalDate(): void {
    for(let count = 0; count < this.times.length; count++) {
      this.times[count].date = new Date(this.times[count].date);
    }
    this.times = this.advertService.sortByHour(this.times);
    this.sortByDate();
  }

  public sortByDate(): void {
    var date;
    for(let i = 0; i < this.times.length; i++) {
      for(let j = 0; j < this.times.length - i - 1; j++) {
        if(this.times[j].date.getFullYear() > this.times[j+1].date.getFullYear()) {
          date = this.times[j];
          this.times[j] = this.times[j+1];
          this.times[j+1] = date;
        }
        else if(this.times[j].date.getMonth() > this.times[j+1].date.getMonth()) {
          date = this.times[j];
          this.times[j] = this.times[j+1];
          this.times[j+1] = date;
        }
        else if(this.times[j].date.getDate() > this.times[j+1].date.getDate()) {
          date = this.times[j];
          this.times[j] = this.times[j+1];
          this.times[j+1] = date;
        }
      }
    }
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    await this.requestService.getAvailableTimesByAdvertId(this.advertService.getIdFromLocalStorage()).subscribe(data => {
      this.times = data;
      this.convertToNormalDate();
    })
  }

}
