import { Component, OnInit } from '@angular/core';
import { PostDto } from '../../models/post/post-feature.model';
import { PostService } from '../../services/post.service';

@Component({
  selector: 'app-post-featured',
  templateUrl: './post-featured.component.html',
  styleUrls: ['./post-featured.component.scss']
})
export class PostFeaturedComponent implements OnInit {
  posts: PostDto[] = [];
  constructor(
    private postService: PostService
  ){
  }
  ngOnInit(): void {
    this.postService.getPostFeatures().subscribe((res) => {
      this.posts = res;
    })
  }

}
