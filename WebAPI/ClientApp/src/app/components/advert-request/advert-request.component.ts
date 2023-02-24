import { Component, Input, OnInit } from '@angular/core';
import { AdvertModelForRequest } from 'src/app/models/AdvertModelForRequest';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';
import { RequestService } from 'src/app/services/request.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-advert-request',
  templateUrl: './advert-request.component.html',
  styleUrls: ['./advert-request.component.scss']
})
export class AdvertRequestComponent implements OnInit {

  public adverts: AdvertModelForRequest[] = [];
  public count: number = 10;
  public scrollFlag = true;
  private requestListRoute = "advert-request/my-requests";

  constructor(public datetimeService: DatetimeService, private advertService: AdvertService, private requestService: RequestService, 
    private router: Router, private tokenService: TokenService) { }

  public navigateToRequest(id: number): void {
    this.router.navigate([this.requestListRoute], {
      queryParams: { id: id }
    });
  }

  public dateConvert(): void {
    for (let count = 0; count < this.adverts.length; count++) {
      this.adverts[count].editDate = new Date(this.adverts[count].editDate);
    }
  }

  public scrollEvent = async (event: any): Promise<void> => {
    if (event.target.scrollingElement.offsetHeight + event.target.scrollingElement.scrollTop >= event.target.scrollingElement.scrollHeight) {
      const length = this.adverts.length;
      const tokenResult = await this.tokenService.tokenVerify();
      if (!tokenResult)
        this.router.navigate(["/authorize"]);
      await this.advertService.getForRequestCustomer(this.count)
        .then(
          (data) => {
            this.adverts = data;
            this.dateConvert();
            this.scrollFlag = this.advertService.checkLenght(length, this.adverts.length);
            this.flagState();
            this.count += 10;
          }
        )
        .catch(
          (error) => {
            console.log(error);
          }
        );
    }
  }

  public async changeFlagState(length: number, firstCount: number): Promise<void> {
    if (length < firstCount) {
      this.scrollFlag = false;
      this.flagState();
    }
  }

  public flagState(): void {
    if (this.scrollFlag == false) {
      this.count = 0;
      window.removeEventListener('scroll', this.scrollEvent, true);
    }
  }

  public async ngOnInit(): Promise<void> {
    window.addEventListener('scroll', this.scrollEvent, true);
    const firstCount = this.count;
    const tokenResult = await this.tokenService.tokenVerify();
      if (!tokenResult)
        this.router.navigate(["/authorize"]);
    await this.advertService.getForRequestCustomer(this.count)
      .then(
        async (data) => {
          this.adverts = data;
          this.dateConvert();
          await this.changeFlagState(this.adverts.length, firstCount);
          this.count += 10;
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

  public ngOnDestroy(): void {
    window.removeEventListener('scroll', this.scrollEvent, true);
  }

}
