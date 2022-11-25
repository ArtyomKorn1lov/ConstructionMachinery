import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Filter } from 'src/app/models/Filter';

const PriceFilter: Filter[] = [
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
]

const RatingFilter: Filter[] = [
  {
    name: 'Все объявления',
    param: 'all'
  },
  {
    name: 'С самым высоким рейтингом',
    param: 'max_rating',
  },
  {
    name: 'С самым низким рейтингом',
    param: 'min_rating',
  }
]

const DateFilter: Filter[] = [
  {
    name: 'Самые новые объявления',
    param: 'max_date',
  },
  {
    name: 'Самые старые объявления',
    param: 'min_date',
  }
]


@Component({
  selector: 'app-dialog-filter',
  templateUrl: './dialog-filter.component.html',
  styleUrls: ['./dialog-filter.component.scss']
})
export class DialogFilterComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<DialogFilterComponent>) {
  }

  public close(): void {
    this.dialogRef.close();
  }

  public price(): void {
    this.dialogRef.close({data: PriceFilter});
  }

  public rating(): void {
    this.dialogRef.close({data: RatingFilter});
  }

  public date(): void {
    this.dialogRef.close({data: DateFilter});
  }

  ngOnInit(): void {
  }

}
