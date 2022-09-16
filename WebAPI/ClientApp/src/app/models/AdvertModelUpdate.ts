import { ImageModel } from "./ImageModel";

export class AdvertModelUpdate {
    id: number;
    name: string;
    description: string;
    price: number;
    userId: number;
    images: ImageModel[];
    startDate: Date;
    endDate: Date;
    startTime: number;
    endTime: number;

    public constructor(_id: number, _name: string, _description: string, _price: number, _userId: number, _images: ImageModel[], _startDate: Date, _endDate: Date, _startTime: number, _endTime: number) {
        this.id = _id;
        this.name = _name;
        this.description = _description;
        this.price = _price;
        this.userId = _userId;
        this.images = _images;
        this.startDate = _startDate;
        this.endDate = _endDate;
        this.startTime = _startTime;
        this.endTime = _endTime;
    }
}