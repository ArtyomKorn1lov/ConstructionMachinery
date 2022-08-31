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
  private targetRoute: string = "/advert-create/time";

  constructor(private advertService: AdvertService, private router: Router, private accountService: AccountService, private imageService: ImageService) { }

  public UploadImage(): void {
    document.getElementById("SelectImage")?.click();
  }

  public Download(event: any): void {
    this.image = event.target.files[0];
    console.log(this.image);
  }

  public CrossingToAvailiableTime(): void {
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
    if(this.image == null || this.image == undefined)
    {
      alert("Не выбран файл");
      return;
    }
    var advert = new AdvertModelCreate(this.name, this.description, this.price, 0, []);
    this.advertService.SetAdvertCreateInService(advert);
    this.imageService.SetImageInService(this.image);
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
    this.advertService.SetAdvertCreateInService(new AdvertModelCreate("", "", 0, 0, []));
  }

}
