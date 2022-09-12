import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-my-adverts-confirm-list',
  templateUrl: './my-adverts-confirm-list.component.html',
  styleUrls: ['./my-adverts-confirm-list.component.scss']
})
export class MyAdvertsConfirmListComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
