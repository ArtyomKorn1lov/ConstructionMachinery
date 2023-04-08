import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/models/UserModel';
import { AccountService } from 'src/app/services/account.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { Router } from '@angular/router';
import { TokenService } from 'src/app/services/token.service';
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { DialogConfirmComponent } from 'src/app/components/dialog-confirm/dialog-confirm.component';
import { DialogNoticeComponent } from 'src/app/components/dialog-notice/dialog-notice.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public user: UserModel = new UserModel(0, "", "", "", new Date(), "");
  private targetRoute: string = "/";

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, private router: Router,
    private tokenService: TokenService, public titleService: Title, private dialog: MatDialog) {
    this.titleService.setTitle("Профиль пользователя");
  }

  public async logout(): Promise<void> {
    const confirmDialog = this.dialog.open(DialogConfirmComponent, { data: { message: "Вы уверены, что хотите выйти?" } });
    confirmDialog.afterClosed().subscribe(async result => {
      if (result.flag) {
        if (this.accountService.logOut()) {
          this.router.navigateByUrl(this.targetRoute);
        }
        else {
          const alertDialog = this.dialog.open(DialogNoticeComponent, { data: { message: "Ошибка выхода" } });
        }
      }
      return;
    }); 
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    const tokenResult = await this.tokenService.tokenVerify();
    if (!tokenResult)
      this.router.navigate(["/authorize"]);
    await this.accountService.getUserProfile()
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
