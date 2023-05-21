import { ImageModel } from "./ImageModel";

export class AdvertModelForRequest {
    public id: number;
    public name: string;
    public editDate: Date;
    public images: ImageModel[];

    public constructor(_id: number, _name: string, _editDate: Date, _images: ImageModel[]) {
        this.id = _id;
        this.name = _name;
        this.editDate = _editDate;
        this.images = _images;
    }
}