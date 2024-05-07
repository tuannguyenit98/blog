import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Route, Router } from '@angular/router';
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
  constructor(
    private postSerive: PostService,
    private activatedRoute: ActivatedRoute
  ){

  }
  ngOnInit(): void {
    this.slug = this.activatedRoute.snapshot.paramMap.get('slug')!;
    this.postSerive.getBySlug(this.slug).subscribe((res) => {
      this.post = res;
    })
  }

}
