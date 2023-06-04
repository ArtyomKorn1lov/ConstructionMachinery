import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() event = new EventEmitter();
  @Output() eventfind = new EventEmitter();
  public visibility: boolean = false;
  public searchForm = new FormGroup({
    search: new FormControl<string>("")
  });
  private targetRoute: string = "/advert-list";

  constructor(public accountService: AccountService, private advertService: AdvertService, private router: Router, private formBuilder: FormBuilder) { }

  public sidenavEvent(): void {
    this.event.emit();
  }

  public focusIn(): void {
    this.visibility = true;
  }

  public focusOut(): void {
    this.visibility = false;
  }

  public onSubmith(): void {
    if(this.searchForm.value.search != null || this.searchForm.value.search != undefined) {
      if(this.searchForm.value.search == "")
        return;
      this.router.navigate([this.targetRoute], {
        queryParams: {
          search: this.searchForm.value.search
        },
        queryParamsHandling: 'merge'
      });
      this.eventfind.emit();
    }
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
