export class AvailableTimeModelForCreateRequest {
    id: number;
    availabilityStateId: number;

    public constructor(_id: number, _availabilityStateId: number) {
        this.id = _id;
        this.availabilityStateId = _availabilityStateId;
    }
}