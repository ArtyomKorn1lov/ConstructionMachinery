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

  constructor(private advertService: AdvertService, private router: Router, private route: ActivatedRoute, private imageService: ImageService) { }

  public async sortByParam(param: string): Promise<void> {
    if (param == "all") {
      this.count = 10;
      this.scrollFlag = true;
      await this.ngOnInit();
      return;
    }
    if (param == "max") {
      this.advertList.sort(this.isMax);
      return;
    }
    if (param == "min") {
      this.advertList.sort(this.isMin);
      return;
    }
  }

  private isMax(first: AdvertModelList, second: AdvertModelList): number {
    if (first.price > second.price)
      return 1;
    if (first.price < second.price)
      return -1;
    return 0;
  }

  private isMin(first: AdvertModelList, second: AdvertModelList): number {
    if (first.price < second.price)
      return 1;
    if (first.price > second.price)
      return -1;
    return 0;
  }

  public getAdvertInfo(id: number): void {
    this.advertService.setIdInLocalStorage(id);
    if (this.page == undefined)
      this.advertService.setPageInLocalStorage('list');
    else
      this.advertService.setPageInLocalStorage(this.page);
    this.route.queryParams.subscribe(async params => {
      const searchString = params['search'];
      if(searchString != "")
        this.advertService.setQueryParametr(searchString);
    });
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public scrollEvent = async (event: any): Promise<void> => {
    if (event.target.scrollingElement.offsetHeight + event.target.scrollingElement.scrollTop >= event.target.scrollingElement.scrollHeight) {
      const length = this.advertList.length;
      if (this.page == 'list') {
        await this.route.queryParams.subscribe(async params => {
          const searchString = params['search'];
          if (searchString == undefined)
            await this.advertService.getAll(this.count).subscribe(data => {
              this.advertList = data;
              this.scrollFlag = this.advertService.checkLenght(length, this.advertList.length);
              this.flagState();
            });
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
    this.advertService.clearLocalStorage();
    this.advertService.setAdvertCreateInService(new AdvertModelCreate("", "", 0, 0, new Date(), new Date(), 0, 0));
    this.imageService.setImagesInService([], []);
    const firstCount = this.count;
    if (this.page == 'list') {
      await this.route.queryParams.subscribe(async params => {
        const searchString = params['search'];
        if (searchString == undefined)
          await this.advertService.getAll(this.count).subscribe(async data => {
            this.advertList = data;
            await this.changeFlagState(this.advertList.length, firstCount);
          });
        else
          await this.advertService.getByName(searchString, this.count).subscribe(async data => {
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
