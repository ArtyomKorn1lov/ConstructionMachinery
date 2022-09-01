import { ImageModel } from "./ImageModel";

export class AdvertModelList {
    id: number;
    name: string;
    price: number;
    images: ImageModel[];

    public constructor(_id: number, _name: string, _price: number, _images: ImageModel[]) {
        this.id = _id;
        this.name = _name;
        this.price = _price;
        this.images = _images;
    }
}