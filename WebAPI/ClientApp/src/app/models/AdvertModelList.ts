import { ImageModel } from "./ImageModel";

export class AdvertModelList {
    id: number;
    name: string;
    price: number;
    averageRating: number;
    images: ImageModel[];

    public constructor(_id: number, _name: string, _price: number, _averageRating: number, _images: ImageModel[]) {
        this.id = _id;
        this.name = _name;
        this.price = _price;
        this.averageRating = _averageRating;
        this.images = _images;
    }
}