import { ImageModel } from "./ImageModel";

export class AvailabilityRequestModelForCustomer {
    public id: number;
    public advertName: string;
    public created: Date;
    public updated: Date;
    public address: string;
    public conditions: string;
    public sum: number;
    public landlordName: string;
    public phone: string;
    public requestStateId: number;
    public userId: number;
    public images: ImageModel[];
    public startRent: Date;
    public endRent: Date;

    public constructor(_id: number, _advertName: string, _created: Date, _updated: Date, _address: string, _conditions: string, _sum: number, 
        _landlordName: string, _phone: string, _requestStateId: number, _userId: number, _images: ImageModel[], _startRent: Date, _endRent: Date) {
        this.id = _id;
        this.advertName = _advertName;
        this.created = _created;
        this.updated = _updated;
        this.address = _address;
        this.conditions = _conditions;
        this.sum = _sum;
        this.landlordName = _landlordName;
        this.phone = _phone;
        this.requestStateId = _requestStateId;
        this.userId = _userId;
        this.images = _images;
        this.startRent = _startRent;
        this.endRent = _endRent;
    }
}