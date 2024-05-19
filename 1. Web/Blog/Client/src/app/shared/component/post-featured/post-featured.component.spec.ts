import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostFeaturedComponent } from './post-featured.component';

describe('PostFeaturedComponent', () => {
  let component: PostFeaturedComponent;
  let fixture: ComponentFixture<PostFeaturedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PostFeaturedComponent]
    });
    fixture = TestBed.createComponent(PostFeaturedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
