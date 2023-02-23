import { Component, OnInit } from '@angular/core';
import { AuthoriseModel } from 'src/app/models/AuthoriseModel';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/models/LoginModel';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.scss']
})
export class AuthorizeComponent implements OnInit {

  public email: string | undefined;
  public password: string | undefined;
  private targetRoute: string = "/";

  constructor(private accountService: AccountService, private router: Router) { }

  public async login(): Promise<void> {
    if (this.email == undefined || this.email.trim() == '') {
      alert("Введите email пользователя");
      this.email = '';
      return;
    }
    if (this.password == undefined || this.password.trim() == '') {
      alert("Введите пароль");
      this.password = '';
      return;
    }
    var model = new LoginModel(this.email, this.password);
    await this.accountService.login(model)
      .then(
        (data) => {
          console.log("success");
          alert("success");
          const token = data.token;
          const refreshToken = data.refreshToken;
          this.accountService.saveTokens(token, refreshToken);
          this.router.navigateByUrl(this.targetRoute);
          return;
        }
      )
      .catch(
        (error) => {
          alert("Некорректные логин и(или) пароль");
          console.log(error);
          this.email = '';
          this.password = '';
          return;
        }
      );
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
