import { Injectable } from '@angular/core';
import { AvailableTimeModel } from '../models/AvailableTimeModel';

@Injectable({
  providedIn: 'root'
})
export class DatetimeService {

  constructor() { }

  public convertDateToUTS(date: number): string {
    if(date >= 0 && date < 10)
      return '0' + date;
    return date.toString();
  }

  public sortByHour(list: AvailableTimeModel[]): AvailableTimeModel[] {
    let time;
    for(let i = 0; i < list.length; i++) {
      for(let j = 0; j < list.length - i - 1; j++) {
        if(list[j].date.getHours() > list[j+1].date.getHours()) {
          time = list[j];
          list[j] = list[j+1];
          list[j+1] = time;
        }
      }
    }
    return list;
  }

}
