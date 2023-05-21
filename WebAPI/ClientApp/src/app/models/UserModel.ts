export class UserModel {
    public id: number;
    public name: string;
    public email: string;
    public address: string;
    public created: Date;
    public phone: string;

    public constructor(_id: number, _name: string, _email: string, _address: string, _created: Date, _phone: string) {
        this.id = _id;
        this.name = _name;
        this.email = _email;
        this.address = _address;
        this.created = _created;
        this.phone = _phone;
    }
}