export class AvailableTimeModel {
    id: number;
    date: Date;
    advertId: number;
    availabilityStateId: number;

    public constructor(_id: number, _date: Date, _advertId: number, _availabilityStateId: number) {
        this.id = _id;
        this.date = _date;
        this.advertId = _advertId;
        this.availabilityStateId = _availabilityStateId;
    }
}