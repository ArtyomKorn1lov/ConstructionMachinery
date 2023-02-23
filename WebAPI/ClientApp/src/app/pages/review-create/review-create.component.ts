import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReviewService } from 'src/app/services/review.service';
import { AdvertService } from 'src/app/services/advert.service';
import { AccountService } from 'src/app/services/account.service';
import { ReviewModelCreate } from 'src/app/models/ReviewModelCreate';

interface Stars {
  first: boolean;
  second: boolean;
  third: boolean;
  fourth: boolean;
  fifth: boolean;
};

@Component({
  selector: 'app-review-create',
  templateUrl: './review-create.component.html',
  styleUrls: ['./review-create.component.scss']
})
export class ReviewCreateComponent implements OnInit {

  public description: string = "";
  public stateStars: Stars = {
    first: false,
    second: false,
    third: false,
    fourth: false,
    fifth: false
  };
  private rating: number = 0;
  private targetRoute: string = "/advert-info";

  constructor(private router: Router, private reviewService: ReviewService, private advertService: AdvertService, private accountService: AccountService) { }

  public async create(): Promise<void> {
    if (this.rating <= 0 || this.rating > 5) {
      alert("Оцените данное объявление");
      this.rating = 0;
      return;
    }
    const review = new ReviewModelCreate(this.description, new Date(), this.rating, this.advertService.getIdFromLocalStorage(), 0);
    await this.reviewService.create(review)
    .then(
      (data) => {
        alert(data);
        console.log(data);
        this.router.navigateByUrl(this.targetRoute);
        return;
      }
    )
    .catch(
      (error) => {
        alert("Ошибка добавления отзыва");
        console.log(error);
        this.description = "";
        return;
      }
    );
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
    await this.accountService.getAuthoriseModel();
  }

}
