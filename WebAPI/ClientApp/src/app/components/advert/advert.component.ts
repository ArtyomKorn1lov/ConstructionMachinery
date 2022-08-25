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
  private infoRoute: string = "advert-list/info";

  constructor(private advertService: AdvertService, private router: Router) { }

  public GetAdvertInfo(id: number) {
    this.advertService.SetIdInLocalStorage(id);
    this.router.navigateByUrl(this.infoRoute);
    return;
  }

  public async ngOnInit(): Promise<void> {
    this.advertService.ClearLocalStorage();
    await this.advertService.GetAll().subscribe(data => {
      this.advertList = data;
    });
  }

}
