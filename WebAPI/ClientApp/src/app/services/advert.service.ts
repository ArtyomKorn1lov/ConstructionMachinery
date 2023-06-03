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

  public async getAll(startPublishDate: Date, endPublishDate: Date, startDate: Date, endDate: Date, startTime: number, endTime: number, startPrice: number, endPrice: number,
    keyWord: string, name: string, sort: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/adverts/${startPublishDate.toDateString()}/${endPublishDate.toDateString()}/${startDate.toDateString()}/${endDate.toDateString()}/${startTime}/${endTime}
    /${startPrice}/${endPrice}/${keyWord}/${name}/${sort}/${page}`));
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

  public async getByUser(startPublishDate: Date, endPublishDate: Date, startDate: Date, endDate: Date, startTime: number, endTime: number, startPrice: number, endPrice: number,
    keyWord: string, name: string, sort: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user/${startPublishDate.toDateString()}/${endPublishDate.toDateString()}/${startDate.toDateString()}/${endDate.toDateString()}/${startTime}/${endTime}
    /${startPrice}/${endPrice}/${keyWord}/${name}/${sort}/${page}`));
  }

  public async remove(id: number): Promise<string> {
    return await lastValueFrom(this.http.delete(`api/advert/remove/${id}`, { responseType: 'text' }));
  }

  public async getByName(name: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-name/${name}/${page}`));
  }

  public async getForRequestCustomer(page: number): Promise<AdvertModelForRequest[]> {
    return await lastValueFrom(this.http.get<AdvertModelForRequest[]>(`api/advert/adverts-for-request-customer/${page}`));
  }

  public async getForUpdate(id: number): Promise<AdvertModelUpdate> {
    return await lastValueFrom(this.http.get<AdvertModelUpdate>(`api/advert/for-update/${id}`));
  }

  public async update(advert: AdvertModelUpdate): Promise<string> {
    return await lastValueFrom(this.http.put(`api/advert/update`, advert, { responseType: 'text' }));
  }

  public async getSortByPriceMax(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-price-max/${page}`));
  }

  public async getSortByPriceMin(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-price-min/${page}`));
  }

  public async GetSortByRatingMax(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-rating-max/${page}`));
  }

  public async GetSortByRatingMin(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-rating-min/${page}`));
  }

  public async GetSortByDateMin(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-date-min/${page}`));
  }

  public async getSortByPriceMaxByName(name: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-price-max/${name}/${page}`));
  }

  public async getSortByPriceMinByName(name: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-price-min/${name}/${page}`));
  }

  public async GetSortByRatingMaxByName(name: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-rating-max/${name}/${page}`));
  }

  public async GetSortByRatingMinByName(name: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-rating-min/${name}/${page}`));
  }

  public async GetSortByDateMinByName(name: string, page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-date-min/${name}/${page}`));
  }

  public async getSortByPriceMaxByUserId(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-price-max/${page}`));
  }

  public async getSortByPriceMinByUserId(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-price-min/${page}`));
  }

  public async getSortByRatingMaxByUserId(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-rating-max/${page}`));
  }

  public async getSortByRatingMinByUserId(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-rating-min/${page}`));
  }

  public async getSortByDateMinByUserId(page: number): Promise<AdvertModelList[]> {
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-date-min/${page}`));
  }

}
