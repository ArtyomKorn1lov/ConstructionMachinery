import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReviewModel } from 'src/app/models/ReviewModel';
import { ReviewService } from 'src/app/services/review.service';
import { AdvertService } from 'src/app/services/advert.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {

  public count: number = 4;
  public scrollFlag = true;
  public reviews: ReviewModel[] = [];

  constructor(private reviewService: ReviewService, private router: Router, private advertService: AdvertService) { }

  public convertToNormalDate(): void {
    for(let index = 0; index < this.reviews.length; index++) {
      this.reviews[index].date = new Date(this.reviews[index].date);
    }
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
      await this.reviewService.getByAdvertId(this.advertService.getIdFromLocalStorage(), this.count).subscribe(data => {
        this.reviews = data;
        this.convertToNormalDate();
        this.scrollFlag = this.reviewService.checkLenght(length, this.reviews.length);
        this.flagState();
      });
      this.count += 4;
    }
  };

  public async ngOnInit(): Promise<void> {
    window.addEventListener('scroll', this.scrollEvent, true);
    const firstCount = this.count;
    await this.reviewService.getByAdvertId(this.advertService.getIdFromLocalStorage(), this.count).subscribe(async data => {
      this.reviews = data;
      this.convertToNormalDate();
      await this.changeFlagState(this.reviews.length, firstCount);
    });
    this.count += 4;
  }

  public ngOnDestroy(): void {
    window.removeEventListener('scroll', this.scrollEvent, true);
  }

}
