import { Component, Input, OnInit } from '@angular/core';
import { ImageModel } from 'src/app/models/ImageModel';

enum SwipeDirection {
  Left = "LEFT",
  Right = "RIGHT"
};

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss'],
})
export class GalleryComponent implements OnInit {
  @Input() images: ImageModel[] = [];

  private posX?: number = 0;

  public activeIndex: number = 0;

  public nexImage(): void {
    if (this.activeIndex === this.images.length - 1) {
      this.activeIndex = 0;
    } else {
      this.activeIndex++;
    }
  }

  public backImage(): void {
    if (this.activeIndex === 0) {
      this.activeIndex = this.images.length - 1;
    } else {
      this.activeIndex--;
    }
  }

  public touchStart(event: TouchEvent): void {
    if (this.images.length !== 1) {
      this.posX = event.targetTouches.item(0)?.pageX;
    }
  }

  public touchEnd(event: TouchEvent): void {
    if (this.images.length !== 1) {
      let x = event.changedTouches.item(0)?.pageX;
      if (this.posX && x != null) {
        if (x - this.posX > 50) {
          this.moveGallery(SwipeDirection.Right);
        }
        if (x - this.posX < -50) {
          this.moveGallery(SwipeDirection.Left);
        }
      }
    }
  }

  public moveGallery(direction: string): void {
    if (direction == SwipeDirection.Right) {
      const images: HTMLCollectionOf<Element> =
        document.getElementsByClassName('gallery__image');
      const nextIndex =
        this.activeIndex === 0 ? this.images.length - 1 : this.activeIndex - 1;

      images[this.activeIndex].classList.add('-active');
      images[nextIndex].classList.add('-active');
      images[this.activeIndex].classList.add('-moving-right-from-center');
      images[nextIndex].classList.add('-moving-right-from-left');

      setTimeout(() => {
        images[this.activeIndex].classList.remove('-moving-right-from-center');
        images[nextIndex].classList.remove('-moving-right-from-left');

        images[this.activeIndex].classList.remove('-active');
        this.backImage();
      }, 300);
    }
    if (direction == SwipeDirection.Left) {
      const images: HTMLCollectionOf<Element> =
        document.getElementsByClassName('gallery__image');
      const nextIndex =
        this.activeIndex === this.images.length - 1 ? 0 : this.activeIndex + 1;

      images[this.activeIndex].classList.add('-active');
      images[nextIndex].classList.add('-active');
      images[this.activeIndex].classList.add('-moving-left-from-center');
      images[nextIndex].classList.add('-moving-left-from-right');

      setTimeout(() => {
        images[this.activeIndex].classList.remove('-moving-left-from-center');
        images[nextIndex].classList.remove('-moving-left-from-right');

        images[this.activeIndex].classList.remove('-active');
        this.nexImage();
      }, 300);
    }
  }

  constructor() {}

  ngOnInit(): void {}
}
