import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { AvailableTimeModelCreate } from 'src/app/models/AvailableTimeModelCreate';
import { Router } from '@angular/router';

@Component({
  selector: 'app-advert-create-time',
  templateUrl: './advert-create-time.component.html',
  styleUrls: ['./advert-create-time.component.scss']
})
export class AdvertCreateTimeComponent implements OnInit {

  public advert: AdvertModelCreate | undefined;
  public availiableTime: AvailableTimeModelCreate[] = [];
  public range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  public startTime: string | undefined;
  public endTime: string | undefined;
  private createRoute = "/advert-create";
  private listRoute = "/my-adverts";

  constructor(private advertService: AdvertService, private router: Router) { }

  public Create(): void {
    if(this.range.value.start == null || this.range.value.start == undefined) {
      alert("Выберете диапазон чисел");
      return;
    }
    if(this.range.value.end == null || this.range.value.end == undefined) {
      alert("Выберете диапазон чисел");
      return;
    }
    if(this.startTime == null || this.startTime == undefined) {
      alert("Выберете диапазон времени");
      return;
    }
    if(this.endTime == null || this.endTime == undefined) {
      alert("Выберете диапазон времени");
      return;
    }
    var startHour = parseInt(this.startTime);
    var endHour = parseInt(this.endTime);
    if(startHour > endHour) {
      alert("Неверный диапазон времени");
      return;
    }
    var dates = this.GetDatesInRange(this.range.value.start, this.range.value.end);
    this.FillAvailiableTime(dates, startHour, endHour);
    if(this.availiableTime == [] || this.advert == undefined) {
      alert("Ошибка заполнения доступного времени");
      return;
    }
    this.advert.availableTimeModelsCreates = this.availiableTime;
    console.log(this.advert.availableTimeModelsCreates);
    this.advertService.CreateAdvert(this.advert).subscribe(data => {
      if (data == "success") {
        console.log(data);
        alert(data);
        this.router.navigateByUrl(this.listRoute);
        return;
      }
      alert("Ошибка создания объявления");
      console.log(data);
      this.range.value.start = null;
      this.range.value.end = null;
      this.startTime = undefined;
      this.endTime = undefined;
      return;
    });
  }

  public GetDatesInRange(startDate: Date, endDate: Date): Date[] {
    var date = new Date(startDate.getTime());
    var dates = [];
    while(date <= endDate) {
      dates.push(new Date(date))
      date.setDate(date.getDate() + 1);
    }
    return dates;
  }

  public FillAvailiableTime(dates: Date[], startTime: number, endTime: number): void {
    var currenthour = startTime;
    for(let count = 0; count < dates.length; count++) {
      while(currenthour <= endTime) {
        dates[count].setHours(currenthour);
        this.availiableTime.push(new AvailableTimeModelCreate(new Date(dates[count]), 0));
        currenthour++;
      }
      currenthour = startTime;
    }
  }

  public async ngOnInit(): Promise<void> {
    this.advert = this.advertService.GetAdvertCreateFromService();
    if(this.advert.name == "")
      this.router.navigateByUrl(this.createRoute);
  }

}
