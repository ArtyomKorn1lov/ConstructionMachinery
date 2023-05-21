export class AdvertModelCreate {
    public name: string;
    public dateIssue: Date;
    public pts: string;
    public vin: string;
    public description: string;
    public price: number;
    public userId: number;
    public startDate: Date;
    public endDate: Date;
    public startTime: number;
    public endTime: number;

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