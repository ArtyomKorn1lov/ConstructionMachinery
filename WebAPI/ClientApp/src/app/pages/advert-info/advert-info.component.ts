import { Component, OnInit } from '@angular/core';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelInfo } from 'src/app/models/AdvertModelInfo';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';
import { AvailableDayModel } from 'src/app/models/AvailableDayModel';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';
import { ImageModel } from 'src/app/models/ImageModel';
import { AdvertModelUpdate } from 'src/app/models/AdvertModelUpdate';
import { ImageService } from 'src/app/services/image.service';
import { DatetimeService } from 'src/app/services/datetime.service';

@Component({
  selector: 'app-advert-info',
  templateUrl: './advert-info.component.html',
  styleUrls: ['./advert-info.component.scss']
})
export class AdvertInfoComponent implements OnInit {

  public advert: AdvertModelInfo = new AdvertModelInfo(0, "", new Date(), "", "", "", new Date(), new Date(), 0, "", [], []);
  public days: AvailableDayModel[] = [];
  public month: number = 0;
  public year: number = 0;
  public page: string = '';
  private listRoute: string = '/advert-list';
  private myRoute: string = '/my-adverts';
  private editRoute: string = '/advert-edit';

  constructor(public datetimeService: DatetimeService, private advertService: AdvertService, private router: Router, public accountService: AccountService, private imageService: ImageService) { }

  public back(): void {
    if (this.page == 'list') {
      if(this.advertService.getQueryParametr() == "")
        this.router.navigateByUrl(this.listRoute);
      else
        this.router.navigate([this.listRoute], {
          queryParams: {
            search: this.advertService.getQueryParametr()
          }
        });
      return;
    }
    if (this.page == 'my') {
      if(this.advertService.getQueryParametr() == "")
        this.router.navigateByUrl(this.myRoute);
      else
      this.router.navigate([this.myRoute], {
        queryParams: {
          search: this.advertService.getQueryParametr()
        }
      });
      return;
    }
    this.router.navigateByUrl('/');
  }

  public packageToDayModel(): void {
    this.advert.publishDate = new Date(this.advert.publishDate);
    this.advert.editDate = new Date(this.advert.editDate);
    this.advert.dateIssue = new Date(this.advert.dateIssue);
    let day;
    let buffer;
    let sortTimes: AvailableTimeModel[];
    for (let j = 0; j < this.advert.availableTimes.length; j++) {
      buffer = new Date(this.advert.availableTimes[j].date);
      if (!this.contain(this.days, buffer)) {
        sortTimes = [];
        for (let i = 0; i < this.advert.availableTimes.length; i++) {
          day = new Date(this.advert.availableTimes[i].date);
          if (buffer.getDate() == day.getDate())
            sortTimes.push(this.advert.availableTimes[i]);
        }
        this.days.push(new AvailableDayModel(buffer, sortTimes));
      }
    }
    if (buffer != null) {
      this.month = buffer.getMonth();
      this.year = buffer.getFullYear();
    }
    this.convertToNormalDate();
    this.sortByDate();
  }

  public contain(list: AvailableDayModel[], elem: Date): Boolean {
    let firstFormat = '';
    let secondFormat = elem.getMonth() + '.' + elem.getDate() + '.' + elem.getFullYear();
    let flag = false;
    for (let count = 0; count < list.length; count++) {
      firstFormat = list[count].day.getMonth() + '.' + list[count].day.getDate() + '.' + list[count].day.getFullYear();
      if (firstFormat == secondFormat) {
        flag = true;
      }
    }
    return flag;
  }

  public convertToNormalDate(): void {
    for (let count_day = 0; count_day < this.days.length; count_day++) {
      for (let count_hour = 0; count_hour < this.days[count_day].times.length; count_hour++) {
        this.days[count_day].times[count_hour].date = new Date(this.days[count_day].times[count_hour].date);
      }
      this.days[count_day].times = this.datetimeService.sortByHour(this.days[count_day].times);
    }
  }

  public sortByDate(): void {
    let date;
    for (let i = 0; i < this.days.length; i++) {
      for (let j = 0; j < this.days.length - i - 1; j++) {
        if (this.days[j].day.getFullYear() > this.days[j + 1].day.getFullYear()) {
          date = this.days[j];
          this.days[j] = this.days[j + 1];
          this.days[j + 1] = date;
        }
        else if (this.days[j].day.getMonth() > this.days[j + 1].day.getMonth()) {
          date = this.days[j];
          this.days[j] = this.days[j + 1];
          this.days[j + 1] = date;
        }
        else if (this.days[j].day.getDate() > this.days[j + 1].day.getDate()) {
          date = this.days[j];
          this.days[j] = this.days[j + 1];
          this.days[j + 1] = date;
        }
      }
    }
  }

  public remove(): void {
    if (this.advert.id == 0) {
      alert("Ошибка удаления");
      return;
    }
    this.advertService.remove(this.advert.id).subscribe({
      next: async (data) => {
        alert(data);
        console.log(data);
        this.back();
        return;
      },
      error: (bad) => {
        alert("Ошибка удаления");
        console.log(bad);
        return;
      }
    });
  }

  public edit(): void {
    this.advertService.setIdInLocalStorage(this.advert.id);
    this.router.navigateByUrl(this.editRoute);
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    this.advertService.setAdvertUpdateInService(new AdvertModelUpdate(0, "", new Date(), "", "", "", 0, 0, [new ImageModel(0, "", "", 0)], new Date(), new Date(), 0, 0));
    this.imageService.setImagesInService([], []);
    this.page = this.advertService.getPageFromLocalStorage();
    await this.advertService.getById(this.advertService.getIdFromLocalStorage()).subscribe(data => {
      this.advert = data;
      this.packageToDayModel();
    });
  }

}
