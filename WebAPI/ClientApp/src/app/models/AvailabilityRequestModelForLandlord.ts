import { ImageModel } from "./ImageModel";

export class AvailabilityRequestModelForLandlord {
    id: number;
    advertName: string;
    created: Date;
    updated: Date;
    address: string;
    conditions: string;
    sum: number;
    phone: string;
    customerName: string;
    userId: number;
    images: ImageModel[];
    startRent: Date;
    endRent: Date;

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