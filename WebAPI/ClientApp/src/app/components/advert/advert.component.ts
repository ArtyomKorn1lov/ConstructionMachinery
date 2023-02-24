import { Component, Input, OnInit } from '@angular/core';
import { AdvertModelList } from 'src/app/models/AdvertModelList';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { ImageService } from 'src/app/services/image.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { TokenService } from 'src/app/services/token.service';

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
  private targetRoute: string = "advert-list";
  private filter: string = "all";

  constructor(public datetimeService: DatetimeService, private advertService: AdvertService, private router: Router,
    private route: ActivatedRoute, private imageService: ImageService, private tokenService: TokenService) { }

  public async sortByParam(param: string): Promise<void> {
    this.filter = param;
    this.count = 10;
    this.scrollFlag = true;
    await this.ngOnInit();
  }

  public getAdvertInfo(id: number): void {
    this.router.navigate([this.targetRoute, id], {
      queryParams: { backUrl: this.router.url }
    });
  }

  public convertToNormalDate(): void {
    for (let count = 0; count < this.advertList.length; count++)
      this.advertList[count].editDate = new Date(this.advertList[count].editDate);
  }

  public scrollEvent = async (event: any): Promise<void> => {
    if (event.target.scrollingElement.offsetHeight + event.target.scrollingElement.scrollTop >= event.target.scrollingElement.scrollHeight) {
      const length = this.advertList.length;
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
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
                    this.flagState();
                  }
                )
                .catch(
                  (error) => {
                    console.log(error);
                  }
                );
          }
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
                this.flagState();
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
                this.flagState();
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
                this.flagState();
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
                this.flagState();
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
                this.flagState();
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
                this.flagState();
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
                this.flagState();
              }
            )
            .catch(
              (error) => {
                console.log(error);
              }
            );
      }
      this.count += 10;
      this.convertToNormalDate();
    }
  };

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

  public async ngOnInit(): Promise<void> {
    window.addEventListener('scroll', this.scrollEvent, true);
    this.advertService.setAdvertCreateInService(new AdvertModelCreate("", new Date(), "", "", "", 0, 0, new Date(), new Date(), 0, 0));
    this.imageService.setImagesInService([], []);
    const firstCount = this.count;
    if (this.page == 'list') {
      await this.route.queryParams.subscribe(async params => {
        const searchString = params['search'];
        if (searchString == undefined) {
          if (this.filter == "all")
            await this.advertService.getAll(this.count)
              .then(
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
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
                async (data) => {
                  this.advertList = data;
                  this.convertToNormalDate();
                  await this.changeFlagState(this.advertList.length, firstCount);
                }
              )
              .catch(
                (error) => {
                  console.log(error);
                }
              );
        }
      });
    }
    if (this.page == 'my') {
      const tokenResult = await this.tokenService.tokenVerify();
      if (!tokenResult)
        this.router.navigate(["/authorize"]);
      if (this.filter == "all")
        await this.advertService.getByUser(this.count)
          .then(
            async (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              await this.changeFlagState(this.advertList.length, firstCount);
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
            async (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              await this.changeFlagState(this.advertList.length, firstCount);
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
            async (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              await this.changeFlagState(this.advertList.length, firstCount);
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
            async (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              await this.changeFlagState(this.advertList.length, firstCount);
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
            async (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              await this.changeFlagState(this.advertList.length, firstCount);
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
            async (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              await this.changeFlagState(this.advertList.length, firstCount);
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
            async (data) => {
              this.advertList = data;
              this.convertToNormalDate();
              await this.changeFlagState(this.advertList.length, firstCount);
            }
          )
          .catch(
            (error) => {
              console.log(error);
            }
          );
    }
    this.count += 10;
  }

  public ngOnDestroy(): void {
    window.removeEventListener('scroll', this.scrollEvent, true);
  }

}
