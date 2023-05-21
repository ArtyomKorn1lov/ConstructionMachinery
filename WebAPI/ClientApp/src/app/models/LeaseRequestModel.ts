import { AvailableDayModel } from "./AvailableDayModel";

export class LeaseRequestModel {
    public price: number;
    public availableDayModels: AvailableDayModel[];

    public constructor(_price: number, _availableDayModels: AvailableDayModel[]) {
        this.price = _price;
        this.availableDayModels = _availableDayModels;
    }
}