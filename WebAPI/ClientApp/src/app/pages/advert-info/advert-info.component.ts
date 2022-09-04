import { Component, OnInit } from '@angular/core';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelInfo } from 'src/app/models/AdvertModelInfo';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';
import { AvailableDayModel } from 'src/app/models/AvailableDayModel';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';
import { ImageModel } from 'src/app/models/ImageModel';

@Component({
  selector: 'app-advert-info',
  templateUrl: './advert-info.component.html',
  styleUrls: ['./advert-info.component.scss']
})
export class AdvertInfoComponent implements OnInit {

  public advert: AdvertModelInfo = new AdvertModelInfo(0, "", "", 0, "", [ new ImageModel(0, "", "", 0) ], []);
  public days: AvailableDayModel[] = [];
  public month: number = 0;
  public year: number = 0;
  public page: string = '';
  private listRoute: string = '/advert-list';
  private myRoute: string = '/my-adverts';

  constructor(private advertService: AdvertService, private router: Router, public accountService: AccountService) { }

  public Back(): void {
    if (this.page == 'list') {
      this.router.navigateByUrl(this.listRoute);
      return;
    }
    if (this.page == 'my') {
      this.router.navigateByUrl(this.myRoute);
      return;
    }
    this.router.navigateByUrl('/');
  }

  public PackageToDayModel(): void {
    let day;
    var buffer;
    var sortTimes: AvailableTimeModel[];
    for (let j = 0; j < this.advert.availableTimes.length; j++) {
      buffer = new Date(this.advert.availableTimes[j].date);
      if (!this.Contain(this.days, buffer)) {
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
    this.ConvertToNormalDate();
    this.SortByDate();
  }

  public Contain(list: AvailableDayModel[], elem: Date): Boolean {
    var firstFormat = '';
    var secondFormat = elem.getMonth() + '.' + elem.getDate() + '.' + elem.getFullYear();
    var flag = false;
    for (let count = 0; count < list.length; count++) {
      firstFormat = list[count].day.getMonth() + '.' + list[count].day.getDate() + '.' + list[count].day.getFullYear();
      if (firstFormat == secondFormat) {
        flag = true;
      }
    }
    return flag;
  }

  public ConvertToNormalDate(): void {
    for (let count_day = 0; count_day < this.days.length; count_day++) {
      for (let count_hour = 0; count_hour < this.days[count_day].times.length; count_hour++) {
        this.days[count_day].times[count_hour].date = new Date(this.days[count_day].times[count_hour].date);
      }
      this.days[count_day].times = this.advertService.SortByHour(this.days[count_day].times);
    }
  }

  public SortByDate(): void {
    var date;
    for(let i = 0; i < this.days.length; i++) {
      for(let j = 0; j < this.days.length - i - 1; j++) {
        if(this.days[j].day.getFullYear() > this.days[j+1].day.getFullYear()) {
          date = this.days[j];
          this.days[j] = this.days[j+1];
          this.days[j+1] = date;
        }
        else if(this.days[j].day.getMonth() > this.days[j+1].day.getMonth()) {
          date = this.days[j];
          this.days[j] = this.days[j+1];
          this.days[j+1] = date;
        }
        else if(this.days[j].day.getDate() > this.days[j+1].day.getDate()) {
          date = this.days[j];
          this.days[j] = this.days[j+1];
          this.days[j+1] = date;
        }
      }
    }
  }

  public Remove(): void {
    if (this.advert.id == 0) {
      alert("Ошибка удаления");
      return;
    }
    this.advertService.Remove(this.advert.id).subscribe(data => {
      if (data == "success") {
        alert(data);
        console.log(data);
        this.Back();
        return;
      }
      alert("Ошибка удаления");
      console.log(data);
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
    this.page = this.advertService.GetPageFromLocalStorage();
    await this.advertService.GetById(this.advertService.GetIdFromLocalStorage()).subscribe(data => {
      this.advert = data;
      this.PackageToDayModel();
    });
  }

}
