import { AvailableTimeModel } from "./AvailableTimeModel";
import { ImageModel } from "./ImageModel";

export class AvailabilityRequestModelForCustomer {
    id: number;
    advertName: string;
    address: string;
    landlordName: string;
    phone: string;
    requestStateId: number;
    userId: number;
    images: ImageModel[];
    availableTimeModels: AvailableTimeModel[];

    public constructor(_id: number, _advertName: string, _address: string, _landlordName: string, _phone: string, _requestStateId: number, _userId: number, 
        _images: ImageModel[], _availableTimeModels: AvailableTimeModel[]) {
        this.id = _id;
        this.advertName = _advertName;
        this.address = _address;
        this.landlordName = _landlordName;
        this.phone = _phone;
        this.requestStateId = _requestStateId;
        this.userId = _userId;
        this.images = _images;
        this.availableTimeModels = _availableTimeModels;
    }
}