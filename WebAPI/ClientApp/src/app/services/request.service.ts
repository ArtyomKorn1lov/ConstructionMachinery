import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AvailabilityRequestModel } from '../models/AvailabilityRequestModel';
import { AvailabilityRequestModelForCustomer } from 'src/app/models/AvailabilityRequestModelForCustomer';
import { AvailabilityRequestModelForLandlord } from '../models/AvailabilityRequestModelForLandlord';
import { AvailableTimeModel } from '../models/AvailableTimeModel';
import { AvailabilityRequestModelCreate } from '../models/AvailabilityRequestModelCreate';
import { ConfirmModel } from '../models/ConfirmModel';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private http: HttpClient, private tokenService: TokenService) { }

  public clearIdLocalStorage(): void {
    localStorage.removeItem('requestId');
  }

  public clearAdvertIdLocalStorage(): void {
    localStorage.removeItem('advertRequestId');
  }

  public setIdInLocalStorage(id: number): void {
    localStorage.setItem('requestId', id.toString());
  }

  public getIdFromLocalStorage(): number {
    let id = localStorage.getItem('requestId');
    if (id == null)
      return 0;
    return parseInt(id);
  }

  public setAdvertIdInLocalStorage(id: number): void {
    localStorage.setItem('advertRequestId', id.toString());
  }

  public getAdvertIdInLocalStorage(): number {
    let id = localStorage.getItem('advertRequestId');
    if (id == null)
      return 0;
    return parseInt(id);
  }

  public checkLenght(oldLength: number, newLength: number): boolean {
    if(oldLength >= newLength || oldLength == 0)
      return false;
    return true;
  }

  public getListForCustomer(id: number, count: number): Observable<AvailabilityRequestModel[]> {
    this.tokenService.tokenVerify();
    return this.http.get<AvailabilityRequestModel[]>(`api/request/for-customer/${id}/${count}`);
  }

  public getListForLandlord(id: number, count: number): Observable<AvailabilityRequestModel[]> {
    this.tokenService.tokenVerify();
    return this.http.get<AvailabilityRequestModel[]>(`api/request/for-landlord/${id}/${count}`);
  }

  public getForCustomer(id: number): Observable<AvailabilityRequestModelForCustomer> {
    this.tokenService.tokenVerify();
    return this.http.get<AvailabilityRequestModelForCustomer>(`api/request/customer/${id}`);
  }

  public getForLandLord(id: number): Observable<AvailabilityRequestModelForLandlord> {
    this.tokenService.tokenVerify();
    return this.http.get<AvailabilityRequestModelForLandlord>(`api/request/landlord/${id}`);
  }

  public getAvailableTimesByAdvertId(id: number): Observable<AvailableTimeModel[]> {
    this.tokenService.tokenVerify();
    return this.http.get<AvailableTimeModel[]>(`api/request/times/${id}`);
  }

  public create(request: AvailabilityRequestModelCreate): Observable<string> {
    this.tokenService.tokenVerify();
    return this.http.post(`api/request/create`, request, { responseType: 'text' });
  }

  public confirm(model: ConfirmModel): Observable<string> {
    this.tokenService.tokenVerify();
    return this.http.put(`api/request/confirm`, model, { responseType: 'text' });
  }

  public remove(id: number): Observable<string> {
    this.tokenService.tokenVerify();
    return this.http.delete(`api/request/remove/${id}`, { responseType: 'text' });
  }

}
