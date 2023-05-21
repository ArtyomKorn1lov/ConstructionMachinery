import { AvailableDayModel } from "./AvailableDayModel";

export class LeaseRequestModel {
    price: number;
    availableDayModels: AvailableDayModel[];

    public constructor(_price: number, _availableDayModels: AvailableDayModel[]) {
        this.price = _price;
        this.availableDayModels = _availableDayModels;
    }
}