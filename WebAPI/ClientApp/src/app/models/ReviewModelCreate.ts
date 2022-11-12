export class ReviewModelCreate {
    description: string;
    date: Date;
    reviewStateId: number;
    advertId: number;
    userId: number;

    public constructor(_description: string, _date: Date, _reviewStateId: number, _advertId: number, _userId: number) {
        this.description = _description;
        this.date = _date;
        this.reviewStateId = _reviewStateId;
        this.advertId = _advertId;
        this.userId = _userId;
    }
}