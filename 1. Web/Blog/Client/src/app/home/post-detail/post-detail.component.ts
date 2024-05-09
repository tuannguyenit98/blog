import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Params, Route, Router } from '@angular/router';
import { PostDto } from 'src/app/shared/models/post/post-feature.model';
import { Post } from 'src/app/shared/models/post/post.model';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss']
})
export class PostDetailComponent implements OnInit {
  slug: string | undefined;
  post: Post = new Post();
  loading: boolean = false;
  
  constructor(
    private postSerive: PostService,
    private activatedRoute: ActivatedRoute
  ){

  }
  ngOnInit(): void {
    this.loading = true;
    this.activatedRoute.params.subscribe((params: Params) => {
      this.slug = params['slug'];
      this.postSerive.getBySlug(this.slug!).subscribe((res) => {
        this.post = res;
        this.loading = false;
      })
    })
  }

}
