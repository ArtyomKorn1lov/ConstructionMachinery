import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdvertModelUpdate } from 'src/app/models/AdvertModelUpdate';
import { ImageModel } from 'src/app/models/ImageModel';
import { AccountService } from 'src/app/services/account.service';
import { AdvertService } from 'src/app/services/advert.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { ImageService } from 'src/app/services/image.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-advert-edit',
  templateUrl: './advert-edit.component.html',
  styleUrls: ['./advert-edit.component.scss']
})
export class AdvertEditComponent implements OnInit {

  public advertUpdate: AdvertModelUpdate = new AdvertModelUpdate(0, "", new Date(), "", "", "", 0, 0, [new ImageModel(0, "", "", 0)], new Date(), new Date(), 0, 0);
  public dateIssure: string | undefined;
  public images: File[] = [];
  public filesBase64: string[] = [];
  public oldImageCount: number = 0;
  public invalidName: boolean = false;
  public messageName: string | undefined;
  public invalidDateIssure: boolean = false;
  public messageDateIssure: string | undefined;
  public invalidPTS: boolean = false;
  public messagePTS: string | undefined;
  public invalidVIN: boolean = false;
  public messageVIN: string | undefined;
  public invalidPice: boolean = false;
  public messagePice: string | undefined;
  public invalidImage: boolean = false;
  public messageImage: string | undefined;
  private myRoute: string = '/my-adverts';
  private targetRoute: string = "/advert-edit/time";

  constructor(private datetimeService: DatetimeService, private advertService: AdvertService, private router: Router,
    private accountService: AccountService, private imageService: ImageService, private tokenService: TokenService, private route: ActivatedRoute) { }

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

  public uploadImage(): void {
    document.getElementById("SelectImage")?.click();
  }

  public download(event: any): void {
    const file = event.target.files[0];
    const reader = new FileReader();
    this.images.push(file);
    reader.readAsDataURL(file);
    reader.onload = () => {
      if (reader.result != null)
        this.filesBase64.push(reader.result.toString());
    }
  }

  public resetValidFlag(): boolean {
    return false;
  }

  public numberOnly(event: any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  public removeFromDownloadImages(image: ImageModel): void {
    let index = this.advertUpdate.images.indexOf(image);
    if (index != -1) {
      this.advertUpdate.images[index].path = "";
      this.oldImageCount--;
    }
  }

  public removeFromUploadImages(fileBase64: string): void {
    let index = this.filesBase64.indexOf(fileBase64);
    if (index != -1) {
      this.filesBase64.splice(index, 1);
      this.images.splice(index, 1);
    }
  }

  private validateForm(): boolean {
    let valid = true;
    let toScroll = true;
    if (this.advertUpdate.name == undefined || this.advertUpdate.name.trim() == '') {
      this.messageName = "Введите название объявления";
      this.invalidName = true;
      this.advertUpdate.name = '';
      valid = false;
      if (toScroll) {
        document.getElementById("name")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.advertUpdate.price == undefined || this.advertUpdate.price == 0) {
      this.messagePice = "Введите стоимость часа работы";
      this.invalidPice = true;
      this.advertUpdate.price = 0;
      valid = false;
      if (toScroll) {
        document.getElementById("price")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.dateIssure == undefined || this.dateIssure.trim() == '') {
      this.messageDateIssure = "Введите год выпуска";
      this.invalidDateIssure = true;
      this.dateIssure = undefined;
      valid = false;
      if (toScroll) {
        document.getElementById("dateIssure")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.advertUpdate.pts == undefined || this.advertUpdate.pts.trim() == '') {
      this.messagePTS = "Введите ПТС или ПСМ";
      this.invalidPTS = true;
      this.advertUpdate.pts = '';
      valid = false;
      if (toScroll) {
        document.getElementById("pts")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.advertUpdate.vin == undefined || this.advertUpdate.vin.trim() == '') {
      this.messageVIN = "Введите VIN, номер кузова или SN";
      this.invalidVIN = true;
      this.advertUpdate.vin = '';
      valid = false;
      if (toScroll) {
        document.getElementById("vin")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if ((this.images == null || this.images == undefined || this.images.length < 1)
      && (this.advertUpdate.images == null || this.advertUpdate.images == undefined || this.advertUpdate.images.length < 1)) {
      this.messageImage = "Не выбран файл";
      this.invalidImage = true;
      valid = false;
      if (toScroll) {
        document.getElementById("images")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    return valid;
  }

  public async crossingToAvailiableTime(): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    if (!this.validateForm())
      return;
    if (this.dateIssure == undefined) {
      return;
    }
    const issure = new Date(this.dateIssure);
    this.advertUpdate.dateIssue = issure;
    this.advertService.setAdvertUpdateInService(this.advertUpdate);
    this.imageService.setImagesInService(this.images, this.filesBase64);
    this.imageService.setImageCountInService(this.oldImageCount);
    this.router.navigate([this.targetRoute], {
      queryParams: {
        backUrl: this.router.url,
        infoUrl: this.getBackUrl()
      }
    });
    return;
  }

  public fillData(advert: AdvertModelUpdate): void {
    this.advertUpdate = advert;
    this.dateIssure = this.advertUpdate.dateIssue.getFullYear() + "-" + this.datetimeService.convertDateToUTS(this.advertUpdate.dateIssue.getMonth() + 1) + "-" + this.datetimeService.convertDateToUTS(this.advertUpdate.dateIssue.getDate());
    this.images = this.imageService.getImagesFromService();
    this.filesBase64 = this.imageService.getBases64FromService();
    this.oldImageCount = this.imageService.getOldImageCountFromService();
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    const backUrl = this.getBackUrl();
    if (backUrl == undefined)
      this.router.navigateByUrl(this.myRoute);
    let advert = this.advertService.getAdvertUpdateFromService();
    if (advert.name == "") {
      this.advertService.setAdvertUpdateInService(new AdvertModelUpdate(0, "", new Date(), "", "", "", 0, 0, [new ImageModel(0, "", "", 0)], new Date(), new Date(), 0, 0));
      let id = 0;
      this.route.queryParams.subscribe(params => {
        id = params["id"];
      });
      await this.advertService.getForUpdate(id)
        .then(
          (data) => {
            this.advertUpdate = data;
            this.advertUpdate.dateIssue = new Date(this.advertUpdate.dateIssue);
            this.dateIssure = this.advertUpdate.dateIssue.getFullYear() + "-" + this.datetimeService.convertDateToUTS(this.advertUpdate.dateIssue.getMonth() + 1) + "-" + this.datetimeService.convertDateToUTS(this.advertUpdate.dateIssue.getDate());
            this.imageService.oldImageFlag = true;
            this.oldImageCount = this.advertUpdate.images.length;
            return;
          }
        )
        .catch(
          (error) => {
            console.log(error);
            return;
          }
        );
    }
    else
      this.fillData(advert);
  }

}
