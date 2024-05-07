import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { Category } from 'src/app/shared/models/category/category.model';
import { PostFeatureModel, PostDto } from 'src/app/shared/models/post/post-feature.model';
import { PostFilter } from 'src/app/shared/models/post/post-filter.model';
import { CategoryService } from 'src/app/shared/services/category.service';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent implements OnInit {
  postFeature: PostFeatureModel = new PostFeatureModel();
  pageIndex: number = 1;
  pageSize: number = 4;
  total: number = 2;
  posts: PostDto[] = [];
  categories: Category[] = [];
  filterModel: PostFilter = new PostFilter();
  constructor(
    private postService: PostService,
    private categoryService: CategoryService
  ){

  } 
  ngOnInit(): void {
    forkJoin([this.postService.getPostFeatures(), this.categoryService.getAll()]).subscribe(([res1, res2]) => {
      this.postFeature = res1;
      this.categories = res2;
      this.filterPostList();
    })
  }

  filterPostList(pageIndex?: number): void {
    this.filterModel.page = pageIndex ? pageIndex : this.filterModel.page;
    this.filterModel.pageSize = this.pageSize;
    const filter = { ...this.filterModel };
    this.postService
      .getPosts(filter)
      .subscribe((result) => {
        this.posts = result.items;
        this.total = result.totalPages;
        this.pageIndex = this.filterModel.page;
      });
  }

  onPageChange(pageIndex: number) {
    this.filterModel.page = pageIndex;
    this.filterPostList();
  }
}
