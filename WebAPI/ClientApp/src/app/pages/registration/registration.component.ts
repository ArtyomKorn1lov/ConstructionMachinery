import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { RegisterModel } from 'src/app/models/RegisterModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  public email: string | undefined;
  public password: string | undefined;
  public confirm_password: string | undefined;
  public name: string | undefined;
  public phone: string | undefined;
  private targetRoute: string = "/";

  constructor(private accountService: AccountService, private router: Router) { }

  public async registration(): Promise<void> {
    if (this.email == undefined || this.email.trim() == '') {
      alert("Введите Email пользователя");
      this.email = '';
      return;
    }
    if (this.password == undefined || this.password.trim() == '') {
      alert("Введите пароль");
      this.password = '';
      return;
    }
    if (this.confirm_password == undefined || this.password.trim() == '') {
      alert("Подтвердите пароль");
      this.confirm_password = '';
      return;
    }
    if (this.name == undefined || this.name.trim() == '') {
      alert("Введите ФИО пользователя");
      this.name = '';
      return;
    }
    if (this.phone == undefined || this.phone.trim() == '') {
      alert("Введите номер телефона");
      this.phone = '';
      return;
    }
    if (this.confirm_password != this.password) {
      alert("Пароли не совпадают, проверьте пароли");
      this.password = '';
      this.confirm_password = '';
      return;
    }
    let model = new RegisterModel(this.name, this.email, this.phone, this.password);
    await this.accountService.registration(model).subscribe({
      next: async (data) => {
        console.log(data);
        alert("success");
        const token = data.token;
        const refreshToken = data.refreshToken;
        this.accountService.saveTokens(token, refreshToken);
        this.router.navigateByUrl(this.targetRoute);
        return;
      },
      error: (bad) => {
        alert("Некорректные логин и(или) пароль");
        console.log(bad);
        this.email = '';
        this.password = '';
        this.confirm_password = '';
        this.name = '';
        this.phone = '';
        return;
      }
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
