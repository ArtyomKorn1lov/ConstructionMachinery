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
  public invalidEmail: boolean = false;
  public messageEmail: string | undefined;
  public password: string | undefined;
  public invalidPassword: boolean = false;
  public messagePassword: string | undefined;
  public confirm_password: string | undefined;
  public invalidConfirm: boolean = false;
  public messageConfirm: string | undefined;
  public name: string | undefined;
  public invalidName: boolean = false;
  public messageName: string | undefined;
  public phone: string | undefined;
  public invalidPhone: boolean = false;
  public messagePhone: string | undefined;
  public address: string | undefined;
  public invalidAddress: boolean = false;
  public messageAddress: string | undefined;
  private targetRoute: string = "/";

  constructor(private accountService: AccountService, private router: Router) { }

  public resetValidFlag(): boolean {
    return false;
  }

  private validateRegistration(): boolean {
    let valid = true;
    let toScroll = true;
    if (this.email == undefined || this.email.trim() == '') {
      this.invalidEmail = true;
      this.messageEmail = "Введите Email пользователя";
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
    if (this.confirm_password == undefined || this.password.trim() == '') {
      this.invalidConfirm = true;
      this.messageConfirm = "Подтвердите пароль";
      this.confirm_password = '';
      valid = false;
      if (toScroll) {
        document.getElementById("confirm")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.name == undefined || this.name.trim() == '') {
      this.invalidName = true;
      this.messageName = "Введите ФИО пользователя";
      this.name = '';
      valid = false;
      if (toScroll) {
        document.getElementById("name")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.phone == undefined || this.phone.trim() == '') {
      this.invalidPhone = true;
      this.messagePhone = "Введите номер телефона";
      this.phone = '';
      valid = false;
      if (toScroll) {
        document.getElementById("phone")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.address == undefined || this.address.trim() == '') {
      this.invalidAddress = true;
      this.messageAddress = "Введите адрес арендодателя";
      this.address = '';
      valid = false;
      if (toScroll) {
        document.getElementById("address")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.confirm_password != this.password) {
      this.invalidPassword = true;
      this.messagePassword = "Пароли не совпадают, проверьте пароли";
      this.invalidConfirm = true;
      this.messageConfirm = "Пароли не совпадают, проверьте пароли";
      this.password = '';
      this.confirm_password = '';
      valid = false;
      if (toScroll) {
        document.getElementById("password")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    return valid;
  }

  public async registration(): Promise<void> {
    if (!this.validateRegistration())
      return;
    if (this.email == undefined || this.password == undefined || this.confirm_password == undefined
      || this.name == undefined || this.phone == undefined || this.address == undefined)
      return;
    let model = new RegisterModel(this.name, this.email, this.phone, this.address, this.password);
    await this.accountService.registration(model)
      .then(
        (data) => {
          console.log(data);
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
          this.confirm_password = '';
          this.name = '';
          this.phone = '';
          return;
        }
      );
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
