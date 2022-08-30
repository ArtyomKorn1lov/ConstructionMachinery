import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AvailabilityRequestModel } from '../models/AvailabilityRequestModel';
import { AvailabilityRequestModelForCustomer } from 'src/app/models/AvailabilityRequestModelForCustomer';
import { AvailabilityRequestModelForLandlord } from '../models/AvailabilityRequestModelForLandlord';
import { AvailableTimeModel } from '../models/AvailableTimeModel';
import { AvailabilityRequestModelCreate } from '../models/AvailabilityRequestModelCreate';
import { ConfirmModel } from '../models/ConfirmModel';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private http: HttpClient) { }

  public ClearLocalStorage(): void {
    localStorage.removeItem('requestId');
  }

  public SetIdInLocalStorage(id: number): void {
    localStorage.setItem('requestId', id.toString());
  }

  public GetIdFromLocalStorage(): number {
    let id = localStorage.getItem('requestId');
    if (id == null)
      return 0;
    return parseInt(id);
  }

  public GetListForCustomer(): Observable<AvailabilityRequestModel[]> {
    return this.http.get<AvailabilityRequestModel[]>(`api/request/for-customer`);
  }

  public GetListForLandlord(): Observable<AvailabilityRequestModel[]> {
    return this.http.get<AvailabilityRequestModel[]>(`api/request/for-landlord`);
  }

  public GetForCustomer(id: number): Observable<AvailabilityRequestModelForCustomer> {
    return this.http.get<AvailabilityRequestModelForCustomer>(`api/request/customer/${id}`);
  }

  public GetForLandLord(id: number): Observable<AvailabilityRequestModelForLandlord> {
    return this.http.get<AvailabilityRequestModelForLandlord>(`api/request/landlord/${id}`);
  }

  public GetAvailableTimesByAdvertId(id: number): Observable<AvailableTimeModel[]> {
    return this.http.get<AvailableTimeModel[]>(`api/request/times/${id}`);
  }

  public Create(request: AvailabilityRequestModelCreate): Observable<string> {
    return this.http.post(`api/request/create`, request, { responseType: 'text' });
  }

  public Confirm(model: ConfirmModel): Observable<string> {
    return this.http.put(`api/request/confirm`, model, { responseType: 'text' });
  }

  public Remove(id: number): Observable<string> {
    return this.http.delete(`api/request/remove/${id}`, { responseType: 'text' });
  }

}
