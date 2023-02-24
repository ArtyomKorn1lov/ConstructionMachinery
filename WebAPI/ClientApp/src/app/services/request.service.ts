import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { AvailabilityRequestModel } from '../models/AvailabilityRequestModel';
import { AvailabilityRequestModelForCustomer } from 'src/app/models/AvailabilityRequestModelForCustomer';
import { AvailabilityRequestModelForLandlord } from '../models/AvailabilityRequestModelForLandlord';
import { AvailableTimeModel } from '../models/AvailableTimeModel';
import { AvailabilityRequestModelCreate } from '../models/AvailabilityRequestModelCreate';
import { ConfirmModel } from '../models/ConfirmModel';
import { TokenService } from './token.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private http: HttpClient) { }

  public checkLenght(oldLength: number, newLength: number): boolean {
    if(oldLength >= newLength || oldLength == 0)
      return false;
    return true;
  }

  public async getListForCustomer(id: number, count: number): Promise<AvailabilityRequestModel[]> {
    return await lastValueFrom(this.http.get<AvailabilityRequestModel[]>(`api/request/for-customer/${id}/${count}`));
  }

  public async getListForLandlord(count: number): Promise<AvailabilityRequestModel[]> {
    return await lastValueFrom(this.http.get<AvailabilityRequestModel[]>(`api/request/for-landlord/${count}`));
  }

  public async getForCustomer(id: number): Promise<AvailabilityRequestModelForCustomer> {
    return await lastValueFrom(this.http.get<AvailabilityRequestModelForCustomer>(`api/request/customer/${id}`));
  }

  public async getForLandLord(id: number): Promise<AvailabilityRequestModelForLandlord> {
    return await lastValueFrom(this.http.get<AvailabilityRequestModelForLandlord>(`api/request/landlord/${id}`));
  }

  public async getAvailableTimesByAdvertId(id: number): Promise<AvailableTimeModel[]> {
    return await lastValueFrom(this.http.get<AvailableTimeModel[]>(`api/request/times/${id}`));
  }

  public async create(request: AvailabilityRequestModelCreate): Promise<string> {
    return await lastValueFrom(this.http.post(`api/request/create`, request, { responseType: 'text' }));
  }

  public async confirm(model: ConfirmModel): Promise<string> {
    return await lastValueFrom(this.http.put(`api/request/confirm`, model, { responseType: 'text' }));
  }

  public async remove(id: number): Promise<string> {
    return await lastValueFrom(this.http.delete(`api/request/remove/${id}`, { responseType: 'text' }));
  }

}
