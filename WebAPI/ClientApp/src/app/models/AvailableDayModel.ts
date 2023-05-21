import { AvailableTimeModel } from "./AvailableTimeModel";

export class AvailableDayModel {
    public date: Date;
    public times: AvailableTimeModel[];

    public constructor(_date: Date, _times: AvailableTimeModel[]) {
        this.date = _date;
        this.times = _times;
    }
}