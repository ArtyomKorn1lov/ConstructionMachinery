import { AvailableTimeModelCreate } from "./AvailableTimeModelCreate";

export class AdvertModelCreate {
    name: string;
    description: string;
    price: number;
    userId: number;
    availableTimeModelsCreates: AvailableTimeModelCreate[];

    public constructor(_name: string, _description: string, _price: number, _userId: number, _availableTimeModelsCreates: AvailableTimeModelCreate[]) {
        this.name = _name;
        this.description = _description;
        this.price = _price;
        this.userId = _userId;
        this.availableTimeModelsCreates = _availableTimeModelsCreates;
    }
}