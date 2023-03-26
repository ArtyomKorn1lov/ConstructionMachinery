import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserUpdateModel } from 'src/app/models/UserUpdateModel';
import { AccountService } from 'src/app/services/account.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {

  public user: UserUpdateModel = new UserUpdateModel(0, "", "", "", "", "");
  public password: string | undefined;
  public confirm_password: string | undefined;
  public invalidEmail: boolean = false;
  public messageEmail: string | undefined;
  public invalidPassword: boolean = false;
  public messagePassword: string | undefined;
  public invalidConfirm: boolean = false;
  public messageConfirm: string | undefined;
  public invalidName: boolean = false;
  public messageName: string | undefined;
  public invalidPhone: boolean = false;
  public messagePhone: string | undefined;
  public invalidAddress: boolean = false;
  public messageAddress: string | undefined;
  private targetRoute: string = "/profile";

  constructor(private accountService: AccountService, private router: Router, private tokenService: TokenService) { }

  public resetValidFlag(): boolean {
    return false;
  }

  private validateEdit(): boolean {
    let valid = true;
    let toScroll = true;
    if (this.user.email == undefined || this.user.email.trim() == '') {
      this.invalidEmail = true;
      this.messageEmail = "Введите Email пользователя";
      this.user.email = '';
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
    if (this.user.name == undefined || this.user.name.trim() == '') {
      this.invalidName = true;
      this.messageName = "Введите ФИО пользователя";
      this.user.name = '';
      valid = false;
      if (toScroll) {
        document.getElementById("name")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.user.phone == undefined || this.user.phone.trim() == '') {
      this.invalidPhone = true;
      this.messagePhone = "Введите номер телефона";
      this.user.phone = '';
      valid = false;
      if (toScroll) {
        document.getElementById("phone")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.user.address == undefined || this.user.address.trim() == '') {
      this.invalidAddress = true;
      this.messageAddress = "Введите ардрес арендодателя";
      this.user.address = '';
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

  public async edit(): Promise<void> {
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    if (!this.validateEdit())
      return;
    if (this.user.email == undefined || this.password == undefined || this.confirm_password == undefined
      || this.user.name == undefined || this.user.phone == undefined || this.user.address == undefined)
      return;
    this.user.password = this.password;
    await this.accountService.update(this.user)
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
          this.password = '';
          this.confirm_password = '';
          return;
        }
      )
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    await this.accountService.getUserProfile()
      .then(
        (data) => {
          this.user.id = data.id;
          this.user.name = data.name;
          this.user.email = data.email;
          this.user.phone = data.phone;
          this.user.address = data.address;
        }
      )
      .catch(
        (error) => {
          console.log(error);
        }
      );
  }

}
