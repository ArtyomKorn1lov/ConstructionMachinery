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
  public visibility: boolean = true;

  constructor(public accountService: AccountService) { }

  public SidenavEvent(): void {
    this.event.emit();
  }

  /*public isAuthorized() {
    return !!this.model.name;
  }*/

  public InputClick(): void {
    this.visibility = !this.visibility;
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
  }

}
