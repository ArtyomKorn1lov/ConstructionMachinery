import { Component, OnInit } from '@angular/core';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelInfo } from 'src/app/models/AdvertModelInfo';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';
import { AvailableDayModel } from 'src/app/models/AvailableDayModel';
import { AccountService } from 'src/app/services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageModel } from 'src/app/models/ImageModel';
import { AdvertModelUpdate } from 'src/app/models/AdvertModelUpdate';
import { ImageService } from 'src/app/services/image.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-advert-info',
  templateUrl: './advert-info.component.html',
  styleUrls: ['./advert-info.component.scss']
})
export class AdvertInfoComponent implements OnInit {

  public advert: AdvertModelInfo = new AdvertModelInfo(0, "", new Date(), "", "", "", new Date(), new Date(), 0, "", [], []);
  public days: AvailableDayModel[] = [];
  public month: number = 0;
  public year: number = 0;
  public page: string = '';
  private listRoute: string = '/advert-list';
  private myRoute: string = '/my-adverts';
  private editRoute: string = '/advert-edit';
  private leaseRoute: string = '/lease-registration';
  private reviewCreateRoute: string = '/review-create';

  constructor(public datetimeService: DatetimeService, private advertService: AdvertService, private router: Router,
    public accountService: AccountService, private imageService: ImageService, private tokenService: TokenService, private route: ActivatedRoute) { }

  public leaseRegistration(): void {
    this.router.navigate([this.leaseRoute], {
      queryParams: { 
        id: this.advert.id,
        backUrl: this.router.url 
      }
    });
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

  public addReview(): void {
    this.router.navigate([this.reviewCreateRoute], {
      queryParams: {
        id: this.advert.id,
        backUrl: this.router.url
      }
    });
  }

  public packageToDayModel(): void {
    this.advert.publishDate = new Date(this.advert.publishDate);
    this.advert.editDate = new Date(this.advert.editDate);
    this.advert.dateIssue = new Date(this.advert.dateIssue);
    if (this.advert.availableTimes == null)
      return;
    let day;
    let buffer;
    let sortTimes: AvailableTimeModel[];
    for (let j = 0; j < this.advert.availableTimes.length; j++) {
      buffer = new Date(this.advert.availableTimes[j].date);
      if (!this.contain(this.days, buffer)) {
        sortTimes = [];
        for (let i = 0; i < this.advert.availableTimes.length; i++) {
          day = new Date(this.advert.availableTimes[i].date);
          if (buffer.getDate() == day.getDate())
            sortTimes.push(this.advert.availableTimes[i]);
        }
        this.days.push(new AvailableDayModel(buffer, sortTimes));
      }
    }
    if (buffer != null) {
      this.month = buffer.getMonth();
      this.year = buffer.getFullYear();
    }
    this.convertToNormalDate();
    this.sortByDate();
  }

  public contain(list: AvailableDayModel[], elem: Date): Boolean {
    let firstFormat = '';
    let secondFormat = elem.getMonth() + '.' + elem.getDate() + '.' + elem.getFullYear();
    let flag = false;
    for (let count = 0; count < list.length; count++) {
      firstFormat = list[count].day.getMonth() + '.' + list[count].day.getDate() + '.' + list[count].day.getFullYear();
      if (firstFormat == secondFormat) {
        flag = true;
      }
    }
    return flag;
  }

  public convertToNormalDate(): void {
    for (let count_day = 0; count_day < this.days.length; count_day++) {
      for (let count_hour = 0; count_hour < this.days[count_day].times.length; count_hour++) {
        this.days[count_day].times[count_hour].date = new Date(this.days[count_day].times[count_hour].date);
      }
      this.days[count_day].times = this.datetimeService.sortByHour(this.days[count_day].times);
    }
  }

  public sortByDate(): void {
    let date;
    for (let i = 0; i < this.days.length; i++) {
      for (let j = 0; j < this.days.length - i - 1; j++) {
        if (this.days[j].day.getFullYear() > this.days[j + 1].day.getFullYear()) {
          date = this.days[j];
          this.days[j] = this.days[j + 1];
          this.days[j + 1] = date;
        }
        else if (this.days[j].day.getMonth() > this.days[j + 1].day.getMonth()) {
          date = this.days[j];
          this.days[j] = this.days[j + 1];
          this.days[j + 1] = date;
        }
        else if (this.days[j].day.getDate() > this.days[j + 1].day.getDate()) {
          date = this.days[j];
          this.days[j] = this.days[j + 1];
          this.days[j + 1] = date;
        }
      }
    }
  }

  public async remove(): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    if (this.advert.id == 0) {
      alert("Ошибка удаления");
      return;
    }
    await this.advertService.remove(this.advert.id)
      .then(
        (data) => {
          alert(data);
          console.log(data);
          this.back();
          return;
        }
      )
      .catch(
        (error) => {
          alert("Ошибка удаления");
          console.log(error);
          return;
        }
      );
  }

  public edit(): void {
    this.router.navigate([this.editRoute], {
      queryParams: {
        id: this.advert.id,
        backUrl: this.router.url
      }
    });
  }

  private verifyPreviosPage(backUrl: string): string {
    if (backUrl == undefined) {
      this.router.navigateByUrl(this.listRoute);
      return "list";
    }
    if (backUrl.includes(this.myRoute))
      return "my";
    return "list";
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    this.advertService.setAdvertUpdateInService(new AdvertModelUpdate(0, "", new Date(), "", "", "", 0, 0, [new ImageModel(0, "", "", 0)], new Date(), new Date(), 0, 0));
    this.imageService.setImagesInService([], []);
    let backUrl = this.getBackUrl();
    this.page = this.verifyPreviosPage(backUrl);
    let id = 0;
    this.route.params.subscribe(params => {
      id = params["id"];
    });
    await this.advertService.getById(id)
      .then(
        (data) => {
          this.advert = data;
          this.packageToDayModel();
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

}
