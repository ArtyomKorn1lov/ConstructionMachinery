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
  public visibility: boolean = false;
  public searchForm = new FormGroup({
    search: new FormControl<string>("")
  });
  private targetRoute: string = "/advert-list";

  constructor(public accountService: AccountService, private advertService: AdvertService, private router: Router, private formBuilder: FormBuilder) { }

  public SidenavEvent(): void {
    this.event.emit();
  }

  public FocusIn(): void {
    this.visibility = true;
  }

  public FocusOut(): void {
    this.visibility = false;
  }

  public OnSubmith(): void {
    if(this.searchForm.value.search == null || this.searchForm.value.search == undefined)
      this.searchForm.value.search = "";
    this.advertService.search = this.searchForm.value.search;
    this.advertService.searchFlag = true;
    this.router.navigateByUrl(this.targetRoute);
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
  }

}
