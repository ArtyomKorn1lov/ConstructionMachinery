export class AvailabilityRequestModel {
    id: number;
    name: string;

    public constructor(_id: number, _name: string) {
        this.id = _id;
        this.name = _name;
    }
}