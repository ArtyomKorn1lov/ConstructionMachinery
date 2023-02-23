import { Component, OnInit } from '@angular/core';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { AdvertService } from 'src/app/services/advert.service';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';
import { ImageService } from 'src/app/services/image.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-advert-create',
  templateUrl: './advert-create.component.html',
  styleUrls: ['./advert-create.component.scss']
})
export class AdvertCreateComponent implements OnInit {

  public name: string | undefined;
  public dateIssure: string | undefined;
  public pts: string | undefined;
  public vin: string | undefined;
  public description: string = "";
  public price: number | undefined;
  public images: File[] = [];
  public filesBase64: string[] = [];
  private targetRoute: string = "/advert-create/time";

  constructor(private datetimeService: DatetimeService, private advertService: AdvertService, private router: Router,
    private accountService: AccountService, private imageService: ImageService, private tokenService: TokenService) { }

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

  public async crossingToAvailiableTime(): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    if (this.name == undefined || this.name.trim() == '') {
      alert("Введите название объявления");
      this.name = '';
      return;
    }
    if (this.price == undefined || this.price == 0) {
      alert("Введите стоимость часа работы");
      this.price = undefined;
      return;
    }
    if (this.dateIssure == undefined || this.dateIssure.trim() == '') {
      alert("Введите год выпуска");
      this.dateIssure = undefined;
      return;
    }
    if (this.pts == undefined || this.pts.trim() == '') {
      alert("Введите ПТС или ПСМ");
      this.pts = '';
      return;
    }
    if (this.vin == undefined || this.vin.trim() == '') {
      alert("Введите VIN, номер кузова или SN");
      this.vin = '';
      return;
    }
    if (this.images == null || this.images == undefined) {
      alert("Не выбран файл");
      return;
    }
    const issure = new Date(this.dateIssure);
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
