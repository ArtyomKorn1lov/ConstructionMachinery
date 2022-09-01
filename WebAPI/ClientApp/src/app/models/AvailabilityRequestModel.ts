import { ImageModel } from "./ImageModel";

export class AvailabilityRequestModel {
    id: number;
    name: string;
    images: ImageModel[]

    public constructor(_id: number, _name: string, _images: ImageModel[]) {
        this.id = _id;
        this.name = _name;
        this.images = _images;
    }
}