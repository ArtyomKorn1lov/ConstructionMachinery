export class UserModel {
    id: number;
    name: string;
    email: string;
    phone: string;

    public constructor(_id: number, _name: string, _email: string, _phone: string) {
        this.id = _id,
        this.name = _name,
        this.email = _email,
        this.phone = _phone;
    }
}