import { AvailableTimeModelForCreateRequest } from "./AvailableTimeModelForCreateRequest";

export class AvailabilityRequestModelCreate {
    public address: string;
    public conditions: string;
    public requestStateId: number;
    public userId: number;
    public availableTimeModelForCreateRequests: AvailableTimeModelForCreateRequest[];

    public constructor(_address: string, _conditions: string, _requestStateId: number, _userId: number, _availableTimeModelForCreateRequests: AvailableTimeModelForCreateRequest[]) {
        this.address = _address;
        this.conditions = _conditions;
        this.requestStateId = _requestStateId;
        this.userId = _userId;
        this.availableTimeModelForCreateRequests = _availableTimeModelForCreateRequests;
    }
}
