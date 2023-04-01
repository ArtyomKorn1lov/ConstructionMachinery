import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { MailModel } from '../models/MailModel';

@Injectable({
  providedIn: 'root'
})
export class MailService {

  constructor(private http: HttpClient) { }

  public async sendMail(model: MailModel): Promise<string> {
    return await lastValueFrom(this.http.post("api/mail/send", model, { responseType: 'text' }));
  }
}
