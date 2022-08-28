export class UserUpdateModel {
    id: number;
    name: string;
    email: string;
    phone: string;
    password: string

    public constructor(_id: number, _name: string, _email: string, _phone: string, _password: string) {
        this.id = _id,
        this.name = _name,
        this.email = _email,
        this.phone = _phone;
        this.password = _password;
    }
}