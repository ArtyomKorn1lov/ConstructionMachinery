import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/models/UserModel';
import { AccountService } from 'src/app/services/account.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public user: UserModel = new UserModel(0, "", "", "", new Date(), "");
  private targetRoute: string = "/";

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, private router: Router) { }

  public async logout(): Promise<void> {
    if (this.accountService.logOut()) {
      alert("Успешный выход");
      this.router.navigateByUrl(this.targetRoute);
    }
    else
      alert("Ошибка выхода");
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    await this.accountService.getUserProfile().subscribe(data => {
      this.user = data;
      this.user.created = new Date(this.user.created);
    });
  }

}
