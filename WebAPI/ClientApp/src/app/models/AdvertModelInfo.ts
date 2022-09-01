import { AvailableTimeModel } from "./AvailableTimeModel";
import { ImageModel } from "./ImageModel";

export class AdvertModelInfo {
    id: number;
    name: string;
    description: string;
    price: number
    userName: string;
    images: ImageModel[];
    availableTimes: AvailableTimeModel[];

    public constructor(_id: number, _name: string, _description: string, _price: number, _userName: string, _images: ImageModel[], _availableTimes: AvailableTimeModel[]) {
        this.id = _id;
        this.name = _name;
        this.description = _description;
        this.price = _price;
        this.userName = _userName;
        this.images = _images;
        this.availableTimes = _availableTimes;
    }
}