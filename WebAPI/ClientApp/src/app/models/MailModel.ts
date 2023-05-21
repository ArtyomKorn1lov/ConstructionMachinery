export class MailModel {
    public name: string;
    public email: string;
    public phone: string;
    public description: string;

    public constructor(_name: string, _email: string, _phone: string, _description: string) {
        this.name = _name;
        this.email = _email;
        this.phone = _phone;
        this.description = _description;
    }
}