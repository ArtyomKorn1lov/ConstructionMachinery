import { Component, OnInit } from '@angular/core';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { AdvertService } from 'src/app/services/advert.service';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-advert-create',
  templateUrl: './advert-create.component.html',
  styleUrls: ['./advert-create.component.scss']
})
export class AdvertCreateComponent implements OnInit {

  public name: string | undefined;
  public description: string = "";
  public price: number | undefined; 
  private targetRoute: string = "/advert-create/time"

  constructor(private advertService: AdvertService, private router: Router, private accountService: AccountService) { }

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
    var advert = new AdvertModelCreate(this.name, this.description, this.price, 0, []);
    this.advertService.SetAdvertCreateInService(advert);
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
    this.advertService.SetAdvertCreateInService(new AdvertModelCreate("", "", 0, 0, []));
  }

}
