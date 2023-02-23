import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReviewModel } from 'src/app/models/ReviewModel';
import { ReviewService } from 'src/app/services/review.service';
import { AdvertService } from 'src/app/services/advert.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {

  public count: number = 4;
  public scrollFlag = true;
  public reviews: ReviewModel[] = [];
  private targetRoute: string = "/review-edit";
  private userRoute: string = "/user-profile";

  constructor(public datetimeService: DatetimeService, private reviewService: ReviewService, private router: Router,
    private advertService: AdvertService, private accountService: AccountService) { }

  public viewProfile(id: number) {
    this.accountService.setUserIdInLocalStorage(id);
    this.accountService.setPageInLocalStorage(this.router.url);
    this.router.navigateByUrl(this.userRoute);
  }

  public convertToNormalDate(): void {
    for (let index = 0; index < this.reviews.length; index++) {
      this.reviews[index].date = new Date(this.reviews[index].date);
    }
  }

  public update(id: number): void {
    this.reviewService.setIdInLocalStorage(id);
    this.router.navigateByUrl(this.targetRoute);
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

  public scrollEvent = async (event: any): Promise<void> => {
    if (event.target.scrollingElement.offsetHeight + event.target.scrollingElement.scrollTop >= event.target.scrollingElement.scrollHeight) {
      const length = this.reviews.length;
      await this.reviewService.getByAdvertId(this.advertService.getIdFromLocalStorage(), this.count)
        .then(
          (data) => {
            this.reviews = data;
            this.convertToNormalDate();
            this.scrollFlag = this.reviewService.checkLenght(length, this.reviews.length);
            this.flagState();
            this.count += 4;
          }
        )
        .catch((error) => {
          console.log(error);
        });
    }
  };

  public async ngOnInit(): Promise<void> {
    window.addEventListener('scroll', this.scrollEvent, true);
    const firstCount = this.count;
    await this.reviewService.getByAdvertId(this.advertService.getIdFromLocalStorage(), this.count)
      .then(
        async (data) => {
          this.reviews = data;
          this.convertToNormalDate();
          await this.changeFlagState(this.reviews.length, firstCount);
          this.count += 4;
        })
      .catch((error) => {
        console.log(error);
      });
  }

  public ngOnDestroy(): void {
    window.removeEventListener('scroll', this.scrollEvent, true);
  }

}
