import { Component, OnInit } from '@angular/core';
import { AuthoriseModel } from 'src/app/models/AuthoriseModel';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/models/LoginModel';
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { DialogNoticeComponent } from 'src/app/components/dialog-notice/dialog-notice.component';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.scss']
})
export class AuthorizeComponent implements OnInit {

  public email: string | undefined;
  public invalidEmail: boolean = false;
  public messageEmail: string | undefined;
  public password: string | undefined;
  public invalidPassword: boolean = false;
  public messagePassword: string | undefined;
  public spinnerFlag = false;
  private targetRoute: string = "/";

  constructor(private accountService: AccountService, private router: Router, public titleService: Title, private dialog: MatDialog) {
    this.titleService.setTitle("Войти в личный кабинет");
  }

  public resetValidFlag(): boolean {
    return false;
  }

  private validateLogin(): boolean {
    let valid = true;
    let toScroll = true;
    if (this.email == undefined || this.email.trim() == '') {
      this.invalidEmail = true;
      this.messageEmail = "Введите email пользователя";
      this.email = '';
      valid = false;
      if (toScroll) {
        document.getElementById("email")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.password == undefined || this.password.trim() == '') {
      this.invalidPassword = true;
      this.messagePassword = "Введите пароль";
      this.password = '';
      valid = false;
      if (toScroll) {
        document.getElementById("password")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    return valid;
  }

  public async login(): Promise<void> {
    this.spinnerFlag = true;
    if (!this.validateLogin()) {
      this.spinnerFlag = false;
      return;
    }
    if (this.email == undefined) {
      this.spinnerFlag = false;
      return;
    }
    if (this.password == undefined) {
      this.spinnerFlag = false;
      return;
    }
    var model = new LoginModel(this.email, this.password);
    await this.accountService.login(model)
      .then(
        (data) => {
          this.spinnerFlag = false;
          console.log("success");
          const token = data.token;
          const refreshToken = data.refreshToken;
          this.accountService.saveTokens(token, refreshToken);
          this.router.navigateByUrl(this.targetRoute);
          return;
        }
      )
      .catch(
        (error) => {
          this.spinnerFlag = false;
          const alertDialog = this.dialog.open(DialogNoticeComponent, { data: { message: "Некорректные логин и(или) пароль" } });
          console.log(error);
          this.email = '';
          this.password = '';
          return;
        }
      );
  }

  public async ngOnInit(): Promise<void> {
    this.spinnerFlag = false;
    await this.accountService.getAuthoriseModel();
  }

}
