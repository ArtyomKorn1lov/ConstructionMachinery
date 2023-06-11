import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogConfirmComponent } from 'src/app/components/dialog-confirm/dialog-confirm.component';
import { DialogNoticeComponent } from 'src/app/components/dialog-notice/dialog-notice.component';
import { AvailabilityRequestModelForLandlord } from 'src/app/models/AvailabilityRequestModelForLandlord';
import { ConfirmModel } from 'src/app/models/ConfirmModel';
import { ImageModel } from 'src/app/models/ImageModel';
import { AccountService } from 'src/app/services/account.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { RequestService } from 'src/app/services/request.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-confirmed-request-info',
  templateUrl: './confirmed-request-info.component.html',
  styleUrls: ['./confirmed-request-info.component.scss']
})
export class ConfirmedRequestInfoComponent implements OnInit {

  public request: AvailabilityRequestModelForLandlord = new AvailabilityRequestModelForLandlord(0, "", new Date(), new Date(),
    "", "", 0, "", "", 0, [new ImageModel(0, "", "", 0)], new Date(), new Date());
  public spinnerFlag = false;
  private privateAreaRoute: string = "private-area";
  private advertConfirmRoute = "my-adverts-confirmed";

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, public titleService: Title,
    private requestService: RequestService, private router: Router, private tokenService: TokenService, private route: ActivatedRoute, private dialog: MatDialog) {
    this.titleService.setTitle("Подтверждение заявки");
  }

  private getIdByQueryParams(): number {
    let id = 0;
    this.route.params.subscribe(params => {
      id = params["id"];
    });
    if (id == undefined) {
      this.router.navigateByUrl(this.privateAreaRoute);
      return 0;
    }
    return id;
  }

  public back(): void {
    const backUrl = this.getBackUrl();
    if (backUrl == undefined) {
      this.router.navigateByUrl(this.advertConfirmRoute);
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

  public async confirm(state: number): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult) {
      this.router.navigate(["/authorize"]);
      return;
    }
    let dialogMessage = "Отказать в бронировании?";
    const confirmDialog = this.dialog.open(DialogConfirmComponent, { data: { message: dialogMessage } });
    confirmDialog.afterClosed().subscribe(async result => {
      if (result.flag) {
        this.spinnerFlag = true;
        let model: ConfirmModel = new ConfirmModel(this.request.id, state);
        await this.requestService.confirm(model)
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
              const alertDialog = this.dialog.open(DialogNoticeComponent, { data: { message: "Ошибка редактирования заявки" } });
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
    const id = this.getIdByQueryParams();
    await this.requestService.getForLandLordConfirm(id)
      .then(
        (data) => {
          this.request = data;
          this.request.created = new Date(this.request.created);
          this.request.updated = new Date(this.request.updated);
          this.request.startRent = new Date(this.request.startRent);
          this.request.endRent = new Date(this.request.endRent);
          if (data != null)
            this.titleService.setTitle("Заявка от " + this.datetimeService.convertDateToUTS(this.request.startRent.getDate()) + "."
              + this.datetimeService.convertDateToUTS(this.request.startRent.getMonth() + 1) + "." + this.request.startRent.getFullYear()
              + " в " + this.datetimeService.convertDateToUTS(this.request.startRent.getHours()) + ":00");
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

}
