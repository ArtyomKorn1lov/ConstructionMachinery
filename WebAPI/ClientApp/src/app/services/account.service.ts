import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterModel } from '../models/RegisterModel';
import { LoginModel } from '../models/LoginModel';
import { AuthoriseModel } from '../models/AuthoriseModel';
import { UserModel } from '../models/UserModel';
import { UserUpdateModel } from '../models/UserUpdateModel';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public userFlag: boolean = false;
  public authorize: AuthoriseModel = new AuthoriseModel("", "");

  constructor(private http: HttpClient) { }

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

  public registration(model: RegisterModel): Observable<string> {
    return this.http.post(`api/account/register`, model, { responseType: 'text' });
  }

  public login(model: LoginModel): Observable<string> {
    return this.http.post(`api/account/login`, model, { responseType: 'text' });
  }

  public logout(): Observable<string> {
    var model = new RegisterModel("", "", "", "");
    return this.http.post(`api/account/logout`, model, { responseType: 'text' });
  }

  public isUserAuthorized(): Observable<AuthoriseModel> {
    return this.http.get<AuthoriseModel>(`api/account/is-authorized`);
  }

  public getUserProfile(): Observable<UserModel> {
    return this.http.get<UserModel>(`api/account/user`);
  }

  public update(model: UserUpdateModel): Observable<string> {
    return this.http.put(`api/account/update`, model, { responseType: 'text' });
  }
//
}
