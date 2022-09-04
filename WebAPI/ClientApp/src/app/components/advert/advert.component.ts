import { Component, Input, OnInit } from '@angular/core';
import { AdvertModelList } from 'src/app/models/AdvertModelList';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.scss']
})
export class AdvertComponent implements OnInit {

  @Input() page: string | undefined;
  public advertList: AdvertModelList[] = [];
  private targetRoute: string = "advert-info";

  constructor(private advertService: AdvertService, private router: Router) { }

  public GetAdvertInfo(id: number) {
    this.advertService.SetIdInLocalStorage(id);
    if(this.page == undefined)
      this.advertService.SetPageInLocalStorage('list');
    else
      this.advertService.SetPageInLocalStorage(this.page);
    this.router.navigateByUrl(this.targetRoute);
    return;
  }

  public async ngOnInit(): Promise<void> {
    this.advertService.ClearLocalStorage();
    if(this.page == 'list')
    {
      if(this.advertService.searchFlag == true)
      {
        await this.advertService.GetByName(this.advertService.search).subscribe(data => {
          this.advertList = data;
          this.advertService.searchFlag = false;
        });
      }
      else
      {
        await this.advertService.GetAll().subscribe(data => {
          this.advertList = data;
        });
      }
    }
    if(this.page == 'my')
      await this.advertService.GetByUser().subscribe(data => {
        this.advertList = data;
      });
  }

}
