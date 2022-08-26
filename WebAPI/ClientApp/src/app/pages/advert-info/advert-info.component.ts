import { Component, OnInit } from '@angular/core';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelInfo } from 'src/app/models/AdvertModelInfo';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';
import { AvailableDayModel } from 'src/app/models/AvailableDayModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-advert-info',
  templateUrl: './advert-info.component.html',
  styleUrls: ['./advert-info.component.scss']
})
export class AdvertInfoComponent implements OnInit {

  public advert: AdvertModelInfo = new AdvertModelInfo(0, "", "", 0, "", []);
  //public times: AvailableTimeModel[] = [];
  public days: AvailableDayModel[] = [];
  public month: number = 0;
  public year: number = 0;
  public page: string = '';
  private listRoute: string = '/advert-list';
  private myRoute: string = '/my-adverts';

  constructor(private advertService: AdvertService, private router: Router) { }

  public Back(): void {
    if(this.page == 'list') {
      this.router.navigateByUrl(this.listRoute);
      return;
    }
    if(this.page == 'my') {
      this.router.navigateByUrl(this.myRoute);
      return;
    }
    this.router.navigateByUrl('/');
  }

  public SortByDay(): void {
    let day = 0;
    var buffer;
    var sortTimes: AvailableTimeModel[] = [];
    for (let count = 0; count < this.advert.availableTimes.length; count++) {
      buffer = new Date(this.advert.availableTimes[count].date);
      if (sortTimes.length != 0 && day != buffer.getDate()) {
        this.days.push(new AvailableDayModel(day, sortTimes));
        day = buffer.getDate();
        sortTimes = [];
      }
      if (day != buffer.getDate()) {
        day = buffer.getDate();
      }
      sortTimes.push(this.advert.availableTimes[count]);
    }
    this.days.push(new AvailableDayModel(day, sortTimes));
    if(buffer != null)
    {
      this.month = buffer.getMonth();
      this.year = buffer.getFullYear();
    }
    this.ConvertToNormalDate();
    console.log(this.days);
  }

  public ConvertToNormalDate(): void {
    for(let count_day = 0; count_day < this.days.length; count_day++) {
      for(let count_hour = 0; count_hour < this.days[count_day].times.length; count_hour++) {
        this.days[count_day].times[count_hour].date = new Date(this.days[count_day].times[count_hour].date);
      }
    }
  }

  public Remove(): void {
    if(this.advert.id == 0) {
      alert("Ошибка удаления");
      return;
    }
    this.advertService.Remove(this.advert.id).subscribe(data => {
      if(data == "success") {
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
    this.page = this.advertService.GetPageFromLocalStorage();
    await this.advertService.GetById(this.advertService.GetIdFromLocalStorage()).subscribe(data => {
      this.advert = data;
      this.SortByDay();
    });
    /*this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T09:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T10:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T11:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T12:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T13:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T14:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T15:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T16:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T17:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T09:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T10:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T11:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T12:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T13:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T14:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T15:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T16:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T17:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T09:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T10:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T11:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T12:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T13:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T14:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T15:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T16:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T17:00:00'), 1, 3));*/
  }

}
