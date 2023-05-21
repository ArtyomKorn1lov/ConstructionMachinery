export class ConfirmModel {
    public id: number;
    public requestStateId: number;

    public constructor(_id: number, _requestStateId: number) {
        this.id = _id;
        this.requestStateId = _requestStateId;
    }
}