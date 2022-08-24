import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/models/UserModel';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public user: UserModel = new UserModel(1, "Артём", "temka.kornilov@mail.ru", "89027434477");

  constructor() { }

  ngOnInit(): void {
  }

}
