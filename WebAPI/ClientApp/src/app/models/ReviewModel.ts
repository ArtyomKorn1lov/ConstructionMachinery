export class ReviewModel {
    id: number;
    description: string;
    date: Date;
    name: string;
    reviewStateId: number;

    public constructor(_id: number, _description: string, _date: Date, _name: string, _reviewStateId: number) {
        this.id = _id;
        this.description = _description;
        this.date = _date;
        this.name = _name;
        this.reviewStateId = _reviewStateId;
    }
}