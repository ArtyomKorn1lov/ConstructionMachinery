import { AvailableTimeModel } from "./AvailableTimeModel";

export class LeaseTimeModel {
    date: Date;
    times: AvailableTimeModel[];

    public constructor(_date: Date, _times: AvailableTimeModel[]) {
        this.date = _date;
        this.times = _times;
    }
}