import { AvailableTimeModel } from "./AvailableTimeModel";

export class AvailabilityRequestModelForLandlord {
    id: number;
    advertName: string;
    address: string;
    phone: string;
    customerName: string;
    userId: number;
    availableTimeModels: AvailableTimeModel[];

    public constructor(_id: number, _advertName: string, _address: string, _phone: string, _customerName: string, _userId: number, _availableTimeModels: AvailableTimeModel[]) {
        this.id = _id;
        this.advertName = _advertName;
        this.address = _address;
        this.phone = _phone;
        this.customerName = _customerName;
        this.userId = _userId;
        this.availableTimeModels = _availableTimeModels;
    }
}