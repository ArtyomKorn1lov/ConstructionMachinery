import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom, Observable } from 'rxjs';
import { AdvertModelList } from '../models/AdvertModelList';
import { AdvertModelInfo } from '../models/AdvertModelInfo';
import { AdvertModelCreate } from '../models/AdvertModelCreate';
import { AdvertModelForRequest } from '../models/AdvertModelForRequest';
import { AdvertModelUpdate } from '../models/AdvertModelUpdate';
import { TokenService } from './token.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {
  private advertCreate: AdvertModelCreate | undefined;
  private advertUpdate: AdvertModelUpdate | undefined;
  private queryParam: string = "";

  constructor(private http: HttpClient, private router: Router, private tokenService: TokenService) { }

  public clearLocalStorage(): void {
    localStorage.removeItem('advertId');
    localStorage.removeItem('page');
    localStorage.removeItem('filter');
  }

  public setIdInLocalStorage(id: number): void {
    localStorage.setItem('advertId', id.toString());
  }

  public setPageInLocalStorage(page: string): void {
    localStorage.setItem('page', page);
  }

  public setFilterInLocalStorage(filter: string): void {
    localStorage.setItem('filter', filter);
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

  public getFilterFromLocalStorage(): string {
    let page = localStorage.getItem('filter');
    if (page == null)
      return '';
    return page;
  }

  public setQueryParametr(param: string): void {
    this.queryParam = param;
  }

  public getQueryParametr(): string {
    return this.queryParam;
  }

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
    if (oldLength >= newLength || oldLength == 0)
      return false;
    return true;
  }

  public async getAll(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/adverts/${count}`));
  }

  public async getById(id: number): Promise<AdvertModelInfo> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelInfo>(`api/advert/by-id/${id}`));
  }

  public async createAdvert(advert: AdvertModelCreate): Promise<string> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.post(`api/advert/create`, advert, { responseType: 'text' }));
  }

  public async getByUser(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user/${count}`));
  }

  public async remove(id: number): Promise<string> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.delete(`api/advert/remove/${id}`, { responseType: 'text' }));
  }

  public async getByName(name: string, count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-name/${name}/${count}`));
  }

  public async getForRequestCustomer(count: number): Promise<AdvertModelForRequest[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelForRequest[]>(`api/advert/adverts-for-request-customer/${count}`));
  }

  public async getForUpdate(id: number): Promise<AdvertModelUpdate> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelUpdate>(`api/advert/for-update/${id}`));
  }

  public async update(advert: AdvertModelUpdate): Promise<string> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.put(`api/advert/update`, advert, { responseType: 'text' }));
  }

  public async getSortByPriceMax(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-price-max/${count}`));
  }

  public async getSortByPriceMin(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-price-min/${count}`));
  }

  public async GetSortByRatingMax(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-rating-max/${count}`));
  }

  public async GetSortByRatingMin(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-rating-min/${count}`));
  }

  public async GetSortByDateMin(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-date-min/${count}`));
  }

  public async getSortByPriceMaxByName(name: string, count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-price-max/${name}/${count}`));
  }

  public async getSortByPriceMinByName(name: string, count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-price-min/${name}/${count}`));
  }

  public async GetSortByRatingMaxByName(name: string, count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-rating-max/${name}/${count}`));
  }

  public async GetSortByRatingMinByName(name: string, count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-rating-min/${name}/${count}`));
  }

  public async GetSortByDateMinByName(name: string,count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-date-min/${name}/${count}`));
  }

  public async getSortByPriceMaxByUserId(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-price-max/${count}`));
  }

  public async getSortByPriceMinByUserId(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-price-min/${count}`));
  }

  public async getSortByRatingMaxByUserId(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-rating-max/${count}`));
  }

  public async getSortByRatingMinByUserId(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-rating-min/${count}`));
  }

  public async getSortByDateMinByUserId(count: number): Promise<AdvertModelList[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<AdvertModelList[]>(`api/advert/by-user-date-min/${count}`));
  }

}
