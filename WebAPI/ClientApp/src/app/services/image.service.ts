import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  public oldImageFlag: boolean = false;
  private imagesCreate: File[] = [];
  private filesBase64: string[] = [];
  private oldImageCount: number = 0;

  constructor(private http: HttpClient) { }

  public setImagesInService(images: File[], bases64: string[]): void {
    this.imagesCreate = images;
    this.filesBase64 = bases64;
  }

  public getImagesFromService(): File[] {
    return this.imagesCreate;
  }

  public getBases64FromService(): string[] {
    return this.filesBase64;
  }

  public setImageCountInService(count: number): void {
    this.oldImageCount = count;
  }

  public getOldImageCountFromService(): number {
    return this.oldImageCount;
  }

  public create(uploadImages: FormData): Observable<string> {
    return this.http.post(`api/image/create`, uploadImages, { responseType: 'text' });
  }

  public update(uploadImage: FormData, id: number): Observable<string> {
    return this.http.put(`api/image/update/${id}`, uploadImage, { responseType: 'text' });
  }

  public remove(imagesId: number[]): Observable<string> {
    return this.http.post(`api/image/remove`, imagesId, { responseType: 'text' });
  }

}
