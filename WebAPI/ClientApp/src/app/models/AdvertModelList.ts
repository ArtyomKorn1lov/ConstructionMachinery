export class AdvertModelList {
    id: number;
    name: string;
    price: number;

    public constructor(_id: number, _name: string, _price: number) {
        this.id = _id;
        this.name = _name;
        this.price = _price;
    }
}