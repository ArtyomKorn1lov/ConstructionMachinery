import { Component, OnInit } from '@angular/core';
import { TechniqueTypeList } from 'src/app/models/TechniqueTypeList';
import { AccountService } from 'src/app/services/account.service';
import { AdvertService } from 'src/app/services/advert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  public techniqueObject: TechniqueTypeList = new TechniqueTypeList();;
  private targetRoute: string = "/advert-list";

  constructor(private accountService: AccountService, private router: Router) { }

  public search(name: string): void {
    this.router.navigate([this.targetRoute], {
      queryParams: {
        search: name
      }
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
