import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  public oldImageFlag: boolean = false;
  private imagesCreate: File[] = [];

  constructor(private http: HttpClient) { }

  public setImagesInService(images: File[]): void {
    this.imagesCreate = images;
  }

  public getImagesFromService(): File[] {
    return this.imagesCreate;
  }

  public create(uploadImages: FormData): Observable<string> {
    return this.http.post(`api/image/create`, uploadImages, { responseType: 'text' });
  }

  public update(uploadImage: FormData, id: number): Observable<string> {
    return this.http.post(`api/image/update/${id}`, uploadImage, { responseType: 'text' });
  }

  public remove(imagesId: number[]): Observable<string> {
    return this.http.post(`api/image/remove`, imagesId, { responseType: 'text' });
  }

}
