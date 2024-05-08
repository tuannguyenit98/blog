import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostDto } from 'src/app/shared/models/post/post-feature.model';
import { PostFilter } from 'src/app/shared/models/post/post-filter.model';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.scss']
})
export class CategoryDetailComponent implements OnInit, OnChanges {
  slug: string | undefined;
  posts: PostDto[] = [];
  pageIndex: number = 1;
  pageSize: number = 4;
  total: number = 2;
  filterModel: PostFilter = new PostFilter();
  constructor(
    private postService: PostService,
    private activatedRoute: ActivatedRoute
  ){

  }
  ngOnChanges(changes: SimpleChanges): void {
    this.filterPostList();
  }
  ngOnInit(): void {
    this.slug = this.activatedRoute.snapshot.paramMap.get('slug')!;
    this.filterPostList();
  }

  filterPostList(pageIndex?: number): void {
    this.filterModel.page = pageIndex ? pageIndex : this.filterModel.page;
    this.filterModel.pageSize = this.pageSize;
    const filter = { ...this.filterModel };
    this.postService
      .getPostsByCategorySlug(filter, this.slug!)
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
