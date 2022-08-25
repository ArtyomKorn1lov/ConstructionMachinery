import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { AuthoriseModel } from 'src/app/models/AuthoriseModel';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() event = new EventEmitter();
  public model: AuthoriseModel = new AuthoriseModel(" ");

  constructor(private accountService: AccountService) { }

  public SidenavEvent(): void {
    this.event.emit();
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.IsUserAuthorized().subscribe(data => {
      this.model = data;
    });
    console.log(this.model);
  }

}
