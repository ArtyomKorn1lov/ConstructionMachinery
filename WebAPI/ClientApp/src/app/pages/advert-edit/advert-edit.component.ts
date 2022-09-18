import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdvertModelUpdate } from 'src/app/models/AdvertModelUpdate';
import { ImageModel } from 'src/app/models/ImageModel';
import { AccountService } from 'src/app/services/account.service';
import { AdvertService } from 'src/app/services/advert.service';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-advert-edit',
  templateUrl: './advert-edit.component.html',
  styleUrls: ['./advert-edit.component.scss']
})
export class AdvertEditComponent implements OnInit {

  public advertUpdate: AdvertModelUpdate = new AdvertModelUpdate(0, "", "", 0, 0, [ new ImageModel(0, "", "", 0) ], new Date(), new Date(), 0, 0);
  public image: File | undefined;
  public fileBase64: string = "";
  private targetRoute: string = "/advert-edit/time";

  constructor(private advertService: AdvertService, private router: Router, private accountService: AccountService, private imageService: ImageService) { }

  public uploadImage(): void {
    document.getElementById("SelectImage")?.click();
  }

  public download(event: any): void {
    const file = event.target.files[0];
    const reader = new FileReader();
    this.image = file;
    reader.readAsDataURL(file);
    reader.onload = () => {
      if (reader.result != null)
        this.fileBase64 = reader.result.toString();
    }
  }

  public crossingToAvailiableTime(): void {
    if (this.advertUpdate.name == undefined || this.advertUpdate.name.trim() == '') {
      alert("Введите название объявления");
      this.advertUpdate.name = '';
      return;
    }
    if (this.advertUpdate.price == undefined || this.advertUpdate.price == 0) {
      alert("Введите стоимость часа работы");
      this.advertUpdate.price = 0;
      return;
    }
    if (this.image == null || this.image == undefined) {
      this.image = new File([""], "");
    }
    if (this.image != null || this.image != undefined) {
      this.imageService.oldImageFlag = false;
    }
    this.advertService.setAdvertUpdateInService(this.advertUpdate);
    this.imageService.setImageInService(this.image);
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public fillData(advert: AdvertModelUpdate): void {
    this.advertUpdate.name = advert.name;
    this.advertUpdate.description = advert.description;
    this.advertUpdate.price = advert.price;
    this.image = this.imageService.getImageFromService();
    const reader = new FileReader();
    reader.readAsDataURL(this.image);
    reader.onload = () => {
      if (reader.result != null)
        this.fileBase64 = reader.result.toString();
    }
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    let advert = this.advertService.getAdvertUpdateFromService();
    if(advert.name == "")
    {
      this.advertService.setAdvertUpdateInService(new AdvertModelUpdate(0, "", "", 0, 0, [ new ImageModel(0, "", "", 0) ], new Date(), new Date(), 0, 0));
      await this.advertService.getForUpdate(this.advertService.getIdFromLocalStorage()).subscribe(data => {
        this.advertUpdate = data;
        this.imageService.oldImageFlag = true;
      });
    }
    this.fillData(advert);
  }

}
