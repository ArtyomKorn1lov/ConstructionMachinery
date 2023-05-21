import { ImageModel } from "./ImageModel";

export class AvailabilityRequestModelForLandlord {
    public id: number;
    public advertName: string;
    public created: Date;
    public updated: Date;
    public address: string;
    public conditions: string;
    public sum: number;
    public phone: string;
    public customerName: string;
    public userId: number;
    public images: ImageModel[];
    public startRent: Date;
    public endRent: Date;

    public constructor(_id: number, _advertName: string, _created: Date, _updated: Date, _address: string, _conditions: string, _sum: number, 
        _phone: string, _customerName: string, _userId: number, _images: ImageModel[], _startRent: Date, _endRent: Date) {
        this.id = _id;
        this.advertName = _advertName;
        this.created = _created;
        this.updated = _updated;
        this.address = _address;
        this.conditions = _conditions;
        this.sum = _sum;
        this.phone = _phone;
        this.customerName = _customerName;
        this.userId = _userId;
        this.images = _images;
        this.startRent = _startRent;
        this.endRent = _endRent;
    }
}