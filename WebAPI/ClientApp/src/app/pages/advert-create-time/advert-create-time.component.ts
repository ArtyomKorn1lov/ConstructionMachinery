import { Component, Injectable, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { ImageService } from 'src/app/services/image.service';
import { DateRange, MatDateRangeSelectionStrategy, MAT_DATE_RANGE_SELECTION_STRATEGY } from '@angular/material/datepicker';
import { DateAdapter } from '@angular/material/core';


@Component({
  selector: 'app-advert-create-time',
  templateUrl: './advert-create-time.component.html',
  styleUrls: ['./advert-create-time.component.scss']
})
export class AdvertCreateTimeComponent implements OnInit {

  public advert: AdvertModelCreate | undefined;
  public range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  public startTime: string | undefined;
  public endTime: string | undefined;
  public images: File[] = [];
  private createRoute = "/advert-create";
  private listRoute = "/my-adverts";

  constructor(private advertService: AdvertService, private router: Router, private accountService: AccountService, private imageService: ImageService, private formBuilder: FormBuilder) { }

  public create(): void {
    if (this.range.value.start == null || this.range.value.start == undefined) {
      alert("Выберите диапазон чисел");
      return;
    }
    if (this.range.value.end == null || this.range.value.end == undefined) {
      alert("Выберете диапазон чисел");
      return;
    }
    if (this.startTime == null || this.startTime == undefined) {
      alert("Выберете диапазон времени");
      return;
    }
    if (this.endTime == null || this.endTime == undefined) {
      alert("Выберете диапазон времени");
      return;
    }
    if (this.images == null || this.images == undefined) {
      alert("Не выбран файл");
      return;
    }
    let startHour = parseInt(this.startTime);
    let endHour = parseInt(this.endTime);
    if (startHour > endHour) {
      alert("Неверный диапазон времени");
      return;
    }
    if (this.advert == null || this.advert == undefined) {
      alert("Ошибка формирования объявления, поля с предыдущей страницы равны нулю");
      return;
    }
    this.advert.startDate = new Date(this.range.value.start);
    this.advert.endDate = new Date(this.range.value.end);
    this.advert.startTime = startHour;
    this.advert.endTime = endHour;
    let formData = new FormData();
    Array.from(this.images).map((image, index) => {
      return formData.append('file' + index, image);
    });
    this.advertService.createAdvert(this.advert).subscribe({
      next: async (data) => {
        console.log(data);
        this.imageService.create(formData).subscribe({
          next: async (data) => {
            console.log(data);
            alert(data);
            this.router.navigateByUrl(this.listRoute);
            return;
          },
          error: (bad) => {
            alert("Ошибка загрузки картинки");
            console.log(bad);
            this.range = this.formBuilder.group({
              start: new FormControl<Date | null>(null),
              end: new FormControl<Date | null>(null)
            });
            this.range.value.end = null;
            this.startTime = undefined;
            this.endTime = undefined;
            return;
          }
        });
        return;
      },
      error: (bad) => {
        alert("Ошибка создания объявления");
        console.log(bad);
        this.range = this.formBuilder.group({
          start: new FormControl<Date | null>(null),
          end: new FormControl<Date | null>(null)
        });
        this.startTime = undefined;
        this.endTime = undefined;
        return;
      }
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    this.advert = this.advertService.getAdvertCreateFromService();
    if (this.advert.name == "")
      this.router.navigateByUrl(this.createRoute);
    this.images = this.imageService.getImagesFromService();
    if (this.images == undefined)
      this.router.navigateByUrl(this.createRoute);
  }

}
