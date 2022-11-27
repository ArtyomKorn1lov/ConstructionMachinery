import { Component, Input, OnInit } from '@angular/core';
import { AdvertModelList } from 'src/app/models/AdvertModelList';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { AdvertModelCreate } from 'src/app/models/AdvertModelCreate';
import { ImageService } from 'src/app/services/image.service';

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
  private targetRoute: string = "advert-info";
  private filter: string = "all";

  constructor(private advertService: AdvertService, private router: Router, private route: ActivatedRoute, private imageService: ImageService) { }

  public async sortByParam(param: string): Promise<void> {
    this.filter = param;
    this.count = 10;
    this.scrollFlag = true;
    await this.ngOnInit();
  }

  public getAdvertInfo(id: number): void {
    this.advertService.setIdInLocalStorage(id);
    if (this.page == undefined)
      this.advertService.setPageInLocalStorage('list');
    else
      this.advertService.setPageInLocalStorage(this.page);
    this.route.queryParams.subscribe(async params => {
      const searchString = params['search'];
      if (searchString != "")
        this.advertService.setQueryParametr(searchString);
    });
    this.advertService.setFilterInLocalStorage(this.filter);
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public scrollEvent = async (event: any): Promise<void> => {
    if (event.target.scrollingElement.offsetHeight + event.target.scrollingElement.scrollTop >= event.target.scrollingElement.scrollHeight) {
      const length = this.advertList.length;
      if (this.page == 'list') {
        await this.route.queryParams.subscribe(async params => {
          const searchString = params['search'];
          if (searchString == undefined) {
            if (this.filter == "all")
              await this.advertService.getAll(this.count).subscribe(data => {
                this.advertList = data;
                this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                this.flagState();
              });
            if (this.filter == "max_price")
              await this.advertService.getSortByPriceMax(this.count).subscribe(data => {
                this.advertList = data;
                this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                this.flagState();
              });
            if (this.filter == "min_price")
              await this.advertService.getSortByPriceMin(this.count).subscribe(data => {
                this.advertList = data;
                this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                this.flagState();
              });
            if (this.filter == "max_rating")
              await this.advertService.GetSortByRatingMin(this.count).subscribe(data => {
                this.advertList = data;
                this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                this.flagState();
              });
            if (this.filter == "min_rating")
              await this.advertService.GetSortByRatingMin(this.count).subscribe(data => {
                this.advertList = data;
                this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                this.flagState();
              });
            if (this.filter == "max_date")
              await this.advertService.getAll(this.count).subscribe(data => {
                this.advertList = data;
                this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                this.flagState();
              });
            if (this.filter == "min_date")
              await this.advertService.GetSortByDateMin(this.count).subscribe(data => {
                this.advertList = data;
                this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
                this.flagState();
              });
          }
          else
            await this.advertService.getByName(searchString, this.count).subscribe(data => {
              this.advertList = data;
              this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
              this.flagState();
            });
        });
      }
      if (this.page == 'my')
        await this.advertService.getByUser(this.count).subscribe(data => {
          this.advertList = data;
          this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
          this.flagState();
        });
      this.count += 10;
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
    if (this.advertService.getFilterFromLocalStorage() != '')
      this.filter = this.advertService.getFilterFromLocalStorage();
    this.advertService.clearLocalStorage();
    this.advertService.setAdvertCreateInService(new AdvertModelCreate("", new Date(), "", "", "", 0, 0, new Date(), new Date(), 0, 0));
    this.imageService.setImagesInService([], []);
    const firstCount = this.count;
    if (this.page == 'list') {
      await this.route.queryParams.subscribe(async params => {
        const searchString = params['search'];
        if (searchString == undefined) {
          if (this.filter == "all")
            await this.advertService.getAll(this.count).subscribe(async data => {
              this.advertList = data;
              await this.changeFlagState(this.advertList.length, firstCount);
            });
          if (this.filter == "max_price")
            await this.advertService.getSortByPriceMax(this.count).subscribe(async data => {
              this.advertList = data;
              await this.changeFlagState(this.advertList.length, firstCount);
            });
          if (this.filter == "min_price")
            await this.advertService.getSortByPriceMin(this.count).subscribe(async data => {
              this.advertList = data;
              await this.changeFlagState(this.advertList.length, firstCount);
            });
          if (this.filter == "max_rating")
            await this.advertService.GetSortByRatingMax(this.count).subscribe(async data => {
              this.advertList = data;
              await this.changeFlagState(this.advertList.length, firstCount);
            });
          if (this.filter == "min_rating")
            await this.advertService.GetSortByRatingMin(this.count).subscribe(async data => {
              this.advertList = data;
              await this.changeFlagState(this.advertList.length, firstCount);
            });
          if (this.filter == "max_date")
            await this.advertService.getAll(this.count).subscribe(async data => {
              this.advertList = data;
              await this.changeFlagState(this.advertList.length, firstCount);
            });
          if (this.filter == "min_date")
            await this.advertService.GetSortByDateMin(this.count).subscribe(async data => {
              this.advertList = data;
              await this.changeFlagState(this.advertList.length, firstCount);
            });
        }
        else
          if (this.filter == "all")
            await this.advertService.getByName(searchString, this.count).subscribe(async data => {
              this.advertList = data;
              await this.changeFlagState(this.advertList.length, firstCount);
            });
        if (this.filter == "max_price")
          await this.advertService.getSortByPriceMaxByName(searchString, this.count).subscribe(async data => {
            this.advertList = data;
            await this.changeFlagState(this.advertList.length, firstCount);
          });
        if (this.filter == "min_price")
          await this.advertService.getSortByPriceMinByName(searchString, this.count).subscribe(async data => {
            this.advertList = data;
            await this.changeFlagState(this.advertList.length, firstCount);
          });
        if (this.filter == "max_rating")
          await this.advertService.GetSortByRatingMaxByName(searchString, this.count).subscribe(async data => {
            this.advertList = data;
            await this.changeFlagState(this.advertList.length, firstCount);
          });
        if (this.filter == "min_rating")
          await this.advertService.GetSortByRatingMinByName(searchString, this.count).subscribe(async data => {
            this.advertList = data;
            await this.changeFlagState(this.advertList.length, firstCount);
          });
        if (this.filter == "max_date")
          await this.advertService.getByName(searchString, this.count).subscribe(async data => {
            this.advertList = data;
            await this.changeFlagState(this.advertList.length, firstCount);
          });
        if (this.filter == "min_date")
          await this.advertService.GetSortByDateMinByName(searchString, this.count).subscribe(async data => {
            this.advertList = data;
            await this.changeFlagState(this.advertList.length, firstCount);
          });
      });
    }
    if (this.page == 'my')
      await this.advertService.getByUser(this.count).subscribe(async data => {
        this.advertList = data;
        await this.changeFlagState(this.advertList.length, firstCount);
      });
    this.count += 10;
  }

  public ngOnDestroy(): void {
    window.removeEventListener('scroll', this.scrollEvent, true);
  }

}
