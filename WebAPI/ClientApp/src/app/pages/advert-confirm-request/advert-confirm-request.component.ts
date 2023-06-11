import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-advert-confirm-request',
  templateUrl: './advert-confirm-request.component.html',
  styleUrls: ['./advert-confirm-request.component.scss']
})
export class AdvertConfirmRequestComponent implements OnInit {

  constructor(private accountService: AccountService, public titleService: Title) {
    this.titleService.setTitle("Подтверждённые заявки в объявлениях");
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
