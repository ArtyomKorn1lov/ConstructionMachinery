import { Component, OnInit } from '@angular/core';
import { AdvertModelList } from 'src/app/models/AdvertModelList';

@Component({
  selector: 'app-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.scss']
})
export class AdvertComponent implements OnInit {

  public advertList: AdvertModelList[] = []

  constructor() { }

  public ngOnInit(): void {
    this.advertList.push(new AdvertModelList(1, "Автовышка АПТ-32", 1200));
    this.advertList.push(new AdvertModelList(1, "Кран ТТ-22", 2000));
    this.advertList.push(new AdvertModelList(1, "Камаз АН246", 1500));
    this.advertList.push(new AdvertModelList(1, "Ямобур 65-36", 1900));
  }

}
