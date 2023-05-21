export class ReviewModelUpdate {
    public id: number;
    public description: string;
    public date: Date;
    public reviewStateId: number;
    public advertId: number;
    public userId: number;

    public constructor(_id: number, _description: string, _date: Date, _reviewStateId: number, _advertId: number, _userId: number) {
        this.id = _id;
        this.description = _description;
        this.date = _date;
        this.reviewStateId = _reviewStateId;
        this.advertId = _advertId;
        this.userId = _userId;
    }
}