import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  public oldImageFlag: boolean = false;
  private imageCreate: File | undefined;

  constructor(private http: HttpClient) { }

  public setImageInService(image: File): void {
    this.imageCreate = image;
  }

  public getImageFromService(): File {
    if(this.imageCreate == undefined)
      return new File([""], "");
    return this.imageCreate;
  }

  public create(uploadImage: FormData): Observable<string> {
    return this.http.post(`api/image/create`, uploadImage, { responseType: 'text' });
  }

  public update(uploadImage: FormData, id: number): Observable<string> {
    return this.http.post(`api/image/update/${id}`, uploadImage, { responseType: 'text' });
  }

  public remove(id: number): Observable<string> {
    return this.http.delete(`api/image/remove/${id}`, { responseType: 'text' });
  }

}
