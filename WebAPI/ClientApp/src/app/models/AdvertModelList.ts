import { ImageModel } from "./ImageModel";

export class AdvertModelList {
    id: number;
    name: string;
    price: number;
    averageRating: number;
    editDate: Date;
    images: ImageModel[];

    public constructor(_id: number, _name: string, _price: number, _averageRating: number, _editDate: Date, _images: ImageModel[]) {
        this.id = _id;
        this.name = _name;
        this.price = _price;
        this.averageRating = _averageRating;
        this.editDate = _editDate;
        this.images = _images;
    }
}