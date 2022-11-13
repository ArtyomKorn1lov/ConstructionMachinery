import { Injectable } from '@angular/core';

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

}
