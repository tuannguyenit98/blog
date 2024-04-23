import { Component, OnInit } from '@angular/core';
import { Category } from '../shared/models/category/category.model';
import { PostService } from '../shared/services/post.service';
import { PostFeatureModel } from '../shared/models/post/post-feature.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  postFeature: PostFeatureModel = new PostFeatureModel();
  constructor(
    private postService: PostService
  ){

  } 
  ngOnInit(): void {
    this.postService.getPostFeatures().subscribe((res) => {
      this.postFeature = res;
    })
  }

}
