import { AvailableTimeModel } from "./AvailableTimeModel";

export class AdvertModelInfo {
    id: number;
    name: string;
    description: string;
    price: number
    userName: string;
    availableTimes: AvailableTimeModel[];

    public constructor(_id: number, _name: string, _description: string, _price: number, _userName: string, _availableTimes: AvailableTimeModel[]) {
        this.id = _id;
        this.name = _name;
        this.description = _description;
        this.price = _price;
        this.userName = _userName;
        this.availableTimes = _availableTimes;
    }
}