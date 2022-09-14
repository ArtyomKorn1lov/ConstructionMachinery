import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
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
  public range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  public startTime: string | undefined;
  public endTime: string | undefined;
  //public myDatePicker: string | undefined;
  public image: File | undefined;
  private createRoute = "/advert-create";
  private listRoute = "/my-adverts";

  constructor(private advertService: AdvertService, private router: Router, private accountService: AccountService, private imageService: ImageService, private formBuilder: FormBuilder) { }

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
    if(this.advert == null || this.advert == undefined) {
      alert("Ошибка формирования объявления, поля с предыдущей страницы равны нулю");
      return;
    }
    this.advert.startDate = new Date(this.range.value.start);
    this.advert.endDate = new Date(this.range.value.end);
    this.advert.startTime = startHour;
    this.advert.endTime = endHour;
    let formData = new FormData();
    formData.append('file', this.image);
    this.advertService.createAdvert(this.advert).subscribe(data => {
      if (data == "success") {
        console.log(data);
        this.imageService.create(formData).subscribe(data => {
          if (data == "success") {
            console.log(data);
            alert(data);
            this.router.navigateByUrl(this.listRoute);
            return;
          }
          alert("Ошибка загрузки картинки");
          console.log(data);
          this.range = this.formBuilder.group({
            start: new FormControl<Date | null>(null),
            end: new FormControl<Date | null>(null)
          });
          this.range.value.end = null;
          this.startTime = undefined;
          this.endTime = undefined;
          return;
        });
        return;
      }
      alert("Ошибка создания объявления");
      console.log(data);
      this.range = this.formBuilder.group({
        start: new FormControl<Date | null>(null),
        end: new FormControl<Date | null>(null)
      });
      this.startTime = undefined;
      this.endTime = undefined;
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    this.advert = this.advertService.getAdvertCreateFromService();
    /*if(this.advert.name == "")
      this.router.navigateByUrl(this.createRoute);
    this.image = this.imageService.getImageFromService();
    if(this.image == undefined)
      this.router.navigateByUrl(this.createRoute);*/
  }

}
