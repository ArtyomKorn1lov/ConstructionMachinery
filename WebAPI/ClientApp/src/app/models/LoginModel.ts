export class LoginModel {
    email: string;
    password: string;

    public constructor(_email: string, _password: string) {
        this.email = _email;
        this.password = _password;
    }
}