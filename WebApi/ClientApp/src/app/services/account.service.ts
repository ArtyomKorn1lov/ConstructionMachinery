import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterModel } from '../models/RegisterModel';
import { LoginModel } from '../models/LoginModel';
import { AuthoriseModel } from '../models/AuthoriseModel';
import { UserModel } from '../models/UserModel';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  public Registration(model: RegisterModel): Observable<string> {
    return this.http.post(`api/account/register`, model, { responseType: 'text' });
  }

  public Login(model: LoginModel): Observable<string> {
    return this.http.post(`api/account/login`, model, { responseType: 'text' });
  }

  public Logout(): Observable<string> {
    var model = new RegisterModel("", "", "", "");
    return this.http.post(`api/account/logout`, model, { responseType: 'text' });
  }

  public IsUserAuthorized(): Observable<AuthoriseModel> {
    return this.http.get<AuthoriseModel>(`api/account/is-authorized`);
  }

  public GetUserProfile(): Observable<UserModel> {
    return this.http.get<UserModel>(`api/account/user`);
  }

}
