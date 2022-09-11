//
export class AuthoriseModel {
    name: string;
    email: string;

    public constructor(_name: string, _email: string) {
        this.name = _name;
        this.email = _email;
    }
}