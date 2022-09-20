import { Component, Input, OnInit } from '@angular/core';
import { ImageModel } from 'src/app/models/ImageModel';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent implements OnInit {

  @Input() images: ImageModel[] = [];
  public index: number = 0;

  public nexImage(): void {
    this.index++;
    if(this.index == this.images.length) {
      this.index = 0;
    }
  }

  constructor() { }

  ngOnInit(): void {
  }

}
