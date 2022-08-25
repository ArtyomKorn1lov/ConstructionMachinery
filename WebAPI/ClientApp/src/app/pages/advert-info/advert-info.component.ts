import { Component, OnInit } from '@angular/core';
import { AdvertService } from 'src/app/services/advert.service';
import { AdvertModelInfo } from 'src/app/models/AdvertModelInfo';
import { AvailableTimeModel } from 'src/app/models/AvailableTimeModel';
import { AvailableDayModel } from 'src/app/models/AvailableDayModel';

@Component({
  selector: 'app-advert-info',
  templateUrl: './advert-info.component.html',
  styleUrls: ['./advert-info.component.scss']
})
export class AdvertInfoComponent implements OnInit {

  public advert: AdvertModelInfo = new AdvertModelInfo(0, "", "", 0, "", []);
  public times: AvailableTimeModel[] = [];
  public days: AvailableDayModel[] = [];

  constructor(private advertService: AdvertService) { }

  public SortByDay(): void {
    let day = 0;
    var sortTimes: AvailableTimeModel[] = [];
    for (let count = 0; count < this.times.length; count++) {
      if (sortTimes.length != 0 && day != this.times[count].date.getDate()) {
        this.days.push(new AvailableDayModel(day, sortTimes));
        day = this.times[count].date.getDate();
        sortTimes = [];
      }
      if (day != this.times[count].date.getDate()) {
        day = this.times[count].date.getDate();
      }
      sortTimes.push(this.times[count]);
    }
    this.days.push(new AvailableDayModel(day, sortTimes));
  }

  public async ngOnInit(): Promise<void> {
    await this.advertService.GetById(this.advertService.GetIdFromLocalStorage()).subscribe(data => {
      this.advert = data;
    });
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T09:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T10:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T11:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T12:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T13:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T14:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T15:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T16:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-25T17:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T09:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T10:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T11:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T12:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T13:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T14:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T15:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T16:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-26T17:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T09:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T10:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T11:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T12:00:00'), 1, 3));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T13:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T14:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T15:00:00'), 1, 1));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T16:00:00'), 1, 2));
    this.times.push(new AvailableTimeModel(1, new Date('2022-07-27T17:00:00'), 1, 3));
    this.SortByDay();
  }

}
