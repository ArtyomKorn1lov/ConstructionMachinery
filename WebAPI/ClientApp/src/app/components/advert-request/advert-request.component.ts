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
  public count: number = 10;
  public scrollFlag = true;
  private confirmListRoute = "advert-confirm/confirm-list";
  private requestListRoute = "advert-request/my-requests";

  constructor(private advertService: AdvertService, private requestService: RequestService, private router: Router) { }

  public navigateToRequest(id: number): void {
    if (this.page == 'in') {
      this.requestService.setAdvertIdInLocalStorage(id);
      this.router.navigateByUrl(this.requestListRoute);
    }
    if (this.page == 'out') {
      this.requestService.setAdvertIdInLocalStorage(id);
      this.router.navigateByUrl(this.confirmListRoute);
    }
  }

  public scrollEvent = async (event: any): Promise<void> => {
    if (event.target.scrollingElement.offsetHeight + event.target.scrollingElement.scrollTop >= event.target.scrollingElement.scrollHeight) {
      const length = this.adverts.length;
      if (this.page == 'in')
        await this.advertService.getForRequestCustomer(this.count).subscribe(data => {
          this.adverts = data;
          this.scrollFlag = this.advertService.checkLenght(length, this.adverts.length);
          this.flagState();
        });
      if (this.page == 'out')
        await this.advertService.getForRequestLandlord(this.count).subscribe(data => {
          this.adverts = data;
          this.scrollFlag = this.advertService.checkLenght(length, this.adverts.length);
          this.flagState();
        });
      this.count += 10;
    }
  }

  public flagState(): void {
    if(this.scrollFlag == false) {
      this.count = 0;
      window.removeEventListener('scroll', this.scrollEvent, true);
    }
  }

  public async ngOnInit(): Promise<void> {
    window.addEventListener('scroll', this.scrollEvent, true);
    this.requestService.clearAdvertIdLocalStorage();
    if (this.page == 'in')
      await this.advertService.getForRequestCustomer(this.count).subscribe(data => {
        this.adverts = data;
      });
    if (this.page == 'out')
      await this.advertService.getForRequestLandlord(this.count).subscribe(data => {
        this.adverts = data;
      });
    this.count += 10;
  }

  public ngOnDestroy(): void {
    window.removeEventListener('scroll', this.scrollEvent, true);
  }

}
