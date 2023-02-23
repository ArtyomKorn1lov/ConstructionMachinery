import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticatedResponse } from '../models/AuthenticatedResponse';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor(private jwtHelper: JwtHelperService, private http: HttpClient) { }

  public async tokenVerify(): Promise<boolean> {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    if (token == null) {
      return false;
    }
    return await this.tryRefreshingTokens(token);
  }

  private async tryRefreshingTokens(token: string): Promise<boolean> {
    const refreshToken = localStorage.getItem("refreshToken");
    if (!token || !refreshToken) {
      return false;
    }
    const credentials = JSON.stringify({ accessToken: token, refreshToken: refreshToken });
    let isRefreshSuccess: boolean;
    let refreshRes = null;
    await lastValueFrom(
      this.http.post<AuthenticatedResponse>(`api/token/refresh`, credentials, {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      })
    )
    .then(
      (data) => {
        refreshRes = data;
        localStorage.setItem("jwt", refreshRes.token);
        localStorage.setItem("refreshToken", refreshRes.refreshToken);
      }
    )
    .catch(
      (error) => {
        console.log(error);
        isRefreshSuccess = false;
        return isRefreshSuccess;
      }
    );
    isRefreshSuccess = true;
    return isRefreshSuccess;
  }

}