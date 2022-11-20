export class AuthoriseModel {
    name: string;
    email: string;
    isAvailable: boolean;

    public constructor(_name: string, _email: string, _isAvailable: boolean) {
        this.name = _name;
        this.email = _email;
        this.isAvailable = _isAvailable;
    }
}