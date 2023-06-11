import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-confirmed-request',
  templateUrl: './confirmed-request.component.html',
  styleUrls: ['./confirmed-request.component.scss']
})
export class ConfirmedRequestComponent implements OnInit {

  constructor(private accountService: AccountService, public titleService: Title) {
    this.titleService.setTitle("Подтверждённые заявки");
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
