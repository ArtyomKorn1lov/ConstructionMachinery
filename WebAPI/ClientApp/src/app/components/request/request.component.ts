import { Component, Input, OnInit } from '@angular/core';
import { AvailabilityRequestModel } from 'src/app/models/AvailabilityRequestModel';
import { RequestService } from 'src/app/services/request.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.scss']
})
export class RequestComponent implements OnInit {

  @Input() page: string | undefined;
  public requests: AvailabilityRequestModel[] = [];
  public count: number = 10;
  public scrollFlag = true;
  private confirmInfoRoute = "confirm-list";
  private requestInfoRoute = "advert-request/my-requests";
  private advertRequestRoute = "advert-request";

  constructor(public datetimeService: DatetimeService, private requestService: RequestService, private router: Router,
    private route: ActivatedRoute, private tokenService: TokenService) { }

  private getIdByQueryParams(): number {
    let id = 0;
    this.route.queryParams.subscribe(params => {
      id = params["id"];
    });
    if (id == undefined) {
      this.router.navigateByUrl(this.advertRequestRoute);
      return 0;
    }
    return id;
  }

  public navigateToInfo(id: number): void {
    if (this.page == 'in')
      this.router.navigate([this.requestInfoRoute, id], {
        queryParams: { backUrl: this.router.url }
      });
    if (this.page == 'out')
      this.router.navigate([this.confirmInfoRoute, id]);
  }

  public scrollEvent = async (event: any): Promise<void> => {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    if (event.target.scrollingElement.offsetHeight + event.target.scrollingElement.scrollTop >= event.target.scrollingElement.scrollHeight) {
      const length = this.requests.length;
      if (this.page == 'in') {
        await this.requestService.getListForCustomer(this.getIdByQueryParams(), this.count)
          .then(
            (data) => {
              this.requests = data;
              this.dateConvert();
              this.scrollFlag = this.requestService.checkLenght(length, this.requests.length);
              this.flagState();
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
      }
      if (this.page == 'out')
        await this.requestService.getListForLandlord(this.count)
          .then(
            (data) => {
              this.requests = data;
              this.dateConvert();
              this.scrollFlag = this.requestService.checkLenght(length, this.requests.length);
              this.flagState();
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
      this.count++;
    }
  }

  public dateConvert(): void {
    for (let count = 0; count < this.requests.length; count++) {
      this.requests[count].date = new Date(this.requests[count].date);
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
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    window.addEventListener('scroll', this.scrollEvent, true);
    const firstCount = this.count;
    if (this.page == 'in')
      await this.requestService.getListForCustomer(this.getIdByQueryParams(), this.count)
        .then(
          async (data) => {
            this.requests = data;
            await this.changeFlagState(this.requests.length, firstCount);
            this.dateConvert();
          }
        )
        .catch(
          (error) => {
            console.log(error);
          }
        );
    if (this.page == 'out')
      await this.requestService.getListForLandlord(this.count)
        .then(
          async (data) => {
            this.requests = data;
            await this.changeFlagState(this.requests.length, firstCount);
            this.dateConvert();
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
