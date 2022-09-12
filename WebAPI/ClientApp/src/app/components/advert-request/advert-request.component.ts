import { Component, Input, OnInit } from '@angular/core';
import { AdvertModelForRequest } from 'src/app/models/AdvertModelForRequest';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';
import { RequestService } from 'src/app/services/request.service';

@Component({
  selector: 'app-advert-request',
  templateUrl: './advert-request.component.html',
  styleUrls: ['./advert-request.component.scss']
})
export class AdvertRequestComponent implements OnInit {

  @Input() page: string | undefined;
  public adverts: AdvertModelForRequest[] = [];
  private confirmListRoute = "advert-confirm/confirm-list";
  private requestListRoute = "advert-request/my-requests";

  constructor(private advertService: AdvertService, private requestService: RequestService, private router: Router) { }

  public navigateToRequest(id: number): void {
    if (this.page == 'in')
    {
      this.requestService.setAdvertIdInLocalStorage(id);
      this.router.navigateByUrl(this.requestListRoute);
    }
    if (this.page == 'out')
    {
      this.requestService.setAdvertIdInLocalStorage(id);
      this.router.navigateByUrl(this.confirmListRoute);
    }
  }

  public async ngOnInit(): Promise<void> {
    this.requestService.clearAdvertIdLocalStorage();
    if (this.page == 'in')
      await this.advertService.getForRequestCustomer().subscribe(data => {
        this.adverts = data;
      });
    if (this.page == 'out')
      await this.advertService.getForRequestLandlord().subscribe(data => {
        this.adverts = data;
      });
  }

}
