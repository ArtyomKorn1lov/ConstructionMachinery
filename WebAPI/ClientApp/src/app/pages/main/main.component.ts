import { Component, OnInit } from '@angular/core';
import { TechniqueTypeList } from 'src/app/models/TechniqueTypeList';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  public techniqueObject: TechniqueTypeList = new TechniqueTypeList();;

  constructor(private accountService: AccountService) { }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetAuthoriseModel();
  }

}
