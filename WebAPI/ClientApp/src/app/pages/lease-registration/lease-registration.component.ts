import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertService } from 'src/app/services/advert.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelCreate } from 'src/app/models/AvailabilityRequestModelCreate';
import { AvailableTimeModelForCreateRequest } from 'src/app/models/AvailableTimeModelForCreateRequest';
import { ActivatedRoute, Router } from '@angular/router';
import { DatetimeService } from 'src/app/services/datetime.service';
import { AvailableDayModel } from 'src/app/models/AvailableDayModel';
import { TokenService } from 'src/app/services/token.service';
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { DialogNoticeComponent } from 'src/app/components/dialog-notice/dialog-notice.component';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';

@Component({
  selector: 'app-lease-registration',
  templateUrl: './lease-registration.component.html',
  styleUrls: ['./lease-registration.component.scss']
})
export class LeaseRegistrationComponent implements OnInit {

  public leaseTimes: AvailableDayModel[] = [];
  public lastTimes: AvailableTimeModel[] = [];
  public address: string = "";
  public invalidAddress: boolean = false;
  public messageAddress: string | undefined;
  public currentTimeIndex: number | undefined;
  public currentTimeId: number | undefined;
  public endTimeIndex: number | undefined;
  public invalidCurrentTimeId: boolean = false;
  public messageCurrentTimeId: string | undefined;
  public request: AvailabilityRequestModelCreate = new AvailabilityRequestModelCreate("", 0, 0, []);
  public spinnerFlag = false;
  private listRoute: string = '/advert-list';

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, private advertService: AdvertService, public titleService: Title,
    private requestService: RequestService, private router: Router, private tokenService: TokenService, private route: ActivatedRoute, private dialog: MatDialog) {
    this.titleService.setTitle("Оформление аренды");
  }

  public back(): void {
    let backUrl = this.getBackUrl();
    if (backUrl == undefined)
      backUrl = this.listRoute;
    this.router.navigateByUrl(backUrl);
  }

  private getBackUrl(): string {
    let backUrl = "";
    this.route.queryParams.subscribe(params => {
      backUrl = params["backUrl"];
    });
    return backUrl;
  }

  public resetValidFlag(): boolean {
    return false;
  }

  public createEndList(): void {
    if (this.currentTimeIndex == undefined) {
      return;
    }
    let index = this.leaseTimes[this.currentTimeIndex].times.findIndex(item => item.id == this.currentTimeId);
    this.lastTimes = this.leaseTimes[this.currentTimeIndex].times.slice(index);
    this.cutTimeData();
  }

  private cutTimeData(): void {
    let curItem = this.lastTimes[0];
    for (let count = 1; count < this.lastTimes.length; count++) {
      if ((this.lastTimes[count].date.getHours() - curItem.date.getHours()) > 1) {
        this.lastTimes = this.lastTimes.slice(0, count);
        return;
      }
      curItem = this.lastTimes[count];
    }
  }

  private validateRequest(): boolean {
    let valid = true;
    let toScroll = true;
    if (this.address == undefined || this.address.trim() == '') {
      this.messageAddress = "Введите адрес доставки";
      this.invalidAddress = true;
      this.address = '';
      valid = false;
      if (toScroll) {
        document.getElementById("address")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.currentTimeId == 0 || this.currentTimeId == undefined) {
      this.messageCurrentTimeId = "Выберете время доставки";
      this.invalidCurrentTimeId = true;
      valid = false;
      if (toScroll) {
        document.getElementById("time")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    return valid;
  }

  public async createRequest(): Promise<void> {
    this.spinnerFlag = true;
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult) {
      this.spinnerFlag = false;
      this.router.navigate(["/authorize"]);
      return;
    }
    if (!this.validateRequest()) {
      this.spinnerFlag = false;
      return;
    }
    if (this.currentTimeId == 0 || this.currentTimeId == undefined) {
      this.spinnerFlag = false;
      return;
    }
    if (this.endTimeIndex == 0 || this.endTimeIndex == undefined) {
      this.spinnerFlag = false;
      return;
    }
    this.lastTimes = this.lastTimes.slice(0, this.endTimeIndex + 1);
    this.spinnerFlag = false;
    let modelTime: AvailableTimeModelForCreateRequest[] = [];
    for(let count = 0; count < this.lastTimes.length; count++) {
      modelTime.push(new AvailableTimeModelForCreateRequest(this.lastTimes[count].id, 3));
    }
    this.request = new AvailabilityRequestModelCreate(this.address, 3, 0, modelTime);
    await this.requestService.create(this.request)
      .then(
        (data) => {
          this.spinnerFlag = false;
          console.log(data);
          this.router.navigateByUrl(this.getBackUrl());
          return;
        }
      )
      .catch(
        (error) => {
          this.spinnerFlag = false;
          const alertDialog = this.dialog.open(DialogNoticeComponent, { data: { message: "Ошибка запроса на аренду" } });
          console.log(error);
          this.address = '';
          return;
        }
      );
  }

  public convertToNormalDate(days: AvailableDayModel[]): AvailableDayModel[] {
    for (let count_day = 0; count_day < days.length; count_day++) {
      days[count_day].date = new Date(days[count_day].date);
      for (let count_hour = 0; count_hour < days[count_day].times.length; count_hour++) {
        days[count_day].times[count_hour].date = new Date(days[count_day].times[count_hour].date);
      }
    }
    return days;
  }

  public async ngOnInit(): Promise<void> {
    this.spinnerFlag = false;
    await this.accountService.getAuthoriseModel();
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    const backUrl = this.getBackUrl();
    if (backUrl == undefined) {
      this.router.navigateByUrl(this.listRoute);
      return;
    }
    let id = 0;
    this.route.queryParams.subscribe(params => {
      id = params["id"];
    });
    await this.requestService.getAvailableTimesByAdvertId(id)
      .then(
        (data) => {
          this.leaseTimes = this.convertToNormalDate(data);
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }
}
