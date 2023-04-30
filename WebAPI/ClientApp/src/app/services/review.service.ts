import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { ReviewModel } from '../models/ReviewModel';
import { ReviewModelCreate } from '../models/ReviewModelCreate';
import { ReviewModelUpdate } from '../models/ReviewModelUpdate';
import { ReviewModelInfo } from '../models/ReviewModelInfo';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private http: HttpClient) { }

  public checkLenght(oldLength: number, newLength: number): boolean {
    if(oldLength >= newLength)
      return false;
    return true;
  }
  
  public async getByUserId(count: number): Promise<ReviewModel[]> {
    return await lastValueFrom(this.http.get<ReviewModel[]>(`api/review/user/${count}`));
  }

  public async getByAdvertId(id: number, count: number): Promise<ReviewModel[]> {
    return await lastValueFrom(this.http.get<ReviewModel[]>(`api/review/advert/${id}/${count}`));
  }

  public async getById(id: number): Promise<ReviewModelInfo> {
    return await lastValueFrom(this.http.get<ReviewModelInfo>(`api/review/${id}`));
  }

  public async remove(id: number): Promise<string> {
    return await lastValueFrom(this.http.delete(`api/review/${id}`, { responseType: 'text' }));
  }

  public async create(review: ReviewModelCreate): Promise<string> {
    return await lastValueFrom(this.http.post(`api/review/create`, review, { responseType: 'text' }));
  }

  public async update(review: ReviewModelUpdate): Promise<string> {
    return await lastValueFrom(this.http.put(`api/review/update`, review, { responseType: 'text' }));
  }

}
