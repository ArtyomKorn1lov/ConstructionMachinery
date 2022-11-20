export class ReviewModel {
    id: number;
    description: string;
    date: Date;
    name: string;
    userId: number;
    isAuthorized: boolean;
    reviewStateId: number;

    public constructor(_id: number, _description: string, _date: Date, _name: string, _userId: number, _isAuthorized: boolean, _reviewStateId: number) {
        this.id = _id;
        this.description = _description;
        this.date = _date;
        this.name = _name;
        this.userId = _userId;
        this.isAuthorized = _isAuthorized;
        this.reviewStateId = _reviewStateId;
    }
}