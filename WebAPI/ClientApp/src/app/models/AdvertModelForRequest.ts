import { ImageModel } from "./ImageModel";

export class AdvertModelForRequest {
    id: number;
    name: string;
    editDate: Date;
    images: ImageModel[];

    public constructor(_id: number, _name: string, _editDate: Date, _images: ImageModel[]) {
        this.id = _id;
        this.name = _name;
        this.editDate = _editDate;
        this.images = _images;
    }
}