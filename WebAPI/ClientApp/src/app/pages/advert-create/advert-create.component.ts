import { Component, OnInit } from '@angular/core';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { AdvertService } from 'src/app/services/advert.service';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-advert-create',
  templateUrl: './advert-create.component.html',
  styleUrls: ['./advert-create.component.scss']
})
export class AdvertCreateComponent implements OnInit {

  public name: string | undefined;
  public description: string = "";
  public price: number | undefined;
  public image: File | undefined;
  public fileBase64: string = "";
  private targetRoute: string = "/advert-create/time";

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
    if (this.image == null || this.image == undefined) {
      alert("Не выбран файл");
      return;
    }
    let advert = new AdvertModelCreate(this.name, this.description, this.price, 0, new Date(), new Date(), 0, 0);
    this.advertService.SetAdvertCreateInService(advert);
    this.imageService.SetImageInService(this.image);
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
    this.advertService.SetAdvertCreateInService(new AdvertModelCreate("", "", 0, 0, new Date(), new Date(), 0, 0));
  }

}
