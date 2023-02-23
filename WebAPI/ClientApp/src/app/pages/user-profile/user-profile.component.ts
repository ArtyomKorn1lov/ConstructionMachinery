import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { UserModel } from 'src/app/models/UserModel';
import { DatetimeService } from 'src/app/services/datetime.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  public user: UserModel = new UserModel(0, "", "", "", new Date(), "");
  private targetRoute: string = "/";

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, private router: Router) { }

  public back(): void {
    this.router.navigateByUrl(this.targetRoute);
    this.accountService.clearLocalStorage();
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    this.targetRoute = this.accountService.getPageFromLocalStorage();
    await this.accountService.getUserById(this.accountService.getUserIdFromLocalStorage())
    .then(
      (data) => {
        this.user = data;
        this.user.created = new Date(this.user.created);
      }
    )
    .catch(
      (error) => {
        console.log(error);
      }
    );
  }

}
