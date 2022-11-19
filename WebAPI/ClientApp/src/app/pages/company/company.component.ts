import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss']
})
export class CompanyComponent implements OnInit {

  public name: string | undefined;
  public phone: string | undefined;
  public email: string | undefined;
  public description: string = "";

  constructor() { }

  ngOnInit(): void {
  }

}
