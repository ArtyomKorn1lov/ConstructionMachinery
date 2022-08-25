import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/models/UserModel';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public user: UserModel = new UserModel(1, "Артём", "temka.kornilov@mail.ru", "89027434477");
  private targetRoute: string = "/";

  constructor(private accountService: AccountService, private router: Router) { }

  public async Logout(): Promise<void> {
    this.accountService.Logout().subscribe(data => {
      if(data == "success") {
        alert("Успешный выход");
        console.log(data);
        this.router.navigateByUrl(this.targetRoute)
        return;
      }
      alert("Ошибка выхода");
      console.log(data);
      return;
    });
  }

  ngOnInit(): void {
  }

}
