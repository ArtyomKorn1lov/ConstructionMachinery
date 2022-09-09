import { Component, Input, OnInit } from '@angular/core';
import { AdvertModelList } from 'src/app/models/AdvertModelList';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.scss']
})
export class AdvertComponent implements OnInit {

  @Input() page: string | undefined;
  public advertList: AdvertModelList[] = [];
  private targetRoute: string = "advert-info";

  constructor(private advertService: AdvertService, private router: Router, private route: ActivatedRoute) { }

  public async SortByParam(param: string): Promise<void> {
    if (param == "all") {
      await this.ngOnInit();
      return;
    }
    if (param == "max") {
      this.SortByMax();
      return;
    }
    if (param == "min") {
      this.SortByMin();
      return;
    }
  }

  public SortByMax(): void {
    var advert;
    for (let i = 0; i < this.advertList.length; i++) {
      for (let j = 0; j < this.advertList.length - i - 1; j++) {
        if (this.advertList[j].price > this.advertList[j + 1].price) {
          advert = this.advertList[j];
          this.advertList[j] = this.advertList[j + 1];
          this.advertList[j + 1] = advert;
        }
      }
    }
  }

  public SortByMin(): void {
    var advert;
    for (let i = 0; i < this.advertList.length; i++) {
      for (let j = 0; j < this.advertList.length - i - 1; j++) {
        if (this.advertList[j].price < this.advertList[j + 1].price) {
          advert = this.advertList[j];
          this.advertList[j] = this.advertList[j + 1];
          this.advertList[j + 1] = advert;
        }
      }
    }
  }

  public GetAdvertInfo(id: number): void {
    this.advertService.SetIdInLocalStorage(id);
    if (this.page == undefined)
      this.advertService.SetPageInLocalStorage('list');
    else
      this.advertService.SetPageInLocalStorage(this.page);
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public async ngOnInit(): Promise<void> {
    this.advertService.ClearLocalStorage();
    if (this.page == 'list') {
      this.route.queryParams.subscribe(async params => {
        const searchString = params['search'];
        if (searchString == undefined)
          await this.advertService.GetAll().subscribe(data => {
            this.advertList = data;
          });
        else
          await this.advertService.GetByName(searchString).subscribe(data => {
            this.advertList = data;
          });
      });
    }
    if (this.page == 'my')
      await this.advertService.GetByUser().subscribe(data => {
        this.advertList = data;
      });
  }

}
