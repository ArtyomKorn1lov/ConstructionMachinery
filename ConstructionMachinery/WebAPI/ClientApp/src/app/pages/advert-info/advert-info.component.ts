import { Component, OnInit } from '@angular/core';
import { AdvertModelInfo } from 'src/app/models/AdvertModelInfo';

@Component({
  selector: 'app-advert-info',
  templateUrl: './advert-info.component.html',
  styleUrls: ['./advert-info.component.scss']
})
export class AdvertInfoComponent implements OnInit {

  public advert: AdvertModelInfo = new AdvertModelInfo(1, "Автовышка АПТ-32", "Автомобиль, оснащённый устройством для подъёма и перемещения рабочих с инструментом и материалами и используемый при монтаже и обслуживании линий электропередач, линий связи и контактных сетей, ремонте и обслуживании зданий и сооружений, обслуживании средств наружной рекламы, уходе за городскими зелёными насаждениями и т. п.",
  1200, "Артём");

  constructor() { }

  ngOnInit(): void {
  }

}
