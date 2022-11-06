export class AuthenticatedResponse {
    token: string;
    refreshToken: string;

    public constructor(_token: string, _refreshToken: string) {
        this.token = _token;
        this.refreshToken = _refreshToken;
    }
}