import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { AdvertModelList } from '../models/AdvertModelList';
import { AdvertModelInfo } from '../models/AdvertModelInfo';
import { AdvertModelCreate } from '../models/AdvertModelCreate';
import { AdvertModelForRequest } from '../models/AdvertModelForRequest';
import { AdvertModelUpdate } from '../models/AdvertModelUpdate';
import { Router } from '@angular/router';
import { AdvertModelDetail } from '../models/AdvertModelDetail';
import { FilterModel } from '../models/FilterModel';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {
  private advertCreate: AdvertModelCreate | undefined;
  private advertUpdate: AdvertModelUpdate | undefined;
  public advertLenght: number = 0;

  constructor(private http: HttpClient, private router: Router) { }

  public setAdvertCreateInService(advert: AdvertModelCreate): void {
    this.advertCreate = advert;
  }

  public getAdvertCreateFromService(): AdvertModelCreate {
    if (this.advertCreate == undefined)
      return new AdvertModelCreate("", new Date(), "", "", "", 0, 0, new Date(), new Date(), 0, 0);
    return this.advertCreate;
  }

  public setAdvertUpdateInService(advert: AdvertModelUpdate): void {
    this.advertUpdate = advert;
  }

  public getAdvertUpdateFromService(): AdvertModelUpdate {
    if (this.advertUpdate == undefined)
      return new AdvertModelUpdate(0, "", new Date(), "", "", "", 0, 0, [], new Date(), new Date(), 0, 0);
    return this.advertUpdate;
  }

  public checkLenght(oldLength: number, newLength: number): boolean {
    if (oldLength >= newLength)
      return false;
    return true;
  }

  public async getAll(startPublishDate: string, endPublishDate: string, startDate: string, endDate: string, startTime: number, endTime: number, startPrice: number, endPrice: number,
    keyWord: string, name: string, sort: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/adverts/${startPublishDate}/${endPublishDate}/${startDate}/${endDate}/${startTime}/${endTime}/${startPrice}/${endPrice}/${keyWord}/${name}/${sort}/${page}`));
  }

  public async getById(id: number): Promise<AdvertModelInfo> {
    return await lastValueFrom(this.http.get<AdvertModelInfo>(`api/advert/by-id/${id}`));
  }

  public async getDetailAdvert(id: number): Promise<AdvertModelDetail> {
    return await lastValueFrom(this.http.get<AdvertModelDetail>(`api/advert/detail/${id}`));
  }

  public async createAdvert(advert: AdvertModelCreate): Promise<string> {
    return await lastValueFrom(this.http.post(`api/advert/create`, advert, { responseType: 'text' }));
  }

  public async getByUser(startPublishDate: string, endPublishDate: string, startDate: string, endDate: string, startTime: number, endTime: number, startPrice: number, endPrice: number,
    keyWord: string, name: string, sort: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user/${startPublishDate}/${endPublishDate}/${startDate}/${endDate}/${startTime}/${endTime}/${startPrice}/${endPrice}/${keyWord}/${name}/${sort}/${page}`));
  }

  public async remove(id: number): Promise<string> {
    return await lastValueFrom(this.http.delete(`api/advert/remove/${id}`, { responseType: 'text' }));
  }

  public async getForRequestCustomer(page: number): Promise<AdvertModelForRequest[]> {
    return await lastValueFrom(this.http.get<AdvertModelForRequest[]>(`api/advert/adverts-for-request-customer/${page}`));
  }

  public async getForRequestLandlord(page: number): Promise<AdvertModelForRequest[]> {
    return await lastValueFrom(this.http.get<AdvertModelForRequest[]>(`api/advert/adverts-for-request-landlord/${page}`));
  }

  public async getForUpdate(id: number): Promise<AdvertModelUpdate> {
    return await lastValueFrom(this.http.get<AdvertModelUpdate>(`api/advert/for-update/${id}`));
  }

  public async update(advert: AdvertModelUpdate): Promise<string> {
    return await lastValueFrom(this.http.put(`api/advert/update`, advert, { responseType: 'text' }));
  }

}
