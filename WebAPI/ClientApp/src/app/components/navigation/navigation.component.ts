import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-navigarion',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  @Output() event = new EventEmitter();

  constructor() { }

  public SidenavEvent(): void {
    this.event.emit();
  }

  ngOnInit(): void {
  }

}
