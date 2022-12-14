import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterModel } from '../models/RegisterModel';
import { LoginModel } from '../models/LoginModel';
import { AuthoriseModel } from '../models/AuthoriseModel';
import { UserModel } from '../models/UserModel';
import { UserUpdateModel } from '../models/UserUpdateModel';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthenticatedResponse } from '../models/AuthenticatedResponse';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public userFlag: boolean = false;
  public authorize: AuthoriseModel = new AuthoriseModel("", "", false);

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  public clearLocalStorage(): void {
    localStorage.removeItem('userId');
    localStorage.removeItem('userPage');
  }

  public setUserIdInLocalStorage(id: number): void {
    localStorage.setItem('userId', id.toString());
  }

  public setPageInLocalStorage(page: string): void {
    localStorage.setItem('userPage', page);
  }

  public getUserIdFromLocalStorage(): number {
    let id = localStorage.getItem('userId');
    if (id == null)
      return 0;
    return parseInt(id);
  }

  public getPageFromLocalStorage(): string {
    let page = localStorage.getItem('userPage');
    if (page == null)
      return '';
    return page;
  }

  public async getAuthoriseModel(): Promise<void> {
    await this.isUserAuthorized().subscribe(data => {
      this.authorize = data;
      if(data != null)
      {
        this.userFlag = true;
        return;
      }
      this.userFlag = false;
    })
  }

  public saveTokens(token: string, refreshToken: string): void {
    localStorage.setItem("jwt", token);
    localStorage.setItem("refreshToken", refreshToken);
  }

  public isAuthorized(): void {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token))
      this.userFlag = true;
    else
      this.userFlag = false;
  }

  public registration(model: RegisterModel): Observable<AuthenticatedResponse> {
    return this.http.post<AuthenticatedResponse>(`api/account/register`, model, { headers: new HttpHeaders({ "Content-Type": "application/json" }) });
  }

  public login(model: LoginModel): Observable<AuthenticatedResponse> {
    return this.http.post<AuthenticatedResponse>(`api/account/login`, model, { headers: new HttpHeaders({ "Content-Type": "application/json" }) });
  }

  public logOut(): boolean {
    if(localStorage.getItem("jwt") == null || localStorage.getItem("refreshToken") == null) {
      return false;
    }
    else {
      localStorage.removeItem("jwt");
      localStorage.removeItem("refreshToken");
      localStorage.clear();
      this.userFlag = false;
      return true;
    }
  }

  public isUserAuthorized(): Observable<AuthoriseModel> {
    return this.http.get<AuthoriseModel>(`api/account/is-authorized`);
  }

  public getUserProfile(): Observable<UserModel> {
    return this.http.get<UserModel>(`api/account/user`);
  }

  public getUserById(id: number): Observable<UserModel> {
    return this.http.get<UserModel>(`api/account/user/${id}`);
  }

  public update(model: UserUpdateModel): Observable<AuthenticatedResponse> {
    return this.http.put<AuthenticatedResponse>(`api/account/update`, model, { headers: new HttpHeaders({ "Content-Type": "application/json" }) });
  }

}
