import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-confirm-list',
  templateUrl: './confirm-list.component.html',
  styleUrls: ['./confirm-list.component.scss']
})
export class ConfirmListComponent implements OnInit {

  constructor(private accountService: AccountService, public titleService: Title) {
    this.titleService.setTitle("Подтверждение заявок");
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
