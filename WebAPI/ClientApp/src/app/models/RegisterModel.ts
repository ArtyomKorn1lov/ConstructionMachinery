export class RegisterModel {
    public name: string;
    public email: string;
    public phone: string;
    public address: string;
    public password: string;

    public constructor(_name: string, _email: string, _phone: string, _address: string, _password: string) {
        this.name = _name;
        this.email = _email;
        this.phone = _phone;
        this.address = _address;
        this.password = _password;
    }
}