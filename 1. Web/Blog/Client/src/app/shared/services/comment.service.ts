import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { CreateOrUpdateModel } from '../models/comment/comment-create-or-update.model';

@Injectable({
  providedIn: 'root'
})
export class CommentService extends BaseService {

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') baseUrl: string,
    ) {
      super(httpClient, baseUrl);
     }
     public createOrUpdate(form: CreateOrUpdateModel, id?: number): Observable<any>{
      if (id)
      {
        return this.put<any>(`api/blog/comments/${id}`, form);
      }
      else{
        return this.post<any>(`api/blog/comments`, form);
      }
    }
}
