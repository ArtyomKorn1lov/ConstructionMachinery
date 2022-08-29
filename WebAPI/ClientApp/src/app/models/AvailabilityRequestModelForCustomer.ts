import { AvailableTimeModel } from "./AvailableTimeModel";

export class AvailabilityRequestModelForCustomer {
    id: number;
    advertName: string;
    address: string;
    landlordName: string;
    phone: string;
    requestStateId: number;
    userId: number;
    availableTimeModels: AvailableTimeModel[];

    public constructor(_id: number, _advertName: string, _address: string, _landlordName: string, _phone: string, _requestStateId: number, _userId: number, _availableTimeModels: AvailableTimeModel[]) {
        this.id = _id;
        this.advertName = _advertName;
        this.address = _address;
        this.landlordName = _landlordName;
        this.phone = _phone;
        this.requestStateId = _requestStateId;
        this.userId = _userId;
        this.availableTimeModels = _availableTimeModels;
    }
}