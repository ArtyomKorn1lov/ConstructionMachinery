import { Component, ElementRef, OnInit, QueryList, ViewChildren } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReviewModel } from 'src/app/models/ReviewModel';
import { ReviewService } from 'src/app/services/review.service';
import { AdvertService } from 'src/app/services/advert.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { Observable, distinctUntilChanged, map, mergeMap } from 'rxjs';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {

  public pagination: number = 0;
  public scrollFlag = true;
  public reviews: ReviewModel[] = [];
  private reviewRoute: string = "/review-edit";
  private userRoute: string = "/user-profile";
  @ViewChildren("lazySpinner") lazySpinner!: QueryList<ElementRef>;

  constructor(public datetimeService: DatetimeService, private reviewService: ReviewService, private router: Router,
    private route: ActivatedRoute) { }

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

  public async changeFlagState(): Promise<void> {
    if (this.reviews.length < 4) {
      this.scrollFlag = false;
      this.flagState();
      return;
    }
    if (this.reviews.length % 4 != 0) {
      this.scrollFlag = false;
      this.flagState();
    }
  }

  public flagState(): void {
    if (this.scrollFlag == false) {
      this.pagination = 0;
    }
  }

  public async loadNewElements(): Promise<void> {
    const length = this.reviews.length;
    let id = 0;
    this.route.params.subscribe(params => {
      id = params["id"];
    });
    await this.reviewService.getByAdvertId(id, this.pagination)
      .then(
        (data) => {
          this.reviews = this.reviews.concat(data);
          this.convertToNormalDate();
          this.scrollFlag = this.reviewService.checkLenght(length, this.reviews.length);
          this.changeFlagState();
          this.pagination++;
        }
      )
      .catch((error) => {
        console.log(error);
      });
  };

  public async ngOnInit(): Promise<void> {
  }

  public async ngAfterViewInit(): Promise<void> {
    this.lazySpinner.forEach((view: ElementRef) =>
      this.createAndObserve(view).subscribe({
        next: async (response) => {
          if (response) {
            await this.loadNewElements();
          }
        }
      })
    );
  }

  private createAndObserve(element: ElementRef): Observable<boolean> {
    return new Observable((observer) => {
      const intersectionObserver = new IntersectionObserver((entries) => {
        observer.next(entries);
      });
      intersectionObserver.observe(element.nativeElement);
      return () => {
        intersectionObserver.disconnect();
      };
    }).pipe(
      mergeMap((entries: any) => entries),
      map((entry: any) => entry.isIntersecting),
      distinctUntilChanged()
    );
  }

}
