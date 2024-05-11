import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { PostDto } from 'src/app/shared/models/post/post-feature.model';
import { PostFilter } from 'src/app/shared/models/post/post-filter.model';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-post-search',
  templateUrl: './post-search.component.html',
  styleUrls: ['./post-search.component.scss']
})
export class PostSearchComponent implements OnInit, OnDestroy {
  posts: PostDto[] = [];
  pageIndex: number = 1;
  pageSize: number = 4;
  total: number = 2;
  filterModel: PostFilter = new PostFilter();
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  constructor(
    private postService: PostService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ){

  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.loading = true;
    this.subscription.add(this.activatedRoute.queryParams.subscribe((params: Params) => {
      this.filterModel.searchTerm = params['searchTerm'];
      this.filterPostList();
    }))
  }

  filterPostList(pageIndex?: number): void {
    this.filterModel.page = pageIndex ? pageIndex : this.filterModel.page;
    this.filterModel.pageSize = this.pageSize;
    const filter = { ...this.filterModel };
    this.subscription.add(this.postService
      .getPosts(filter)
      .subscribe((result) => {
        this.posts = result.items;
        this.total = result.totalPages;
        this.pageIndex = this.filterModel.page;
        this.loading = false;
      }));
  }

  onPageChange(pageIndex: number) {
    this.filterModel.page = pageIndex;
    this.filterPostList();
  }

  handleSearch(): void{
    if(this.filterModel.searchTerm){
      this.router.navigate(['/search'], {
        queryParams: {searchTerm: this.filterModel.searchTerm}
      })
    }
  }
}
