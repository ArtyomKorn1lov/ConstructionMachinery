export class ReviewModelInfo {
    public id: number;
    public description: string;
    public reviewStateId: number;
    public advertId: number;

    public constructor(_id: number, _description: string, _reviewStateId: number, _advertId: number) {
        this.id = _id;
        this.description = _description;
        this.reviewStateId = _reviewStateId;
        this.advertId = _advertId;
    }
}