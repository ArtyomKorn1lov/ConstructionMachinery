import { AvailableTimeModel } from "./AvailableTimeModel";

export class AvailableDayModel {
    day: Date;
    times: AvailableTimeModel[];

    public constructor(_day: Date, _times: AvailableTimeModel[]) {
        this.day = _day;
        this.times = _times;
    }
}