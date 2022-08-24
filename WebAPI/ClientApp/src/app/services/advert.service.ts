import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdvertModelList } from '../models/AdvertModelList';

@Injectable({
  providedIn: 'root'
})
export class AdvertService {

  constructor(private http: HttpClient) { }

  public GetAll(): Observable<AdvertModelList[]> {
    return this.http.get<AdvertModelList[]>(`api/advert/advert-list`);
  }

}
