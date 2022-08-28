import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.scss']
})
export class MyRequestsComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
  }

}
