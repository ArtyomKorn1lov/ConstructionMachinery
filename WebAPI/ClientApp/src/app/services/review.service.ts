import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
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
    if(oldLength >= newLength || oldLength == 0)
      return false;
    return true;
  }

  public setIdInLocalStorage(id: number): void {
    localStorage.setItem('reviewId', id.toString());
  }

  public getIdFromLocalStorage(): number {
    let id = localStorage.getItem('reviewId');
    if (id == null)
      return 0;
    return parseInt(id);
  }
  
  public getByUserId(count: number): Observable<ReviewModel[]> {
    return this.http.get<ReviewModel[]>(`api/review/user/${count}`);
  }

  public getByAdvertId(id: number, count: number): Observable<ReviewModel[]> {
    return this.http.get<ReviewModel[]>(`api/review/advert/${id}/${count}`);
  }

  public getById(id: number): Observable<ReviewModelInfo> {
    return this.http.get<ReviewModelInfo>(`api/review/${id}`);
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
