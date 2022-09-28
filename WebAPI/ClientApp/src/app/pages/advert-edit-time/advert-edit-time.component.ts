import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { AdvertService } from 'src/app/services/advert.service';
import { ImageService } from 'src/app/services/image.service';
import { AdvertModelUpdate } from 'src/app/models/AdvertModelUpdate';
import { Router } from '@angular/router';
import { ImageModel } from 'src/app/models/ImageModel';

@Component({
  selector: 'app-advert-edit-time',
  templateUrl: './advert-edit-time.component.html',
  styleUrls: ['./advert-edit-time.component.scss']
})
export class AdvertEditTimeComponent implements OnInit {

  public advert: AdvertModelUpdate = new AdvertModelUpdate(0, "", "", 0, 0, [], new Date(), new Date(), 0, 0);
  public range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  public startTime: string = "";
  public endTime: string = "";
  public images: File[] = [];
  private updateRoute = "/advert-edit";
  private infoRoute = "/advert-info";

  constructor(private advertService: AdvertService, private router: Router, private accountService: AccountService, private imageService: ImageService, private formBuilder: FormBuilder) { }

  public prepareArrayId(images: ImageModel[]): number[] {
    let numberArray = [];
    for (let count = 0; count < images.length; count++) {
      if (images[count].path == "")
        numberArray.push(images[count].id);
    }
    return numberArray;
  }

  public update(): void {
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
      this.images = [];
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
    let numberArray: number[] = []
    if (!this.imageService.oldImageFlag)
      numberArray = this.prepareArrayId(this.advert.images);
    let formData = new FormData();
    Array.from(this.images).map((image, index) => {
      return formData.append('file' + index, image);
    });
    this.advertService.update(this.advert).subscribe(data => {
      if (data == "success") {
        console.log(data);
        if (!this.imageService.oldImageFlag) {
          this.imageService.remove(numberArray).subscribe(data => {
            if (data == "success") {
              console.log(data);
              this.imageService.update(formData, this.advert.id).subscribe(data => {
                if (data == "success") {
                  console.log(data);
                  alert(data);
                  this.router.navigateByUrl(this.infoRoute);
                  return;
                }
                alert("Ошибка загрузки картинки");
                console.log(data);
                this.range = this.formBuilder.group({
                  start: new FormControl<Date | null>(null),
                  end: new FormControl<Date | null>(null)
                });
                this.range.value.end = null;
                this.startTime = "";
                this.endTime = "";
                return;
              });
              return;
            }
            alert("Ошибка удаления картинки");
            console.log(data);
            this.range = this.formBuilder.group({
              start: new FormControl<Date | null>(null),
              end: new FormControl<Date | null>(null)
            });
            this.range.value.end = null;
            this.startTime = "";
            this.endTime = "";
            return;
          });
          return;
        }
        return;
      }
      alert("Ошибка создания объявления");
      console.log(data);
      this.range = this.formBuilder.group({
        start: new FormControl<Date | null>(null),
        end: new FormControl<Date | null>(null)
      });
      this.startTime = "";
      this.endTime = "";
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    this.advert = this.advertService.getAdvertUpdateFromService();
    this.images = this.imageService.getImagesFromService();
    if (this.advert.name == "")
      this.router.navigateByUrl(this.updateRoute);
    this.range = this.formBuilder.group({
      start: this.advert.startDate,
      end: this.advert.endDate
    });
    this.startTime = this.advert.startTime.toString()+":00";
    this.endTime = this.advert.endTime.toString()+":00";
  }

}
