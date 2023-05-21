import { ImageModel } from "./ImageModel";

export class AdvertModelList {
    public id: number;
    public name: string;
    public price: number;
    public averageRating: number;
    public editDate: Date;
    public images: ImageModel[];

    public constructor(_id: number, _name: string, _price: number, _averageRating: number, _editDate: Date, _images: ImageModel[]) {
        this.id = _id;
        this.name = _name;
        this.price = _price;
        this.averageRating = _averageRating;
        this.editDate = _editDate;
        this.images = _images;
    }
}