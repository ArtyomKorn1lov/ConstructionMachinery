import { Component, EventEmitter, OnInit, ViewChild } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertComponent } from 'src/app/components/advert/advert.component';

@Component({
  selector: 'app-advert-list',
  templateUrl: './advert-list.component.html',
  styleUrls: ['./advert-list.component.scss']
})
export class AdvertListComponent implements OnInit {

  public filter: string = "all";
  @ViewChild(AdvertComponent) child: AdvertComponent | undefined;

  constructor(private accountService: AccountService) { }

  public async selectEvent(): Promise<void> {
    await this.child?.sortByParam(this.filter);
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
