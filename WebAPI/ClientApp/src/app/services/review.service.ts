import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReviewModel } from '../models/ReviewModel';
import { ReviewModelCreate } from '../models/ReviewModelCreate';
import { ReviewModelUpdate } from '../models/ReviewModelUpdate';

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
  
  public getByUserId(count: number): Observable<ReviewModel[]> {
    return this.http.get<ReviewModel[]>(`api/review/user/${count}`);
  }

  public getByAdvertId(id: number, count: number): Observable<ReviewModel[]> {
    return this.http.get<ReviewModel[]>(`api/review/advert/${id}/${count}`);
  }

  public getById(id: number): Observable<ReviewModel> {
    return this.http.get<ReviewModel>(`api/review/${id}`);
  }

  public remove(id: number): Observable<string> {
    return this.http.delete(`api/review/${id}`, { responseType: 'text' });
  }

  public create(review: ReviewModelCreate): Observable<string> {
    return this.http.post(`api/review/create`, review, { responseType: 'text' });
  }

  public update(review: ReviewModelUpdate): Observable<string> {
    return this.http.put(`api/review/update`, review, { responseType: 'text' });
  }

}
