import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-private-area',
  templateUrl: './private-area.component.html',
  styleUrls: ['./private-area.component.scss']
})
export class PrivateAreaComponent implements OnInit {

  constructor(private accountService: AccountService, public titleService: Title) {
    this.titleService.setTitle("Личный кабинет");
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
