import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-my-adverts-requests',
  templateUrl: './my-adverts-requests.component.html',
  styleUrls: ['./my-adverts-requests.component.scss']
})
export class MyAdvertsRequestsComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
