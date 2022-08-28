import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-confirm-list-info',
  templateUrl: './confirm-list-info.component.html',
  styleUrls: ['./confirm-list-info.component.scss']
})
export class ConfirmListInfoComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
  }

}
