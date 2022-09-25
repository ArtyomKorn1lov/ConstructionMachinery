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
  public posX?: number = 0;

  public nexImage(): void {
    this.activeIndex++;
  }

  public backImage(): void {
    this.activeIndex--;
  }

  public touchStart(event: TouchEvent): void {
    this.posX = event.targetTouches.item(0)?.pageX;
  }

  public touchEnd(event: TouchEvent): void {
    let x = event.changedTouches.item(0)?.pageX;
    if(this.posX && x != null) {
      if((x - this.posX) > 50) {
        this.moveGallery("right");
      }
      if((x - this.posX) < -50) {
        this.moveGallery("left");
      }
    }
      
  }

  public moveGallery(direction: string): void {
    if(direction == "right") {
      let element = document.getElementById("gallery__container");
      if(element && this.activeIndex != 0) {
        let imageWidth = (element.scrollWidth ?? 0) / this.images.length;
        let currentMargin = this.activeIndex * imageWidth;
        element.style.left = (-currentMargin + imageWidth) + "px";
        this.backImage();
      }
    }
    if(direction == "left") {
      let element = document.getElementById("gallery__container");
      if(element && this.activeIndex != this.images.length - 1) {
        let imageWidth = (element.scrollWidth ?? 0) / this.images.length;
        let currentMargin = this.activeIndex * imageWidth;
        element.style.left = (-currentMargin - imageWidth) + "px";
        this.nexImage();
      }
    }
  }

  constructor() { }

  ngOnInit(): void {
  }

}
