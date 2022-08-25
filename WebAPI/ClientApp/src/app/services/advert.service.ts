import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdvertModelList } from '../models/AdvertModelList';
import { AdvertModelInfo } from '../models/AdvertModelInfo';
import { AdvertModelCreate } from '../models/AdvertModelCreate';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  private advertCreate: AdvertModelCreate | undefined; 

  constructor(private http: HttpClient) { }

  public ClearLocalStorage(): void {
    localStorage.removeItem('advertId');
  }

  public SetIdInLocalStorage(id: number): void {
    localStorage.setItem('advertId', id.toString());
  }

  public GetIdFromLocalStorage(): number {
    let id = localStorage.getItem('advertId');
    if(id == null) 
      return 0;
    return parseInt(id);
  }

  public SetAdvertCreateInService(advert: AdvertModelCreate): void {
    this.advertCreate = advert;
  }

  public GetAdvertCreateFromService(): AdvertModelCreate {
    if(this.advertCreate == undefined)
      return new AdvertModelCreate("", "", 0, 0, []);
    return this.advertCreate;
  }

  public GetAll(): Observable<AdvertModelList[]> {
    return this.http.get<AdvertModelList[]>(`api/advert/adverts`);
  }

  public GetById(id: number): Observable<AdvertModelInfo> {
    return this.http.get<AdvertModelInfo>(`api/advert/by-id/${id}`);
  }

  public CreateAdvert(advert: AdvertModelCreate): Observable<string> {
    return this.http.post(`api/advert/create`, advert, { responseType: 'text' });
  }

}
