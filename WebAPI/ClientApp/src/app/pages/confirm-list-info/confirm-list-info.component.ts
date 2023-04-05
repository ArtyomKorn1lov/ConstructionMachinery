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

@Component({
  selector: 'app-confirm-list-info',
  templateUrl: './confirm-list-info.component.html',
  styleUrls: ['./confirm-list-info.component.scss']
})
export class ConfirmListInfoComponent implements OnInit {

  public request: AvailabilityRequestModelForLandlord = new AvailabilityRequestModelForLandlord(0, "", "", "", "", 0, [new ImageModel(0, "", "", 0)], []);
  public date: Date = new Date();
  public spinnerFlag = false;
  private privateAreaRoute: string = "private-area";
  private confirmListRoute = "confirm-list";

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, public titleService: Title,
    private requestService: RequestService, private router: Router, private tokenService: TokenService, private route: ActivatedRoute) {
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
    this.spinnerFlag = true;
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult) {
      this.spinnerFlag = false;
      this.router.navigate(["/authorize"]);
      return;
    }
    let model: ConfirmModel = new ConfirmModel(this.request.id, state);
    await this.requestService.confirm(model)
      .then(
        (data) => {
          this.spinnerFlag = false;
          console.log(data);
          alert(data);
          this.router.navigateByUrl(this.confirmListRoute);
          return;
        }
      )
      .catch(
        (error) => {
          this.spinnerFlag = false;
          alert("Ошибка подтверждения запроса");
          console.log(error);
          return;
        }
      );
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
          this.date = new Date(this.request.availableTimeModels[0].date);
          if (this.date != null)
            this.titleService.setTitle("Заявка от " + this.datetimeService.convertDateToUTS(this.date.getDate()) + "."
              + this.datetimeService.convertDateToUTS(this.date.getMonth() + 1) + "." + this.date.getFullYear()
              + " в " + this.datetimeService.convertDateToUTS(this.date.getHours()) + ":00");
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

}
