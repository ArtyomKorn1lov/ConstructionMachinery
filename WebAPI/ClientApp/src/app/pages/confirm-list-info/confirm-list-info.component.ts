import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { RequestService } from 'src/app/services/request.service';
import { AvailabilityRequestModelForLandlord } from 'src/app/models/AvailabilityRequestModelForLandlord';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmModel } from 'src/app/models/ConfirmModel';
import { ImageModel } from 'src/app/models/ImageModel';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { DialogConfirmComponent } from 'src/app/components/dialog-confirm/dialog-confirm.component';
import { DialogNoticeComponent } from 'src/app/components/dialog-notice/dialog-notice.component';

@Component({
  selector: 'app-confirm-list-info',
  templateUrl: './confirm-list-info.component.html',
  styleUrls: ['./confirm-list-info.component.scss']
})
export class ConfirmListInfoComponent implements OnInit {

  public request: AvailabilityRequestModelForLandlord = new AvailabilityRequestModelForLandlord(0, "", "", "", "", 0, [new ImageModel(0, "", "", 0)], new Date(), new Date());
  public startRent: Date = new Date();
  public endRent: Date = new Date();
  public spinnerFlag = false;
  private privateAreaRoute: string = "private-area";
  private confirmListRoute = "confirm-list";

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

  public async confirm(state: number): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult) {
      this.router.navigate(["/authorize"]);
      return;
    }
    let dialogMessage = "Подтвердить данную заявку?";
    if (state == 2)
      dialogMessage = "Отказать в бронировании?"
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
              this.router.navigateByUrl(this.confirmListRoute);
              return;
            }
          )
          .catch(
            (error) => {
              this.spinnerFlag = false;
              const alertDialog = this.dialog.open(DialogNoticeComponent, { data: { message: "Ошибка подтверждения заявки" } });
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
    await this.requestService.getForLandLord(id)
      .then(
        (data) => {
          this.request = data;
          this.startRent = new Date(this.request.startRent);
          this.endRent = new Date(this.request.endRent);
          if (data != null)
            this.titleService.setTitle("Заявка от " + this.datetimeService.convertDateToUTS(this.startRent.getDate()) + "."
              + this.datetimeService.convertDateToUTS(this.startRent.getMonth() + 1) + "." + this.startRent.getFullYear()
              + " в " + this.datetimeService.convertDateToUTS(this.startRent.getHours()) + ":00");
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

}
