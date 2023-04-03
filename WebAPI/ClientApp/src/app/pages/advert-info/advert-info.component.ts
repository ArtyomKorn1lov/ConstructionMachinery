import { Component, OnInit } from '@angular/core';
import { AdvertService } from 'src/app/services/advert.service';
import { AvailableDayModel } from 'src/app/models/AvailableDayModel';
import { AccountService } from 'src/app/services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageModel } from 'src/app/models/ImageModel';
import { AdvertModelUpdate } from 'src/app/models/AdvertModelUpdate';
import { ImageService } from 'src/app/services/image.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';
import { AdvertModelDetail } from 'src/app/models/AdvertModelDetail';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-advert-info',
  templateUrl: './advert-info.component.html',
  styleUrls: ['./advert-info.component.scss']
})
export class AdvertInfoComponent implements OnInit {

  public advert: AdvertModelDetail = new AdvertModelDetail(0, "", new Date(), "", "", "", new Date(), new Date(), 0, "", [], []);
  public month: number = 0;
  public year: number = 0;
  public page: string = '';
  private listRoute: string = '/advert-list';
  private myRoute: string = '/my-adverts';
  private editRoute: string = '/advert-edit';
  private leaseRoute: string = '/lease-registration';
  private reviewCreateRoute: string = '/review-create';

  constructor(public datetimeService: DatetimeService, private advertService: AdvertService, private router: Router, public titleService: Title,
    public accountService: AccountService, private imageService: ImageService, private tokenService: TokenService, private route: ActivatedRoute) {
      this.titleService.setTitle("Информация об объявлении");
  }

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

  public convertToNormalDate(days: AvailableDayModel[]): AvailableDayModel[] {
    for (let count_day = 0; count_day < days.length; count_day++) {
      days[count_day].date = new Date(days[count_day].date);
      for (let count_hour = 0; count_hour < days[count_day].times.length; count_hour++) {
        days[count_day].times[count_hour].date = new Date(days[count_day].times[count_hour].date);
      }
    }
    return days;
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
    await this.advertService.getDetailAdvert(id)
      .then(
        (data) => {
          this.advert = data;
          this.advert.availableDays = this.convertToNormalDate(this.advert.availableDays);
          this.advert.publishDate = new Date(this.advert.publishDate);
          this.advert.editDate = new Date(this.advert.editDate);
          this.advert.dateIssue = new Date(this.advert.dateIssue);
          if (this.advert.name != null && this.advert.name.trim() != "")
            this.titleService.setTitle(this.advert.name);
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

}
