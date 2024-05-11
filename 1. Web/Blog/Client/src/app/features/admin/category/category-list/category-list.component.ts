import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, debounceTime } from 'rxjs';
import { CategoryFilter } from 'src/app/shared/models/category/category-filter.model';
import { Category } from 'src/app/shared/models/category/category.model';
import { CategoryService } from 'src/app/shared/services/category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  filterModel: CategoryFilter = new CategoryFilter();
  categories: Category[] = [];
  searchTerm$ = new BehaviorSubject('');
  pageIndex = 1;
  pageSize = 5;
  total = 1;
  constructor(
    private categoryService: CategoryService,
  ) {}

  ngOnInit(): void {
    this.searchTerm$.pipe(debounceTime(200)).subscribe((_) => {
      this.filterModel.keyWord = this.searchTerm$.value.trim();
      this.pageIndex = 1;
      this.filterProductList();
    });
  }

  filterProductList(pageIndex?: number): void {
    this.filterModel.page = pageIndex ? pageIndex : this.filterModel.page;
    const filter = { ...this.filterModel };
    this.categoryService
      .getCategories(filter)
      .subscribe((result) => {
        this.categories = result.items;
        this.total = result.totalPages;
        this.pageIndex = this.filterModel.page;
        this.pageSize = this.filterModel.pageSize;
      });
  }

  changPageSize(event: number): void {
    this.filterModel.pageSize = event;
    this.filterProductList();
  }

  changePageIndex(event: number): void {
    this.filterProductList(event);
  }

  renderIndex(index: number): number{
    return index + this.pageSize * (this.pageIndex - 1) + 1;
  }

  delete(id: number): void {
    this.categoryService
      .deleteById(id)
      .subscribe((result) => {
        this.filterProductList();
      });
  }
}
