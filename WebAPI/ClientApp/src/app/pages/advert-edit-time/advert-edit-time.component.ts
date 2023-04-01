import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { AdvertService } from 'src/app/services/advert.service';
import { ImageService } from 'src/app/services/image.service';
import { AdvertModelUpdate } from 'src/app/models/AdvertModelUpdate';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageModel } from 'src/app/models/ImageModel';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-advert-edit-time',
  templateUrl: './advert-edit-time.component.html',
  styleUrls: ['./advert-edit-time.component.scss']
})
export class AdvertEditTimeComponent implements OnInit {

  public advert: AdvertModelUpdate = new AdvertModelUpdate(0, "", new Date(), "", "", "", 0, 0, [], new Date(), new Date(), 0, 0);
  public range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });
  public invalidRange: boolean = false;
  public messageRange: string | undefined;
  public startTime: string = "";
  public invalidStartTime: boolean = false;
  public messageStartTime: string | undefined;
  public endTime: string = "";
  public invalidEndTime: boolean = false;
  public messageEndTime: string | undefined;
  public images: File[] = [];
  private myRoute: string = '/my-adverts';
  private updateRoute = "/advert-edit";

  constructor(private advertService: AdvertService, private router: Router, private accountService: AccountService,
    private imageService: ImageService, private formBuilder: FormBuilder, private tokenService: TokenService, private route: ActivatedRoute) { }

  public back(): void {
    let backUrl = this.getBackUrl();
    if (backUrl == undefined)
      backUrl = this.myRoute;
    this.router.navigateByUrl(backUrl);
  }

  private getBackUrl(): string {
    let backUrl = "";
    this.route.queryParams.subscribe(params => {
      backUrl = params["backUrl"];
    });
    return backUrl;
  }

  private getInfoUrl(): string {
    let infoUrl = "";
    this.route.queryParams.subscribe(params => {
      infoUrl = params["infoUrl"];
    });
    return infoUrl;
  }

  public prepareArrayId(images: ImageModel[]): number[] {
    let numberArray = [];
    for (let count = 0; count < images.length; count++) {
      if (images[count].path.trim() == "")
        numberArray.push(images[count].id);
    }
    return numberArray;
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

  public async update(): Promise<void> {
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
    let numberArray: number[] = []
    if (this.imageService.oldImageFlag)
      numberArray = this.prepareArrayId(this.advert.images);
    let formData = new FormData();
    Array.from(this.images).map((image, index) => {
      return formData.append('file' + index, image);
    });
    await this.advertService.update(this.advert)
      .then(
        (data) => {
          console.log(data);
        }
      )
      .catch(
        (error) => {
          alert("Ошибка редактирования объявления");
          console.log(error);
          this.range = this.formBuilder.group({
            start: new FormControl<Date | null>(null),
            end: new FormControl<Date | null>(null)
          });
          this.range.value.end = null;
          this.startTime = "";
          this.endTime = "";
          return;
        }
      );
    await this.imageService.remove(numberArray)
      .then(
        (data) => {
          console.log(data);
        }
      )
      .catch(
        (error) => {
          alert("Ошибка удаления картинки");
          console.log(error);
          this.range = this.formBuilder.group({
            start: new FormControl<Date | null>(null),
            end: new FormControl<Date | null>(null)
          });
          this.range.value.end = null;
          this.startTime = "";
          this.endTime = "";
          return;
        }
      );
    await this.imageService.update(formData, this.advert.id)
      .then(
        (data) => {
          console.log(data);
          alert(data);
          this.router.navigateByUrl(this.getInfoUrl());
          return;
        }
      )
      .catch(
        (error) => {
          alert("Ошибка обновления картинки");
          console.log(error);
          this.range = this.formBuilder.group({
            start: new FormControl<Date | null>(null),
            end: new FormControl<Date | null>(null)
          });
          this.range.value.end = null;
          this.startTime = "";
          this.endTime = "";
          return;
        }
      );
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    const backUrl = this.getBackUrl();
    if (backUrl == undefined)
      this.router.navigateByUrl(this.myRoute);
    const infoUrl = this.getInfoUrl();
    if (infoUrl == undefined)
      this.router.navigateByUrl(this.myRoute);
    this.advert = this.advertService.getAdvertUpdateFromService();
    this.images = this.imageService.getImagesFromService();
    if (this.advert.name == "")
      this.router.navigateByUrl(this.getBackUrl());
    this.range = this.formBuilder.group({
      start: this.advert.startDate,
      end: this.advert.endDate
    });
    this.startTime = this.advert.startTime.toString() + ":00";
    this.endTime = this.advert.endTime.toString() + ":00";
  }

}
