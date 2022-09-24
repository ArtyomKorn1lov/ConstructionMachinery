import { Component, Input, OnInit } from '@angular/core';
import { ImageModel } from 'src/app/models/ImageModel';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent implements OnInit {

  @Input() images: ImageModel[] = [];
  public dotArray: boolean[] = []
  public activeIndex: number = 0;

  public nexImage(): void {
    this.activeIndex++;
    if(this.activeIndex == this.images.length) {
      this.activeIndex = 0;
    }
  }

  constructor() { }

  ngOnInit(): void {
  }

}
