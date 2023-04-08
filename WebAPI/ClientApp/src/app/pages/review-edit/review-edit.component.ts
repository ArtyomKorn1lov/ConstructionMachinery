import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReviewService } from 'src/app/services/review.service';
import { AdvertService } from 'src/app/services/advert.service';
import { ReviewModelUpdate } from 'src/app/models/ReviewModelUpdate';
import { AccountService } from 'src/app/services/account.service';
import { TokenService } from 'src/app/services/token.service';
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { DialogNoticeComponent } from 'src/app/components/dialog-notice/dialog-notice.component';
import { DialogConfirmComponent } from 'src/app/components/dialog-confirm/dialog-confirm.component';

interface Stars {
  first: boolean;
  second: boolean;
  third: boolean;
  fourth: boolean;
  fifth: boolean;
};

@Component({
  selector: 'app-review-edit',
  templateUrl: './review-edit.component.html',
  styleUrls: ['./review-edit.component.scss']
})
export class ReviewEditComponent implements OnInit {

  public stateStars: Stars = {
    first: false,
    second: false,
    third: false,
    fourth: false,
    fifth: false
  };
  public description: string = "";
  private review: ReviewModelUpdate = new ReviewModelUpdate(0, "", new Date(), 0, 0, 0);
  public invalidStars: boolean = false;
  public messageStars: string | undefined;
  public spinnerFlag = false;
  private rating: number = 0;
  private targetRoute: string = "/advert-info";
  private advertListRoute: string = "/advert-list";

  constructor(private router: Router, private reviewService: ReviewService, private advertService: AdvertService, public titleService: Title,
    private accountService: AccountService, private tokenService: TokenService, private route: ActivatedRoute, private dialog: MatDialog) {
    this.titleService.setTitle("Редактировать отзыв");
  }

  public back(): void {
    let backUrl = this.getBackUrl();
    if (backUrl == undefined)
      backUrl = this.advertListRoute;
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
    this.route.queryParams.subscribe(params => {
      id = params["id"];
    });
    if (id == undefined) {
      this.router.navigateByUrl("/");
      return 0;
    }
    return id;
  }

  public resetValidFlag(): boolean {
    return false;
  }

  private validateReview(): boolean {
    let valid = true;
    let toScroll = true;
    if (this.rating <= 0 || this.rating > 5) {
      this.messageStars = "Оцените данное объявление";
      this.invalidStars = true;
      this.rating = 0;
      valid = false;
      if (toScroll) {
        document.getElementById("star")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    return valid;
  }

  public async update(): Promise<void> {
    this.spinnerFlag = true;
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult) {
      this.spinnerFlag = false;
      this.router.navigate(["/authorize"]);
      return;
    }
    if (!this.validateReview()) {
      this.spinnerFlag = false;
      return;
    }
    this.review.description = this.description;
    this.review.date = new Date();
    this.review.reviewStateId = this.rating;
    await this.reviewService.update(this.review)
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
          const alertDialog = this.dialog.open(DialogNoticeComponent, { data: { message: "Ошибка обновления отзыва" } });
          console.log(error);
          return;
        }
      );
  }

  public async remove(): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult) {
      this.router.navigate(["/authorize"]);
      return;
    }
    const confirmDialog = this.dialog.open(DialogConfirmComponent, { data: { message: "Вы уверены, что хотите удалить данный отзыв?" } });
    confirmDialog.afterClosed().subscribe(async result => {
      if (result.flag) {
        this.spinnerFlag = true;
        await this.reviewService.remove(this.review.id)
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
              const alertDialog = this.dialog.open(DialogNoticeComponent, { data: { message: "Ошибка удаления отзыва" } });
              console.log(error);
              return;
            }
          );
      }
      return;
    });
  }

  public setState(index: number): void {
    if (index == 1) {
      this.stateStars.first = true;
      this.stateStars.second = false;
      this.stateStars.third = false;
      this.stateStars.fourth = false;
      this.stateStars.fifth = false;
      this.rating = 1;
      return;
    }
    if (index == 2) {
      this.stateStars.first = true;
      this.stateStars.second = true;
      this.stateStars.third = false;
      this.stateStars.fourth = false;
      this.stateStars.fifth = false;
      this.rating = 2;
      return;
    }
    if (index == 3) {
      this.stateStars.first = true;
      this.stateStars.second = true;
      this.stateStars.third = true;
      this.stateStars.fourth = false;
      this.stateStars.fifth = false;
      this.rating = 3;
      return;
    }
    if (index == 4) {
      this.stateStars.first = true;
      this.stateStars.second = true;
      this.stateStars.third = true;
      this.stateStars.fourth = true;
      this.stateStars.fifth = false;
      this.rating = 4;
      return;
    }
    if (index == 5) {
      this.stateStars.first = true;
      this.stateStars.second = true;
      this.stateStars.third = true;
      this.stateStars.fourth = true;
      this.stateStars.fifth = true;
      this.rating = 5;
      return;
    }
  }

  public removeState(index: number): void {
    if (index == 1) {
      this.stateStars.first = false;
      this.stateStars.second = false;
      this.stateStars.third = false;
      this.stateStars.fourth = false;
      this.stateStars.fifth = false;
      this.rating = 0;
      return;
    }
    if (index == 2) {
      this.stateStars.first = true;
      this.stateStars.second = false;
      this.stateStars.third = false;
      this.stateStars.fourth = false;
      this.stateStars.fifth = false;
      this.rating = 1;
      return;
    }
    if (index == 3) {
      this.stateStars.first = true;
      this.stateStars.second = true;
      this.stateStars.third = false;
      this.stateStars.fourth = false;
      this.stateStars.fifth = false;
      this.rating = 2;
      return;
    }
    if (index == 4) {
      this.stateStars.first = true;
      this.stateStars.second = true;
      this.stateStars.third = true;
      this.stateStars.fourth = false;
      this.stateStars.fifth = false;
      this.rating = 3;
      return;
    }
    if (index == 5) {
      this.stateStars.first = true;
      this.stateStars.second = true;
      this.stateStars.third = true;
      this.stateStars.fourth = true;
      this.stateStars.fifth = false;
      this.rating = 4;
      return;
    }
  }

  public async ngOnInit(): Promise<void> {
    this.spinnerFlag = false;
    await this.accountService.getAuthoriseModel();
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    const backUrl = this.getBackUrl();
    if (backUrl == undefined) {
      this.router.navigateByUrl("/");
      return;
    }
    const id = this.getIdByQueryParams();
    await this.reviewService.getById(id)
      .then(
        (data) => {
          this.review.id = data.id;
          this.review.description = data.description;
          this.review.reviewStateId = data.reviewStateId;
          this.review.advertId = data.advertId;
          this.rating = this.review.reviewStateId;
          this.setState(this.rating);
          this.description = this.review.description;
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

}
