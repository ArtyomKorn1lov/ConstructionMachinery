import { Component, OnInit } from '@angular/core';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';
import { AccountService } from 'src/app/services/account.service';
import { AdvertService } from 'src/app/services/advert.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelCreate } from 'src/app/models/AvailabilityRequestModelCreate';
import { AvailableTimeModelForCreateRequest } from 'src/app/models/AvailableTimeModelForCreateRequest';
import { Router } from '@angular/router';
import { DatetimeService } from 'src/app/services/datetime.service';
import { LeaseTimeModel } from 'src/app/models/LeaseTimeModel';

@Component({
  selector: 'app-lease-registration',
  templateUrl: './lease-registration.component.html',
  styleUrls: ['./lease-registration.component.scss']
})
export class LeaseRegistrationComponent implements OnInit {

  public leaseTimes: LeaseTimeModel[] = [];
  public address: string = "";
  public currentTimeIndex: number | undefined;
  public currentTimeId: number | undefined;
  public request: AvailabilityRequestModelCreate = new AvailabilityRequestModelCreate("", 0, 0, []);
  private targerRoute: string = "/advert-info";

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, private advertService: AdvertService, private requestService: RequestService, private router: Router) { }

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
    this.requestService.create(this.request).subscribe({
      next: async (data) => {
        console.log(data);
        alert(data);
        this.router.navigateByUrl(this.targerRoute);
        return;
      },
      error: (bad) => {
        alert("Ошибка запроса на аренду");
        console.log(bad);
        this.address = '';
        return;
      }
    });
  }

  public convertToNormalDate(times: AvailableTimeModel[]): LeaseTimeModel[] {
    for (let count = 0; count < times.length; count++) {
      times[count].date = new Date(times[count].date);
    }
    times = this.datetimeService.sortByHour(times);
    times = this.sortByDate(times);
    return this.contain(times);
  }

  public sortByDate(times: AvailableTimeModel[]): AvailableTimeModel[] {
    var date;
    for (let i = 0; i < times.length; i++) {
      for (let j = 0; j < times.length - i - 1; j++) {
        if (times[j].date.getFullYear() > times[j + 1].date.getFullYear()) {
          date = times[j];
          times[j] = times[j + 1];
          times[j + 1] = date;
        }
        else if (times[j].date.getMonth() > times[j + 1].date.getMonth()) {
          date = times[j];
          times[j] = times[j + 1];
          times[j + 1] = date;
        }
        else if (times[j].date.getDate() > times[j + 1].date.getDate()) {
          date = times[j];
          times[j] = times[j + 1];
          times[j + 1] = date;
        }
      }
    }
    return times;
  }

  public contain(times: AvailableTimeModel[]): LeaseTimeModel[] {
    let leaseTimes: LeaseTimeModel[] = [];
    for (let i = 0; i < times.length; i++) {
      if (!this.findInList(leaseTimes, times[i].date)) {
        leaseTimes.push(new LeaseTimeModel(times[i].date, []));
        for (let j = 0; j < times.length; j++) {
          if (times[i].date.getMonth() + '.' + times[i].date.getDate() + '.' + times[i].date.getFullYear() == times[j].date.getMonth() + '.' + times[j].date.getDate() + '.' + times[j].date.getFullYear()) {
            leaseTimes[leaseTimes.length - 1].times.push(times[j]);
          }
        }
      }
    }
    return leaseTimes;
  }

  public findInList(leaseTimes: LeaseTimeModel[], date: Date): boolean {
    for (let count = 0; count < leaseTimes.length; count++) {
      if (leaseTimes[count].date.getMonth() + '.' + leaseTimes[count].date.getDate() + '.' + leaseTimes[count].date.getFullYear() == date.getMonth() + '.' + date.getDate() + '.' + date.getFullYear()) {
        return true;
      }
    }
    return false;
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    await this.requestService.getAvailableTimesByAdvertId(this.advertService.getIdFromLocalStorage()).subscribe(data => {
      this.leaseTimes = this.convertToNormalDate(data);
    });
  }
}
