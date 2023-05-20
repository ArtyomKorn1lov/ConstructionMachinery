import { AvailableTimeModel } from "./AvailableTimeModel";
import { ImageModel } from "./ImageModel";

export class AvailabilityRequestModelForLandlord {
    id: number;
    advertName: string;
    address: string;
    phone: string;
    customerName: string;
    userId: number;
    images: ImageModel[];
    startRent: Date;
    endRent: Date;

    public constructor(_id: number, _advertName: string, _address: string, _phone: string, _customerName: string, _userId: number, 
        _images: ImageModel[], _startRent: Date, _endRent: Date) {
        this.id = _id;
        this.advertName = _advertName;
        this.address = _address;
        this.phone = _phone;
        this.customerName = _customerName;
        this.userId = _userId;
        this.images = _images;
        this.startRent = _startRent;
        this.endRent = _endRent;
    }
}