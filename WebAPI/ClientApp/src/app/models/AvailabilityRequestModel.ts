import { ImageModel } from "./ImageModel";

export class AvailabilityRequestModel {
    id: number;
    name: string;
    date: Date;
    images: ImageModel[]

    public constructor(_id: number, _name: string, _date: Date, _images: ImageModel[]) {
        this.id = _id;
        this.name = _name;
        this.date = _date;
        this.images = _images;
    }
}