import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { UserModel } from 'src/app/models/UserModel';
import { DatetimeService } from 'src/app/services/datetime.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  public user: UserModel = new UserModel(0, "", "", "", new Date(), "");

  constructor(public datetimeService: DatetimeService, private accountService: AccountService, public titleService: Title,
    private router: Router, private route: ActivatedRoute) {
    this.titleService.setTitle("Профиль пользователя");
  }

  public back(): void {
    let backUrl = this.getBackUrl();
    if (backUrl == undefined)
      backUrl = "/";
    this.router.navigateByUrl(backUrl);
  }

  private getBackUrl(): string {
    let backUrl = "";
    this.route.queryParams.subscribe(params => {
      backUrl = params["backUrl"];
    });
    return backUrl;
  }

  private getIdByQueryParams(): number {
    let id = 0;
    this.route.queryParams.subscribe(params => {
      id = params["id"];
    });
    if (id == undefined) {
      this.router.navigateByUrl("/");
      return 0;
    }
    return id;
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
    const backUrl = this.getBackUrl();
    if (backUrl == undefined) {
      this.router.navigateByUrl("/");
      return;
    }
    const id = this.getIdByQueryParams();
    await this.accountService.getProfileById(id)
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
