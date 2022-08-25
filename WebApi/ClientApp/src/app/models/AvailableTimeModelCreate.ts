export class AvailableTimeModelCreate {
    date: Date;
    availabilityStateId: number;

    public constructor(_date: Date, _availabilityStateId: number) {
        this.date = _date;
        this.availabilityStateId = _availabilityStateId;
    }
}