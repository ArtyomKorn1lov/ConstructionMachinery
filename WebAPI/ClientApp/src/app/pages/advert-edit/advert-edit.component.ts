import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdvertModelUpdate } from 'src/app/models/AdvertModelUpdate';
import { ImageModel } from 'src/app/models/ImageModel';
import { AccountService } from 'src/app/services/account.service';
import { AdvertService } from 'src/app/services/advert.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-advert-edit',
  templateUrl: './advert-edit.component.html',
  styleUrls: ['./advert-edit.component.scss']
})
export class AdvertEditComponent implements OnInit {

  public advertUpdate: AdvertModelUpdate = new AdvertModelUpdate(0, "", new Date(), "", "",  "", 0, 0, [ new ImageModel(0, "", "", 0) ], new Date(), new Date(), 0, 0);
  public dateIssure: string | undefined;
  public images: File[] = [];
  public filesBase64: string[] = [];
  public oldImageCount: number = 0;
  private targetRoute: string = "/advert-edit/time";

  constructor(private datetimeService: DatetimeService, private advertService: AdvertService, private router: Router, private accountService: AccountService, private imageService: ImageService) { }

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

  public removeFromDownloadImages(image: ImageModel): void {
    let index = this.advertUpdate.images.indexOf(image);
    if(index != -1) {
      this.advertUpdate.images[index].path = "";
      this.oldImageCount--;
    }
  }

  public removeFromUploadImages(fileBase64: string): void {
    let index = this.filesBase64.indexOf(fileBase64);
    if(index != -1) {
      this.filesBase64.splice(index, 1);
      this.images.splice(index, 1);
    }
  }

  public crossingToAvailiableTime(): void {
    if (this.advertUpdate.name == undefined || this.advertUpdate.name.trim() == '') {
      alert("?????????????? ???????????????? ????????????????????");
      this.advertUpdate.name = '';
      return;
    }
    if (this.advertUpdate.price == undefined || this.advertUpdate.price == 0) {
      alert("?????????????? ?????????????????? ???????? ????????????");
      this.advertUpdate.price = 0;
      return;
    }
    if (this.dateIssure == undefined || this.dateIssure.trim() == '') {
      alert("?????????????? ?????? ??????????????");
      this.dateIssure = "";
      return;
    }
    if (this.advertUpdate.pts == undefined || this.advertUpdate.pts.trim() == '') {
      alert("?????????????? ?????? ?????? ??????");
      this.advertUpdate.pts = '';
      return;
    }
    if (this.advertUpdate.vin == undefined || this.advertUpdate.vin.trim() == '') {
      alert("?????????????? VIN, ?????????? ???????????? ?????? SN");
      this.advertUpdate.vin = '';
      return;
    }
    if (this.images == null || this.images == undefined) {
      this.images = [];
    }
    if (this.images != null || this.images != undefined) {
      this.imageService.oldImageFlag = false;
    }
    if ((this.images == null || this.images == undefined) && (this.advertUpdate.images == null || this.advertUpdate.images == undefined)) {
      alert("???? ???????????? ????????");
      return;
    }
    const issure = new Date(this.dateIssure);
    this.advertUpdate.dateIssue = issure;
    this.advertService.setAdvertUpdateInService(this.advertUpdate);
    this.imageService.setImagesInService(this.images, this.filesBase64);
    this.imageService.setImageCountInService(this.oldImageCount);
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public fillData(advert: AdvertModelUpdate): void {
    this.advertUpdate = advert;
    this.dateIssure = this.advertUpdate.dateIssue.getFullYear() + "-" + this.datetimeService.convertDateToUTS(this.advertUpdate.dateIssue.getMonth()) + "-" + this.datetimeService.convertDateToUTS(this.advertUpdate.dateIssue.getDate());
    this.images = this.imageService.getImagesFromService();
    this.filesBase64 = this.imageService.getBases64FromService();
    this.oldImageCount = this.imageService.getOldImageCountFromService();
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    let advert = this.advertService.getAdvertUpdateFromService();
    if(advert.name == "")
    {
      this.advertService.setAdvertUpdateInService(new AdvertModelUpdate(0, "", new Date(), "", "", "", 0, 0, [ new ImageModel(0, "", "", 0) ], new Date(), new Date(), 0, 0));
      await this.advertService.getForUpdate(this.advertService.getIdFromLocalStorage()).subscribe(data => {
        this.advertUpdate = data;
        this.dateIssure = this.advertUpdate.dateIssue.getFullYear() + "-" + this.datetimeService.convertDateToUTS(this.advertUpdate.dateIssue.getMonth()) + "-" + this.datetimeService.convertDateToUTS(this.advertUpdate.dateIssue.getDate());
        this.imageService.oldImageFlag = true;
        this.oldImageCount = this.advertUpdate.images.length;
        return;
      });
    }
    this.fillData(advert);
  }

}
