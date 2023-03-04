import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { RegisterModel } from '../models/RegisterModel';
import { LoginModel } from '../models/LoginModel';
import { AuthoriseModel } from '../models/AuthoriseModel';
import { UserModel } from '../models/UserModel';
import { UserUpdateModel } from '../models/UserUpdateModel';
import { AuthenticatedResponse } from '../models/AuthenticatedResponse';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public userFlag: boolean = false;
  public authorize: AuthoriseModel = new AuthoriseModel("", "", false);

  constructor(private http: HttpClient, private tokenService: TokenService) { }

  private resetAuthParams(): void {
    localStorage.removeItem("jwt");
    localStorage.removeItem("refreshToken");
    localStorage.clear();
    this.userFlag = false;
    this.authorize = new AuthoriseModel("", "", false);
  }

  public async getAuthoriseModel(): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult) {
      this.resetAuthParams();
      return;
    }
    await this.isUserAuthorized()
      .then(
        (data) => {
          if (data != null) {
            this.authorize = data;
            this.userFlag = true;
            return;
          }
          this.resetAuthParams();
        }
      )
      .catch(
        (error) => {
          console.log(error);
          this.resetAuthParams();
        }
      );
  }

  public saveTokens(token: string, refreshToken: string): void {
    localStorage.setItem("jwt", token);
    localStorage.setItem("refreshToken", refreshToken);
  }

  public async registration(model: RegisterModel): Promise<AuthenticatedResponse> {
    return await lastValueFrom(this.http.post<AuthenticatedResponse>(`api/account/register`, model, { headers: new HttpHeaders({ "Content-Type": "application/json" }) }));
  }

  public async login(model: LoginModel): Promise<AuthenticatedResponse> {
    return await lastValueFrom(this.http.post<AuthenticatedResponse>(`api/account/login`, model, { headers: new HttpHeaders({ "Content-Type": "application/json" }) }));
  }

  public logOut(): boolean {
    if (localStorage.getItem("jwt") == null || localStorage.getItem("refreshToken") == null) {
      return false;
    }
    else {
      this.resetAuthParams();
      return true;
    }
  }

  public async isUserAuthorized(): Promise<AuthoriseModel> {
    return await lastValueFrom(this.http.get<AuthoriseModel>(`api/account/is-authorized`));
  }

  public async getUserProfile(): Promise<UserModel> {
    return await lastValueFrom(this.http.get<UserModel>(`api/account/user`));
  }

  public async getUserById(id: number): Promise<UserModel> {
    return await lastValueFrom(this.http.get<UserModel>(`api/account/user/${id}`));
  }

  public async getProfileById(id: number): Promise<UserModel> {
    return await lastValueFrom(this.http.get<UserModel>(`api/account/user-profile/${id}`));
  }

  public async update(model: UserUpdateModel): Promise<AuthenticatedResponse> {
    return await lastValueFrom(this.http.put<AuthenticatedResponse>(`api/account/update`, model, { headers: new HttpHeaders({ "Content-Type": "application/json" }) }));
  }

}
