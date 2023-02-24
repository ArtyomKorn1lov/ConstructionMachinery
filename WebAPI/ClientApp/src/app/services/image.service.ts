import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

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

  public async create(uploadImages: FormData): Promise<string> {
    return await lastValueFrom(this.http.post(`api/image/create`, uploadImages, { responseType: 'text' }));
  }

  public async update(uploadImage: FormData, id: number): Promise<string> {
    return await lastValueFrom(this.http.put(`api/image/update/${id}`, uploadImage, { responseType: 'text' }));
  }

  public async remove(imagesId: number[]): Promise<string> {
    return await lastValueFrom(this.http.post(`api/image/remove`, imagesId, { responseType: 'text' }));
  }

}
