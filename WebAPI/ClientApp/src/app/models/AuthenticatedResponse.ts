export class AuthenticatedResponse {
    public token: string;
    public refreshToken: string;

    public constructor(_token: string, _refreshToken: string) {
        this.token = _token;
        this.refreshToken = _refreshToken;
    }
}