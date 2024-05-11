import { Component, OnInit } from '@angular/core';
import { debounceTime } from 'rxjs';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { PostDto } from 'src/app/shared/models/post/post-feature.model';
import { PostFilter } from 'src/app/shared/models/post/post-filter.model';
import { Post } from 'src/app/shared/models/post/post.model';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit {
  filterModel: PostFilter = new PostFilter();
  posts: PostDto[] = [];
  searchTerm$ = new BehaviorSubject('');
  pageIndex = 1;
  pageSize = this.filterModel.pageSize;
  total: number = 3;
  constructor(
    private postService: PostService,
  ) {}

  ngOnInit(): void {
    this.searchTerm$.pipe(debounceTime(200)).subscribe((_) => {
      this.filterModel.searchTerm = this.searchTerm$.value.trim();
      this.pageIndex = 1;
      this.filterProductList();
    });
  }

  filterProductList(): void {
    const filter = { ...this.filterModel };
    this.postService
      .getPosts(filter)
      .subscribe((result) => {
        this.posts = result.items;
        this.total = result.totalCount;
        this.pageIndex = this.filterModel.page;
        this.pageSize = this.filterModel.pageSize;
      });
  }

  changPageSize(event: number): void {
    this.filterModel.pageSize = event;
    this.filterProductList();
  }

  changePageIndex(event: number): void {
    this.filterModel.page = event
    this.filterProductList();
  }

  renderIndex(index: number): number{
    return index + this.pageSize * (this.pageIndex - 1) + 1;
  }

  delete(id: number): void {
    this.postService
      .deleteById(id)
      .subscribe((_) => {
        this.filterProductList();
      });
  }
}
