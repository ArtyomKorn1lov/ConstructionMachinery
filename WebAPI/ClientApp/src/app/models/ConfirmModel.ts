export class ConfirmModel {
    id: number;
    requestStateId: number;

    public constructor(_id: number, _requestStateId: number) {
        this.id = _id;
        this.requestStateId = _requestStateId;
    }
}