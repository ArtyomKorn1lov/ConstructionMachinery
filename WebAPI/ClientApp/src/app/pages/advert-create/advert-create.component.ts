import { Component, OnInit } from '@angular/core';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { AdvertService } from 'src/app/services/advert.service';
import { AccountService } from 'src/app/services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageService } from 'src/app/services/image.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-advert-create',
  templateUrl: './advert-create.component.html',
  styleUrls: ['./advert-create.component.scss']
})
export class AdvertCreateComponent implements OnInit {

  public name: string | undefined;
  public invalidName: boolean = false;
  public messageName: string | undefined;
  public dateIssure: string | undefined;
  public invalidDateIssure: boolean = false;
  public messageDateIssure: string | undefined;
  public pts: string | undefined;
  public invalidPTS: boolean = false;
  public messagePTS: string | undefined;
  public vin: string | undefined;
  public invalidVIN: boolean = false;
  public messageVIN: string | undefined;
  public description: string = "";
  public price: number | undefined;
  public invalidPice: boolean = false;
  public messagePice: string | undefined;
  public images: File[] = [];
  public filesBase64: string[] = [];
  public invalidImage: boolean = false;
  public messageImage: string | undefined;
  private myRoute: string = '/my-adverts';
  private targetRoute: string = "/advert-create/time";

  constructor(private datetimeService: DatetimeService, private advertService: AdvertService, private router: Router, public titleService: Title,
    private accountService: AccountService, private imageService: ImageService, private tokenService: TokenService, private route: ActivatedRoute) {
    this.titleService.setTitle("Новое объявление");
  }

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

  public numberOnly(event: any): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  public resetValidFlag(): boolean {
    return false;
  }

  private validateForm(): boolean {
    let valid = true;
    let toScroll = true;
    if (this.name == undefined || this.name.trim() == '') {
      this.messageName = "Введите название объявления";
      this.invalidName = true;
      this.name = '';
      valid = false;
      if (toScroll) {
        document.getElementById("name")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.price == undefined || this.price == 0) {
      this.messagePice = "Введите стоимость часа работы";
      this.invalidPice = true;
      this.price = undefined;
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
    if (this.pts == undefined || this.pts.trim() == '') {
      this.messagePTS = "Введите ПТС или ПСМ";
      this.invalidPTS = true;
      this.pts = '';
      valid = false;
      if (toScroll) {
        document.getElementById("pts")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.vin == undefined || this.vin.trim() == '') {
      this.messageVIN = "Введите VIN, номер кузова или SN";
      this.invalidVIN = true;
      this.vin = '';
      valid = false;
      if (toScroll) {
        document.getElementById("vin")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.images == null || this.images == undefined || this.images.length < 1) {
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
    if (this.dateIssure == undefined || this.name == undefined
      || this.pts == undefined || this.vin == undefined || this.price == undefined)
      return;
    const issure = new Date(this.dateIssure);
    this.description = this.description.trim();
    let advert = new AdvertModelCreate(this.name, issure, this.pts, this.vin, this.description, this.price, 0, new Date(), new Date(), 0, 0);
    this.advertService.setAdvertCreateInService(advert);
    this.imageService.setImagesInService(this.images, this.filesBase64);
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public removeFromUploadImages(fileBase64: string): void {
    let index = this.filesBase64.indexOf(fileBase64);
    if (index != -1) {
      this.filesBase64.splice(index, 1);
      this.images.splice(index, 1);
    }
  }

  public async fillData(advert: AdvertModelCreate): Promise<void> {
    this.name = advert.name;
    this.dateIssure = advert.dateIssue.getFullYear() + "-" + this.datetimeService.convertDateToUTS(advert.dateIssue.getMonth()) + "-" + this.datetimeService.convertDateToUTS(advert.dateIssue.getDate());
    this.pts = advert.pts;
    this.vin = advert.vin;
    this.description = advert.description;
    this.price = advert.price;
    this.images = this.imageService.getImagesFromService();
    this.filesBase64 = this.imageService.getBases64FromService();
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    let advert = this.advertService.getAdvertCreateFromService();
    if (advert.name == "") {
      this.advertService.setAdvertCreateInService(new AdvertModelCreate("", new Date(), "", "", "", 0, 0, new Date(), new Date(), 0, 0));
      return;
    }
    await this.fillData(advert);
  }

}
