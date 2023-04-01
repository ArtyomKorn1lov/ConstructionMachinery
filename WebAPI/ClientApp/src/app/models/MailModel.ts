export class MailModel {
    name: string;
    email: string;
    phone: string;
    description: string;

    public constructor(_name: string, _email: string, _phone: string, _description: string) {
        this.name = _name;
        this.email = _email;
        this.phone = _phone;
        this.description = _description;
    }
}