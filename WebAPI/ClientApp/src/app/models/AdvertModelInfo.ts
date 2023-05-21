import { AvailableTimeModel } from "./AvailableTimeModel";
import { ImageModel } from "./ImageModel";

export class AdvertModelInfo {
    public id: number;
    public name: string;
    public dateIssue: Date;
    public pts: string;
    public vin: string;
    public description: string;
    public publishDate: Date;
    public editDate: Date;
    public price: number
    public userName: string;
    public images: ImageModel[];
    public availableTimes: AvailableTimeModel[];

    public constructor(_id: number, _name: string, _dateIssue: Date, _pts: string, _vin: string, _description: string, _publishDate: Date, _editDate: Date, _price: number, _userName: string, _images: ImageModel[], _availableTimes: AvailableTimeModel[]) {
        this.id = _id;
        this.name = _name;
        this.dateIssue = _dateIssue;
        this.pts = _pts;
        this.vin = _vin;
        this.publishDate = _publishDate;
        this.editDate = _editDate;
        this.description = _description;
        this.price = _price;
        this.userName = _userName;
        this.images = _images;
        this.availableTimes = _availableTimes;
    }
}