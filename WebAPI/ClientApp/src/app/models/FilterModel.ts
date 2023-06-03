export class FilterModel {
    public startPublishDate: Date;
    public endPublishDate: Date;
    public startDate: Date;
    public endDate: Date;
    public startTime: number;
    public endTime: number;
    public startPrice: number;
    public endPrice: number;
    public keyWord: string[];

    public constructor(_startPublishDate: Date, _endPublishDate: Date, _startDate: Date, _endDate: Date, _startTime: number, _endTime: number,
        _startPrice: number, _endPrice: number, _keyWord: string[]) {
            this.startPublishDate = _startPublishDate;
            this.endPublishDate = _endPublishDate;
            this.startDate = _startDate;
            this.endDate = _endDate;
            this.startTime = _startTime;
            this.endTime = _endTime;
            this.startPrice = _startPrice;
            this.endPrice = _endPrice;
            this.keyWord = _keyWord;
    }
}