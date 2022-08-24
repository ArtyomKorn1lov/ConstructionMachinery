import { AvailableTimeModel } from "./AvailableTimeModel";

export class AvailableDayModel {
    day: number;
    times: AvailableTimeModel[];

    public constructor(_day: number, _times: AvailableTimeModel[]) {
        this.day = _day;
        this.times = _times;
    }
}