import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelForCustomer } from 'src/app/models/AvailabilityRequestModelForCustomer';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageModel } from 'src/app/models/ImageModel';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { DialogConfirmComponent } from 'src/app/components/dialog-confirm/dialog-confirm.component';
import { DialogNoticeComponent } from 'src/app/components/dialog-notice/dialog-notice.component';

@Component({
  selector: 'app-my-request-info',
  templateUrl: './my-request-info.component.html',
  styleUrls: ['./my-request-info.component.scss']
})
export class MyRequestInfoComponent implements OnInit {

  public request: AvailabilityRequestModelForCustomer = new AvailabilityRequestModelForCustomer(0, "", "", "", "", 0, 0, [new ImageModel(0, "", "", 0)], new Date(), new Date());
  public startRent: Date = new Date();
  public endRent: Date = new Date();
  public spinnerFlag = false;
  private advertRequestRoute = "advert-request";

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, public titleService: Title,
    private requestService: RequestService, private router: Router, private tokenService: TokenService, private route: ActivatedRoute, private dialog: MatDialog) {
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
    if (!tokenResult) {
      this.router.navigate(["/authorize"]);
      return;
    }
    let dialogMessage = "Вы уверены, что хотите отменить текущую заявку?";
    if (this.request.requestStateId == 2) {
      dialogMessage = "Удалить данную заявку?";
    }
    const confirmDialog = this.dialog.open(DialogConfirmComponent, { data: { message: dialogMessage } });
    confirmDialog.afterClosed().subscribe(async result => {
      if (result.flag) {
        this.spinnerFlag = true;
        this.requestService.remove(this.request.id)
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
              let messageAlert = "Ошибка отмены заявки";
              if (this.request.requestStateId == 2)
                messageAlert = "Ошибка очистки заявки";
              const alertDialog = this.dialog.open(DialogNoticeComponent, { data: { message: messageAlert } });
              console.log(error);
              return;
            }
          );
      }
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    this.spinnerFlag = false;
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
          this.startRent = new Date(this.request.startRent);
          this.endRent = new Date(this.request.endRent);
          if (data != null)
            this.titleService.setTitle("Заявка от " + this.datetimeService.convertDateToUTS(this.startRent.getDate()) + "." + this.datetimeService.convertDateToUTS(this.startRent.getMonth() + 1)
              + "." + this.startRent.getFullYear() + " в " + this.datetimeService.convertDateToUTS(this.startRent.getHours()) + ":00");
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

}
