import { Injectable } from '@angular/core';
import { AuthenticatedResponse } from '../models/AuthenticatedResponse';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AccountService } from '../services/account.service';

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate {

    public constructor(private router: Router, private jwtHelper: JwtHelperService, private http: HttpClient, private accountService: AccountService) { }

    public async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const token = localStorage.getItem("jwt");
        if (token && !this.jwtHelper.isTokenExpired(token)) {
            return true;
        }
        if (token == null) {
            this.router.navigate([""]);
            return false;
        }
        const isRefreshSuccess = await this.tryRefreshingTokens(token);
        if (!isRefreshSuccess) {
            this.router.navigate([""]);
            return isRefreshSuccess;
        }
        return isRefreshSuccess;
    }

    private async tryRefreshingTokens(token: string): Promise<boolean> {
        const refreshToken = localStorage.getItem("refreshToken");
        if (!token || !refreshToken) {
            return false;
        }
        const credentials = JSON.stringify({ accessToken: token, refreshToken: refreshToken });
        let isRefreshSuccess: boolean;
        const refreshRes = await new Promise<AuthenticatedResponse>((resolve, reject) => {
            this.http.post<AuthenticatedResponse>(`api/token/refresh`, credentials, {
                headers: new HttpHeaders({
                    "Content-Type": "application/json"
                })
            }).subscribe({
                next: (res: AuthenticatedResponse) => resolve(res),
                error: (_) => { reject; isRefreshSuccess = false; }
            });
        });
        localStorage.setItem("jwt", refreshRes.token);
        localStorage.setItem("refreshToken", refreshRes.refreshToken);
        isRefreshSuccess = true;
        return isRefreshSuccess;
    }
}