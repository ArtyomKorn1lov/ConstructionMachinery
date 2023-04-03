import { Component, Injectable, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { ImageService } from 'src/app/services/image.service';
import { DateRange, MatDateRangeSelectionStrategy, MAT_DATE_RANGE_SELECTION_STRATEGY } from '@angular/material/datepicker';
import { DateAdapter } from '@angular/material/core';
import { TokenService } from 'src/app/services/token.service';
import { Title } from '@angular/platform-browser';


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
  public invalidRange: boolean = false;
  public messageRange: string | undefined;
  public startTime: string | undefined;
  public invalidStartTime: boolean = false;
  public messageStartTime: string | undefined;
  public endTime: string | undefined;
  public invalidEndTime: boolean = false;
  public messageEndTime: string | undefined;
  public images: File[] = [];
  private createRoute = "/advert-create";
  private listRoute = "/my-adverts";

  constructor(private advertService: AdvertService, private router: Router, private accountService: AccountService, public titleService: Title,
    private imageService: ImageService, private formBuilder: FormBuilder, private tokenService: TokenService) {
    this.titleService.setTitle("Новое объявление");
  }

  public resetValidFlag(): boolean {
    return false;
  }

  private validateForm(): boolean {
    let valid = true;
    let toScroll = true;
    if (this.range.controls.start.errors != null || this.range.controls.end.errors != null) {
      document.getElementById("datePicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
      toScroll = false;
      valid = false;
    }
    if (this.range.value.start == null || this.range.value.end == null) {
      this.messageRange = "Выберете дни аренды";
      this.invalidRange = true;
      valid = false;
      if (toScroll) {
        document.getElementById("datePicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.startTime == null || this.startTime == undefined) {
      this.messageStartTime = "Выберете диапазон времени";
      this.invalidStartTime = true;
      valid = false;
      if (toScroll) {
        document.getElementById("startPicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.endTime == null || this.endTime == undefined) {
      this.messageEndTime = "Выберете диапазон времени";
      this.invalidEndTime = true;
      valid = false;
      if (toScroll) {
        document.getElementById("endPicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    return valid;
  }

  private dateRangeValid(): boolean {
    if (this.range.value.start == null || this.range.value.end == null)
      return false;
    if (this.advert == null || this.advert == undefined)
      return false;
    let valid = true;
    let toScroll = true;
    this.advert.startDate = new Date(this.range.value.start);
    this.advert.endDate = new Date(this.range.value.end);
    const oneDay = 1000 * 60 * 60 * 24;
    const diffInTime = this.advert.endDate.getTime() - this.advert.startDate.getTime();
    const diffInDays = Math.round(diffInTime / oneDay);
    if (diffInDays <= 0) {
      this.messageRange = "Неверный диапазон дат";
      this.invalidRange = true;
      valid = false;
      if (toScroll) {
        document.getElementById("datePicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (diffInDays > 14) {
      this.messageRange = "Слишком большой диапазон занимаемых дней";
      this.invalidRange = true;
      valid = false;
      if (toScroll) {
        document.getElementById("datePicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    return valid;
  }

  private timeRangeValid(): boolean {
    if (this.advert == null || this.advert == undefined)
      return false;
    if (this.startTime == null || this.endTime == null)
      return false;
    let valid = true;
    let toScroll = true;
    let startHour = parseInt(this.startTime);
    let endHour = parseInt(this.endTime);
    if (startHour > endHour) {
      this.messageStartTime = "Неверный диапазон времени";
      this.invalidStartTime = true;
      this.messageEndTime = "Неверный диапазон времени";
      this.invalidEndTime = true;
      valid = false;
      if (toScroll) {
        document.getElementById("endPicker")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    else {
      this.advert.startTime = startHour;
      this.advert.endTime = endHour;
    }
    return valid;
  }

  public async create(): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    if (!this.validateForm())
      return;
    if (this.advert == null || this.advert == undefined)
      return;
    if (!this.dateRangeValid())
      return;
    if (!this.timeRangeValid())
      return;
    let formData = new FormData();
    Array.from(this.images).map((image, index) => {
      return formData.append('file' + index, image);
    });
    await this.advertService.createAdvert(this.advert)
      .then(
        (data) => {
          console.log(data);
        }
      )
      .catch(
        (error) => {
          alert("Ошибка создания объявления");
          console.log(error);
          this.range = this.formBuilder.group({
            start: new FormControl<Date | null>(null),
            end: new FormControl<Date | null>(null)
          });
          this.range.value.end = null;
          this.startTime = undefined;
          this.endTime = undefined;
          return;
        }
      );
    await this.imageService.create(formData)
      .then(
        (data) => {
          console.log(data);
          alert(data);
          this.router.navigateByUrl(this.listRoute);
          return;
        }
      )
      .catch(
        (error) => {
          alert("Ошибка загрузки картинки");
          console.log(error);
          this.range = this.formBuilder.group({
            start: new FormControl<Date | null>(null),
            end: new FormControl<Date | null>(null)
          });
          this.range.value.end = null;
          this.startTime = undefined;
          this.endTime = undefined;
          return;
        }
      );
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
