import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserUpdateModel } from 'src/app/models/UserUpdateModel';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {

  public user: UserUpdateModel = new UserUpdateModel(0, "", "", "", "", "");
  public password: string | undefined;
  public confirm_password: string | undefined;
  private targetRoute: string = "/profile";

  constructor(private accountService: AccountService, private router: Router) { }

  public async edit(): Promise<void> {
    if (this.user.email == undefined || this.user.email.trim() == '') {
      alert("Введите Email пользователя");
      this.user.email = '';
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
    if (this.user.name == undefined || this.user.name.trim() == '') {
      alert("Введите ФИО пользователя");
      this.user.name = '';
      return;
    }
    if (this.user.phone == undefined || this.user.phone.trim() == '') {
      alert("Введите номер телефона");
      this.user.phone = '';
      return;
    }
    if (this.user.address == undefined || this.user.address.trim() == '') {
      alert("Введите ардрес арендодателя");
      this.user.address = '';
      return;
    }
    if (this.confirm_password != this.password) {
      alert("Пароли не совпадают, проверьте пароли");
      this.password = '';
      this.confirm_password = '';
      return;
    }
    this.user.password = this.password;
    await this.accountService.update(this.user).subscribe({
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
        this.password = '';
        this.confirm_password = '';
        return;
      }
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    await this.accountService.getUserProfile().subscribe(data => {
      this.user.id = data.id;
      this.user.name = data.name;
      this.user.email = data.email;
      this.user.phone = data.phone;
      this.user.address = data.address;
    });
  }
  
}
