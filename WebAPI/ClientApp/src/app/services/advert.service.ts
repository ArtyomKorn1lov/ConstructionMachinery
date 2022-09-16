import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdvertModelList } from '../models/AdvertModelList';
import { AdvertModelInfo } from '../models/AdvertModelInfo';
import { AdvertModelCreate } from '../models/AdvertModelCreate';
import { AvailableTimeModel } from '../models/AvailableTimeModel';
import { AdvertModelForRequest } from '../models/AdvertModelForRequest';
import { AdvertModelUpdate } from '../models/AdvertModelUpdate';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {
  private advertCreate: AdvertModelCreate | undefined;
  private advertUpdate: AdvertModelUpdate | undefined;

  constructor(private http: HttpClient) { }

  public clearLocalStorage(): void {
    localStorage.removeItem('advertId');
    localStorage.removeItem('page');
  }

  public setIdInLocalStorage(id: number): void {
    localStorage.setItem('advertId', id.toString());
  }

  public setPageInLocalStorage(page: string): void {
    localStorage.setItem('page', page);
  }

  public getIdFromLocalStorage(): number {
    let id = localStorage.getItem('advertId');
    if (id == null)
      return 0;
    return parseInt(id);
  }

  public getPageFromLocalStorage(): string {
    let page = localStorage.getItem('page');
    if (page == null)
      return '';
    return page;
  }

  public setAdvertCreateInService(advert: AdvertModelCreate): void {
    this.advertCreate = advert;
  }

  public getAdvertCreateFromService(): AdvertModelCreate {
    if (this.advertCreate == undefined)
      return new AdvertModelCreate("", "", 0, 0, new Date(), new Date(), 0, 0);
    return this.advertCreate;
  }

  public setAdvertUpdateInService(advert: AdvertModelUpdate): void {
    this.advertUpdate = advert;
  }

  public getAdvertUpdateFromService(): AdvertModelUpdate {
    if (this.advertUpdate == undefined)
      return new AdvertModelUpdate(0, "", "", 0, 0, [], new Date(), new Date(), 0, 0);
    return this.advertUpdate;
  }

  public sortByHour(list: AvailableTimeModel[]): AvailableTimeModel[] {
    let time;
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

  public checkLenght(oldLength: number, newLength: number): boolean {
    if(oldLength >= newLength)
      return false;
    return true;
  }

  public getAll(count: number): Observable<AdvertModelList[]> {
    return this.http.get<AdvertModelList[]>(`api/advert/adverts/${count}`);
  }

  public getById(id: number): Observable<AdvertModelInfo> {
    return this.http.get<AdvertModelInfo>(`api/advert/by-id/${id}`);
  }

  public createAdvert(advert: AdvertModelCreate): Observable<string> {
    return this.http.post(`api/advert/create`, advert, { responseType: 'text' });
  }

  public getByUser(count: number): Observable<AdvertModelList[]> {
    return this.http.get<AdvertModelList[]>(`api/advert/by-user/${count}`);
  }

  public remove(id: number): Observable<string> {
    return this.http.delete(`api/advert/remove/${id}`, { responseType: 'text' })
  }

  public getByName(name: string, count: number): Observable<AdvertModelList[]> {
    return this.http.get<AdvertModelList[]>(`api/advert/by-name/${name}/${count}`);
  }

  public getForRequestCustomer(count: number): Observable<AdvertModelForRequest[]> {
    return this.http.get<AdvertModelForRequest[]>(`api/advert/adverts-for-request-customer/${count}`);
  }

  public getForRequestLandlord(count: number): Observable<AdvertModelForRequest[]> {
    return this.http.get<AdvertModelForRequest[]>(`api/advert/adverts-for-request-landlord/${count}`);
  }

  public getForUpdate(id: number): Observable<AdvertModelUpdate> {
    return this.http.get<AdvertModelUpdate>(`api/advert/for-update/${id}`)
  }

  public update(advert: AdvertModelUpdate): Observable<string> {
    return this.http.put(`api/advert/update`, advert, { responseType: 'text' });
  }

}
