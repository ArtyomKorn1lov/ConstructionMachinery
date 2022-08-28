import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-my-request-info',
  templateUrl: './my-request-info.component.html',
  styleUrls: ['./my-request-info.component.scss']
})
export class MyRequestInfoComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
  }

}
