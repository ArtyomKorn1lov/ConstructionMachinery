import { Component, ElementRef, Input, OnInit, QueryList, ViewChildren } from '@angular/core';
import { AdvertModelList } from 'src/app/models/AdvertModelList';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { ImageService } from 'src/app/services/image.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { AccountService } from 'src/app/services/account.service';
import { TokenService } from 'src/app/services/token.service';
import { Observable, distinctUntilChanged, map, mergeMap } from 'rxjs';

@Component({
  selector: 'app-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.scss']
})
export class AdvertComponent implements OnInit {

  @Input() page: string | undefined;
  public advertList: AdvertModelList[] = [];
  public count: number = 10;
  public scrollFlag = true;
  private listRoute: string = "advert-list";
  private myRoute: string = "my-adverts";
  private filter: string = "all";
  @ViewChildren("lazySpinner") lazySpinner!: QueryList<ElementRef>;

  constructor(public datetimeService: DatetimeService, private advertService: AdvertService, private router: Router,
    private route: ActivatedRoute, private imageService: ImageService, public accountService: AccountService, private tokenService: TokenService) { }

  public async sortByParam(param: string): Promise<void> {
    this.filter = param;
    this.count = 10;
    this.scrollFlag = true;
    this.advertList = [];
  }

  public getAdvertInfo(id: number): void {
    if (this.page == 'my') {
      this.router.navigate([this.myRoute, id], {
        queryParams: { backUrl: this.router.url }
      });
    }
    else {
      this.router.navigate([this.listRoute, id], {
        queryParams: { backUrl: this.router.url }
      });
    }
  }

  public convertToNormalDate(): void {
    for (let count = 0; count < this.advertList.length; count++)
      this.advertList[count].editDate = new Date(this.advertList[count].editDate);
  }

  public async loadNewElements(): Promise<void> {
    length = this.advertList.length;
    if (this.page == 'list') {
      await this.route.queryParams.subscribe(async params => {
        const searchString = params['search'];
        if (searchString == undefined) {
          if (this.filter == "all")
            await this.advertService.getAll(this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "max_price")
            await this.advertService.getSortByPriceMax(this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "min_price")
            await this.advertService.getSortByPriceMin(this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "max_rating")
            await this.advertService.GetSortByRatingMax(this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "min_rating")
            await this.advertService.GetSortByRatingMin(this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "max_date")
            await this.advertService.getAll(this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "min_date")
            await this.advertService.GetSortByDateMin(this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
        }
        else {
          if (this.filter == "all")
            await this.advertService.getByName(searchString, this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "max_price")
            await this.advertService.getSortByPriceMaxByName(searchString, this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "min_price")
            await this.advertService.getSortByPriceMinByName(searchString, this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "max_rating")
            await this.advertService.GetSortByRatingMaxByName(searchString, this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "min_rating")
            await this.advertService.GetSortByRatingMinByName(searchString, this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "max_date")
            await this.advertService.getByName(searchString, this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
          if (this.filter == "min_date")
            await this.advertService.GetSortByDateMinByName(searchString, this.count)
              .then(
                (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                  this.changeFlagState();
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
        }
        this.advertService.advertLenght = this.advertList.length;
      });
    }
    if (this.page == 'my') {
      const tokenResult = await this.tokenService.tokenVerify();
      if (!tokenResult)
        this.router.navigate(["/authorize"]);
      if (this.filter == "all")
        await this.advertService.getByUser(this.count)
          .then(
            (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
              this.changeFlagState();
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
      if (this.filter == "max_price")
        await this.advertService.getSortByPriceMaxByUserId(this.count)
          .then(
            (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
              this.changeFlagState();
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
      if (this.filter == "min_price")
        await this.advertService.getSortByPriceMinByUserId(this.count)
          .then(
            (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
              this.changeFlagState();
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
      if (this.filter == "max_rating")
        await this.advertService.getSortByRatingMaxByUserId(this.count)
          .then(
            (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
              this.changeFlagState();
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
      if (this.filter == "min_rating")
        await this.advertService.getSortByRatingMinByUserId(this.count)
          .then(
            (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
              this.changeFlagState();
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
      if (this.filter == "max_date")
        await this.advertService.getByUser(this.count)
          .then(
            (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
              this.changeFlagState();
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
      if (this.filter == "min_date")
        await this.advertService.getSortByDateMinByUserId(this.count)
          .then(
            (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
              this.changeFlagState();
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
      this.advertService.advertLenght = this.advertList.length;
    }
    this.count += 10;
    this.convertToNormalDate();
  }

  public async changeFlagState(): Promise<void> {
    if (this.advertList.length < 10) {
      this.scrollFlag = false;
      this.flagState();
      return
    }
  }

  public flagState(): void {
    if (this.scrollFlag == false) {
      this.count = 0;
    }
  }

  public async ngOnInit(): Promise<void> {
    this.advertService.setAdvertCreateInService(new AdvertModelCreate("", new Date(), "", "", "", 0, 0, new Date(), new Date(), 0, 0));
    this.imageService.setImagesInService([], []);
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
