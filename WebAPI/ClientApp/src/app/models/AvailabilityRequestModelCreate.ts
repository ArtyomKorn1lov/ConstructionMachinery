import { AvailableTimeModelForCreateRequest } from "./AvailableTimeModelForCreateRequest";

export class AvailabilityRequestModelCreate {
    address: string;
    conditions: string;
    requestStateId: number;
    userId: number;
    availableTimeModelForCreateRequests: AvailableTimeModelForCreateRequest[];

    public constructor(_address: string, _conditions: string, _requestStateId: number, _userId: number, _availableTimeModelForCreateRequests: AvailableTimeModelForCreateRequest[]) {
        this.address = _address;
        this.conditions = _conditions;
        this.requestStateId = _requestStateId;
        this.userId = _userId;
        this.availableTimeModelForCreateRequests = _availableTimeModelForCreateRequests;
    }
}
