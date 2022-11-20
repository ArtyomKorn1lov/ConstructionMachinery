export class AdvertModelCreate {
    name: string;
    dateIssue: Date;
    pts: string;
    vin: string;
    description: string;
    price: number;
    userId: number;
    startDate: Date;
    endDate: Date;
    startTime: number;
    endTime: number;

    public constructor(_name: string, _dateIssue: Date, _pts: string, _vin: string, _description: string, _price: number, _userId: number, _startDate: Date, _endDate: Date, _startTime: number, _endTime: number) {
        this.name = _name;
        this.dateIssue = _dateIssue;
        this.pts = _pts;
        this.vin = _vin;
        this.description = _description;
        this.price = _price;
        this.userId = _userId;
        this.startDate = _startDate;
        this.endDate = _endDate;
        this.startTime = _startTime;
        this.endTime = _endTime;
    }
}