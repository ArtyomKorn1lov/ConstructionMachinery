import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelForCustomer } from 'src/app/models/AvailabilityRequestModelForCustomer';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageModel } from 'src/app/models/ImageModel';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-my-request-info',
  templateUrl: './my-request-info.component.html',
  styleUrls: ['./my-request-info.component.scss']
})
export class MyRequestInfoComponent implements OnInit {

  public request: AvailabilityRequestModelForCustomer = new AvailabilityRequestModelForCustomer(0, "", "", "", "", 0, 0, [new ImageModel(0, "", "", 0)], []);
  public date: Date = new Date();
  private advertRequestRoute = "advert-request";

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, public titleService: Title,
    private requestService: RequestService, private router: Router, private tokenService: TokenService, private route: ActivatedRoute) {
    this.titleService.setTitle("Исходящая заявка");
  }

  public back(): void {
    const backUrl = this.getBackUrl();
    if (backUrl == undefined) {
      this.router.navigateByUrl(this.advertRequestRoute);
      return;
    }
    this.router.navigateByUrl(backUrl);
  }

  private getBackUrl(): string {
    let backUrl = "";
    this.route.queryParams.subscribe(params => {
      backUrl = params["backUrl"];
    });
    return backUrl;
  }

  private getIdByQueryParams(): number {
    let id = 0;
    this.route.params.subscribe(params => {
      id = params["id"];
    });
    if (id == undefined) {
      this.router.navigateByUrl(this.advertRequestRoute);
      return 0;
    }
    return id;
  }

  public async cancel(): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    this.requestService.remove(this.request.id)
      .then(
        (data) => {
          console.log(data);
          alert(data);
          this.router.navigateByUrl(this.getBackUrl());
          return;
        }
      )
      .catch(
        (error) => {
          alert("Ошибка отмены заявки");
          console.log(error);
          return;
        }
      );
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    const backUrl = this.getBackUrl();
    if (backUrl == undefined) {
      this.router.navigateByUrl(this.advertRequestRoute);
      return;
    }
    const id = this.getIdByQueryParams();
    await this.requestService.getForCustomer(id)
      .then(
        (data) => {
          this.request = data;
          this.date = new Date(this.request.availableTimeModels[0].date);
          if (data != null)
            this.titleService.setTitle("Заявка от " + this.datetimeService.convertDateToUTS(this.date.getDate()) + "." + this.datetimeService.convertDateToUTS(this.date.getMonth() + 1)
              + "." + this.date.getFullYear() + " в " + this.datetimeService.convertDateToUTS(this.date.getHours()) + ":00");
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

}
