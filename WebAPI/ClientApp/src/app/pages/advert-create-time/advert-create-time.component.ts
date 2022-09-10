import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { AvailableTimeModelCreate } from 'src/app/models/AvailableTimeModelCreate';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { ImageService } from 'src/app/services/image.service';

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
  public image: File | undefined;
  private createRoute = "/advert-create";
  private listRoute = "/my-adverts";

  constructor(private advertService: AdvertService, private router: Router, private accountService: AccountService, private imageService: ImageService) { }

  public create(): void {
    if(this.range.value.start == null || this.range.value.start == undefined) {
      alert("Выберите диапазон чисел");
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
    if(this.image == null || this.image == undefined)
    {
      alert("Не выбран файл");
      return;
    }
    let startHour = parseInt(this.startTime);
    let endHour = parseInt(this.endTime);
    if(startHour > endHour) {
      alert("Неверный диапазон времени");
      return;
    }
    let dates = this.getDatesInRange(this.range.value.start, this.range.value.end);
    this.fillAvailiableTime(dates, startHour, endHour);
    if(this.availiableTime == undefined || this.advert == undefined) {
      alert("Ошибка заполнения доступного времени");
      return;
    }
    this.advert.availableTimeModelsCreates = this.availiableTime;
    let formData = new FormData();
    formData.append('file', this.image);
    this.advertService.CreateAdvert(this.advert).subscribe(data => {
      if (data == "success") {
        console.log(data);
        this.imageService.Create(formData).subscribe(data => {
          if (data == "success") {
            console.log(data);
            alert(data);
            this.router.navigateByUrl(this.listRoute);
            return;
          }
          alert("Ошибка загрузки картинки");
          console.log(data);
          this.range.value.start = null;
          this.range.value.end = null;
          this.startTime = undefined;
          this.endTime = undefined;
          return;
        });
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

  public getDatesInRange(startDate: Date, endDate: Date): Date[] {
    let date = new Date(startDate.getTime());
    let dates = [];
    while(date <= endDate) {
      dates.push(new Date(date))
      date.setDate(date.getDate() + 1);
    }
    return dates;
  }

  public fillAvailiableTime(dates: Date[], startTime: number, endTime: number): void {
    let currenthour = startTime;
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
    await this.accountService.GetAuthoriseModel();
    this.advert = this.advertService.GetAdvertCreateFromService();
    if(this.advert.name == "")
      this.router.navigateByUrl(this.createRoute);
    this.image = this.imageService.GetImageFromService();
    if(this.image == undefined)
      this.router.navigateByUrl(this.createRoute);
  }

}
