import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navigarion',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  @Output() event = new EventEmitter();
  @Output() eventfind = new EventEmitter();

  constructor(public accountService: AccountService) { }

  public sidenavEvent(): void {
    this.event.emit();
  }

  ngOnInit(): void {
  }

}
