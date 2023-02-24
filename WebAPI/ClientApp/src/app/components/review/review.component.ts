import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  private reviewRoute: string = "/review-edit";
  private userRoute: string = "/user-profile";

  constructor(public datetimeService: DatetimeService, private reviewService: ReviewService, private router: Router,
    private advertService: AdvertService, private route: ActivatedRoute) { }

  public viewProfile(id: number) {
    this.router.navigate([this.userRoute], {
      queryParams: {
        id: id,
        backUrl: this.router.url
      }
    });
  }

  public convertToNormalDate(): void {
    for (let index = 0; index < this.reviews.length; index++) {
      this.reviews[index].date = new Date(this.reviews[index].date);
    }
  }

  public update(id: number): void {
    this.router.navigate([this.reviewRoute], {
      queryParams: {
        id: id,
        backUrl: this.router.url
      }
    });
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
      let id = 0;
      this.route.params.subscribe(params => {
        id = params["id"];
      });
      await this.reviewService.getByAdvertId(id, this.count)
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
    let id = 0;
    this.route.params.subscribe(params => {
      id = params["id"];
    });
    const firstCount = this.count;
    await this.reviewService.getByAdvertId(id, this.count)
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
