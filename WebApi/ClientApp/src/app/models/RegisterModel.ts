export class RegisterModel {
    name: string;
    email: string;
    phone: string;
    password: string;

    public constructor(_name: string, _email: string, _phone: string, _password: string) {
        this.name = _name;
        this.email = _email;
        this.phone = _phone;
        this.password = _password;
    }
}