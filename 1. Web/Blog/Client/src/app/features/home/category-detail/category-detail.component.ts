import {
  Component,
  DoCheck,
  OnChanges,
  OnDestroy,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';
import { PostDto } from 'src/app/shared/models/post/post-feature.model';
import { PostFilter } from 'src/app/shared/models/post/post-filter.model';
import { PostService } from 'src/app/shared/services/post.service';
import { SeoService } from 'src/app/shared/services/seo.service';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.scss'],
})
export class CategoryDetailComponent implements OnInit, OnDestroy {
  slug: string = '';
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
    private seoService: SeoService
  ) {}
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.loading = true;
    this.subscription.add(
      this.activatedRoute.params.subscribe((params: Params) => {
        this.slug = params['slug'];
        this.seoService.setPageTitle(this.slug);
        this.filterPostList();
      })
    );
  }

  filterPostList(pageIndex?: number): void {
    this.filterModel.page = pageIndex ? pageIndex : this.filterModel.page;
    this.filterModel.pageSize = this.pageSize;
    const filter = { ...this.filterModel };
    this.subscription.add(
      this.postService
        .getPostsByCategorySlug(filter, this.slug)
        .subscribe((result) => {
          this.posts = result.items;
          this.total = result.totalPages;
          this.pageIndex = this.filterModel.page;
          this.loading = false;
        })
    );
  }

  onPageChange(pageIndex: number) {
    this.filterModel.page = pageIndex;
    this.filterPostList();
  }
}
