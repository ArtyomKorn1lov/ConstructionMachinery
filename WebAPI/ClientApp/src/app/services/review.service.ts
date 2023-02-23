import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom, Observable } from 'rxjs';
import { ReviewModel } from '../models/ReviewModel';
import { ReviewModelCreate } from '../models/ReviewModelCreate';
import { ReviewModelUpdate } from '../models/ReviewModelUpdate';
import { ReviewModelInfo } from '../models/ReviewModelInfo';
import { TokenService } from './token.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private http: HttpClient, private router: Router, private tokenService: TokenService) { }

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
  
  public async getByUserId(count: number): Promise<ReviewModel[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<ReviewModel[]>(`api/review/user/${count}`));
  }

  public async getByAdvertId(id: number, count: number): Promise<ReviewModel[]> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<ReviewModel[]>(`api/review/advert/${id}/${count}`));
  }

  public async getById(id: number): Promise<ReviewModelInfo> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.get<ReviewModelInfo>(`api/review/${id}`));
  }

  public async remove(id: number): Promise<string> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.delete(`api/review/${id}`, { responseType: 'text' }));
  }

  public async create(review: ReviewModelCreate): Promise<string> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.post(`api/review/create`, review, { responseType: 'text' }));
  }

  public async update(review: ReviewModelUpdate): Promise<string> {
    this.tokenService.tokenVerify();
    return await lastValueFrom(this.http.put(`api/review/update`, review, { responseType: 'text' }));
  }

}
