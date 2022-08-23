import { Component, Input, OnInit } from '@angular/core';
import { AdvertModelList } from 'src/app/models/AdvertModelList';
import { AdvertService } from 'src/app/services/advert.service';

@Component({
  selector: 'app-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.scss']
})
export class AdvertComponent implements OnInit {

  @Input() page: string | undefined;
  public advertList: AdvertModelList[] = [];

  constructor(private advertService: AdvertService) { }

  public async ngOnInit(): Promise<void> {
    this.advertList.push(new AdvertModelList(1, "Автовышка АПТ-32", 1200));
    this.advertList.push(new AdvertModelList(1, "Кран ТТ-22", 2000));
    this.advertList.push(new AdvertModelList(1, "Камаз АН246", 1500));
    this.advertList.push(new AdvertModelList(1, "Ямобур 65-36", 1900));
    /*await this.advertService.GetAll().subscribe(data => {
      this.advertList = data;
    });*/
  }

}
