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
  public visibility: boolean = false;

  constructor(public accountService: AccountService) { }

  public SidenavEvent(): void {
    this.event.emit();
  }

  public FocusIn(): void {
    this.visibility = true;
  }

  public FocusOut(): void {
    this.visibility = false;
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
  }

}
