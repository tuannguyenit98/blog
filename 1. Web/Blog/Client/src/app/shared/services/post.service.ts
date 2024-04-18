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
      return this.get(`api/blog/posts/${id}`);
     }

     public createOrUpdate(form: PostCreateModel, id?: number): Observable<any>{
      const formData: FormData = new FormData();
      formData.append('title', form.title);
      formData.append('metaTitle', form.metaTitle);
      formData.append('content', form.content);
      formData.append('fK_CategoryId', form.fK_CategoryId.toString());
      formData.append('file', form.file, form.file.name);
      if (id)
      {
        return this.http.put<any>(this.baseUrl + `api/blog/posts/${id}`, formData);
      }
      else{
        return this.http.post<any>(this.baseUrl + `api/blog/posts`, formData);
      }
    }

    deleteById(id: number): Observable<Post> {
      return this.delete(`api/blog/posts/${id}`);
    }
}
