import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdvertModelList } from '../models/AdvertModelList';
import { AdvertModelInfo } from '../models/AdvertModelInfo';
import { AdvertModelCreate } from '../models/AdvertModelCreate';
import { AvailableTimeModel } from '../models/AvailableTimeModel';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  private advertCreate: AdvertModelCreate | undefined;

  constructor(private http: HttpClient) { }

  public ClearLocalStorage(): void {
    localStorage.removeItem('advertId');
    localStorage.removeItem('page');
  }

  public SetIdInLocalStorage(id: number): void {
    localStorage.setItem('advertId', id.toString());
  }

  public SetPageInLocalStorage(page: string): void {
    localStorage.setItem('page', page);
  }

  public GetIdFromLocalStorage(): number {
    let id = localStorage.getItem('advertId');
    if (id == null)
      return 0;
    return parseInt(id);
  }

  public GetPageFromLocalStorage(): string {
    let page = localStorage.getItem('page');
    if (page == null)
      return '';
    return page;
  }

  public SetAdvertCreateInService(advert: AdvertModelCreate): void {
    this.advertCreate = advert;
  }

  public GetAdvertCreateFromService(): AdvertModelCreate {
    if (this.advertCreate == undefined)
      return new AdvertModelCreate("", "", 0, 0, []);
    return this.advertCreate;
  }

  public SortByHour(list: AvailableTimeModel[]): AvailableTimeModel[] {
    var time;
    for(let i = 0; i < list.length; i++) {
      for(let j = 0; j < list.length - i - 1; j++) {
        if(list[j].date.getHours() > list[j+1].date.getHours()) {
          time = list[j];
          list[j] = list[j+1];
          list[j+1] = time;
        }
      }
    }
    return list;
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

  public GetByUser(): Observable<AdvertModelList[]> {
    return this.http.get<AdvertModelList[]>(`api/advert/by-user`);
  }

  public Remove(id: number): Observable<string> {
    return this.http.delete(`api/advert/remove/${id}`, { responseType: 'text' })
  }

}
