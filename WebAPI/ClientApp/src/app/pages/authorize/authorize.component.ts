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
    await this.accountService.Login(model).subscribe(data => {
      if(data == "success") {
        console.log(data);
        alert(data);
        this.router.navigateByUrl(this.targetRoute);
        return;
      }
      if(data == "authorize") {
        alert("Пользователь уже авторизован");
        console.log(data);
        this.email = '';
        this.password = '';
        return;
      }
      alert("Некорректные логин и(или) пароль");
      console.log(data);
      this.email = '';
      this.password = '';
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
  }

}
