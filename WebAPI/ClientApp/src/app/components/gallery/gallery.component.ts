import { Component, Input, OnInit } from '@angular/core';
import { ImageModel } from 'src/app/models/ImageModel';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent implements OnInit {

  @Input() images: ImageModel[] = [];
  @Input() dotCount: number = 0;
  public dotArray: boolean[] = []
  public index: number = 0;

  public nexImage(): void {
    this.dotArray[this.index] = false;
    this.index++;
    if(this.index == this.images.length) {
      this.index = 0;
    }
    this.dotArray[this.index] = true;
  }

  constructor() { }

  public async ngOnInit(): Promise<void> {
    for(let count = 0; count < this.dotCount; count++) {
      await this.dotArray.push(false);
    }
    if(this.dotArray.length > 0) {
      this.dotArray[0] = true;
    }
  }

}
