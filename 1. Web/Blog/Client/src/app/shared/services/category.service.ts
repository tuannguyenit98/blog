import { Inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category/category.model';
import { Observable } from 'rxjs';
import { CategoryFilter } from '../models/category/category-filter.model';
import { PagePagination } from '../models/page-pagination.model';
import { CategoryCreateModel } from '../models/category/category-create.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends BaseService {

  constructor(
    private httpClient: HttpClient,
    @Inject('API_BASE_URL') baseUrl: string,
    ) {
      super(httpClient, baseUrl);
     }

     getCategories(params: CategoryFilter): Observable<PagePagination<Category>> {
      const paramsFilter: CategoryFilter = { ...params };
    if (!paramsFilter.keyWord) {
      delete paramsFilter.keyWord;
    }
      return this.get('api/blog/categories', paramsFilter);
     }

     getAll(): Observable<Category[]> {
      return this.get('api/blog/categories/all');
     }

     getById(id: number): Observable<Category> {
      return this.get(`api/blog/categories/${id}`);
     }

     public createOrUpdate(form: CategoryCreateModel, id?: number): Observable<any>{
      if (id)
      {
        return this.put<any>(`api/blog/categories/${id}`, form);
      }
      else{
        return this.post<any>(`api/blog/categories`, form);
      }
    }

    deleteById(id: number): Observable<Category> {
      return this.delete(`api/blog/categories/${id}`);
    }
}
