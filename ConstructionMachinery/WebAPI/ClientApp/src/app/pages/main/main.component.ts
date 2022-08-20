import { Component, OnInit } from '@angular/core';
import { TechniqueTypeList } from 'src/app/models/TechniqueTypeList';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  public techniqueObject: TechniqueTypeList = new TechniqueTypeList();;

  constructor() { }

  ngOnInit(): void {
  }

}
