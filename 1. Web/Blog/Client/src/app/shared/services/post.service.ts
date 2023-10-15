import { Inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PostFilter } from '../models/post/post-filter.model';
import { Post } from '../models/post/post.model';
import { PostCreateModel } from '../models/post/post-create.model';
import { PagePagination } from '../models/page-pagination.model';

@Injectable({
  providedIn: 'root'
})
export class PostService extends BaseService {

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') baseUrl: string,
    ) {
      super(httpClient, baseUrl);
     }

     getPosts(params: PostFilter): Observable<PagePagination<Post>> {
      const paramsFilter: PostFilter = { ...params };
    if (!paramsFilter.keyWord) {
      delete paramsFilter.keyWord;
    }
      return this.get('api/blog/posts', paramsFilter);
     }

     getAll(): Observable<Post[]> {
      return this.get('api/blog/posts/all');
     }

     getById(id: number): Observable<Post> {
      return this.get(`api/blog/post/${id}`);
     }

     public createOrUpdate(form: PostCreateModel, id?: number): Observable<any>{
      if (id)
      {
        return this.put<any>(`api/blog/posts/${id}`, form);
      }
      else{
        return this.post<any>(`api/blog/posts`, form);
      }
    }
}
