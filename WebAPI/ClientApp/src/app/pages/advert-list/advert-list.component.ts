import { Component, OnInit, ViewChild } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AdvertComponent } from 'src/app/components/advert/advert.component';
import { DialogFilterComponent } from 'src/app/components/dialog-filter/dialog-filter.component';
import { MatDialog } from '@angular/material/dialog';
import { Filter } from 'src/app/models/Filter';

@Component({
  selector: 'app-advert-list',
  templateUrl: './advert-list.component.html',
  styleUrls: ['./advert-list.component.scss']
})
export class AdvertListComponent implements OnInit {

  public filters: Filter[] = [
    {
      name: 'Все объявления',
      param: 'all'
    },
    {
      name: 'По возрастанию цены',
      param: 'max_price',
    },
    {
      name: 'По убыванию цены',
      param: 'min_price',
    }
  ];

  public filterState: string = "all";
  @ViewChild(AdvertComponent) child: AdvertComponent | undefined;

  constructor(private accountService: AccountService, private dialog: MatDialog) { }

  public async selectEvent(): Promise<void> {
    await this.child?.sortByParam(this.filterState);
  }

  public openFilterDialog(): void {
    const dialogRef = this.dialog.open(DialogFilterComponent);
    dialogRef.afterClosed().subscribe(async result => {
      if (result == undefined)
        return;
      this.filters = result.data;
      await this.child?.sortByParam(this.filters[0].param);
    })
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
