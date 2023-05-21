export class AvailableTimeModelForCreateRequest {
    public id: number;
    public availabilityStateId: number;

    public constructor(_id: number, _availabilityStateId: number) {
        this.id = _id;
        this.availabilityStateId = _availabilityStateId;
    }
}