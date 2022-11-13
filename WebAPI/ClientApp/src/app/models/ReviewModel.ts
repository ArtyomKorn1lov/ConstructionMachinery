export class ReviewModel {
    id: number;
    description: string;
    date: Date;
    name: string;
    isAuthorized: boolean;
    reviewStateId: number;

    public constructor(_id: number, _description: string, _date: Date, _name: string, _isAuthorized: boolean, _reviewStateId: number) {
        this.id = _id;
        this.description = _description;
        this.date = _date;
        this.name = _name;
        this.isAuthorized = _isAuthorized;
        this.reviewStateId = _reviewStateId;
    }
}