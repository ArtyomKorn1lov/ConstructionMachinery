import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private imageCreate: File | undefined;

  constructor(private http: HttpClient) { }

  public SetImageInService(image: File): void {
    this.imageCreate = image;
  }

  public GetImageFromService(): File {
    if(this.imageCreate == undefined)
      return new File([""], "");
    return this.imageCreate;
  }

  public Create(uploadImage: FormData): Observable<string> {
    return this.http.post(`api/image/create`, uploadImage, { responseType: 'text' });
  }

}