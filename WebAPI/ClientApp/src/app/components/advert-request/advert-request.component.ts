import { Component, ElementRef, OnInit, QueryList, ViewChildren } from '@angular/core';
import { AdvertModelForRequest } from 'src/app/models/AdvertModelForRequest';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';
import { RequestService } from 'src/app/services/request.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';
import { Observable, distinctUntilChanged, map, mergeMap } from 'rxjs';

@Component({
  selector: 'app-advert-request',
  templateUrl: './advert-request.component.html',
  styleUrls: ['./advert-request.component.scss']
})
export class AdvertRequestComponent implements OnInit {

  public adverts: AdvertModelForRequest[] = [];
  public pagination: number = 0;
  public scrollFlag = true;
  private requestListRoute = "advert-request/my-requests";
  @ViewChildren("lazySpinner") lazySpinner!: QueryList<ElementRef>;

  constructor(public datetimeService: DatetimeService, private advertService: AdvertService, private requestService: RequestService,
    private router: Router, private tokenService: TokenService) { }

  public navigateToRequest(id: number): void {
    this.router.navigate([this.requestListRoute], {
      queryParams: { id: id }
    });
  }

  public dateConvert(): void {
    for (let count = 0; count < this.adverts.length; count++) {
      this.adverts[count].editDate = new Date(this.adverts[count].editDate);
    }
  }

  public async loadNewElements(): Promise<void> {
    const length = this.adverts.length;
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    await this.advertService.getForRequestCustomer(this.pagination)
      .then(
        (data) => {
          this.adverts = this.adverts.concat(data);
          this.dateConvert();
          this.scrollFlag = this.advertService.checkLenght(length, this.adverts.length);
          this.changeFlagState();
          this.pagination++;
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

  public async changeFlagState(): Promise<void> {
    if (this.adverts.length < 10) {
      this.scrollFlag = false;
      this.flagState();
      return;
    }
    if (this.adverts.length % 10 != 0) {
      this.scrollFlag = false;
      this.flagState();
    }
  }

  public flagState(): void {
    if (this.scrollFlag == false) {
      this.pagination = 0;
    }
  }

  public async ngOnInit(): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
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
