import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Params, Route, Router } from '@angular/router';
import { CreateOrUpdateModel } from 'src/app/shared/models/comment/comment-create-or-update.model';
import { PostDto } from 'src/app/shared/models/post/post-feature.model';
import { Post } from 'src/app/shared/models/post/post.model';
import { CommentService } from 'src/app/shared/services/comment.service';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss']
})
export class PostDetailComponent implements OnInit, AfterViewInit {
  slug: string | undefined;
  post: Post = new Post();
  loading: boolean = false;
  commentForm!: FormGroup;
  id: number = 0;
  index = -1;
  @ViewChild('container') container: ElementRef<HTMLElement> | undefined;
  href: string = '';
  
  constructor(
    private postSerive: PostService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private commentService: CommentService,
    private router: Router
  ){

  }
  ngAfterViewInit(): void {
    this.activatedRoute.params.subscribe(param => {
      if(param['pageSec']){
        const section = this.container!.nativeElement.querySelector(`#${param['pageSec']}`);
        section?.scrollIntoView()
      }
    })
  }
  ngOnInit(): void {
    this.loading = true;
    this.activatedRoute.params.subscribe((params: Params) => {
      this.href = this.router.url;
      this.slug = params['slug'];
      this.postSerive.getBySlug(this.slug!).subscribe((res) => {
        this.post = res;
        this.loading = false;
        this.createForm();
      })
    })
  }

  createForm(): void {
    this.commentForm = this.formBuilder.group({
      userName: [null, Validators.required],
      content: [null, Validators.required],
      parseInt: [null]
    });
  }

  mappingModel(form: any): CreateOrUpdateModel{
    return {
      userName: form.userName,
      fK_PostId: this.post.id,
      content: form.content,
      parentId: form.parentId
    };
  }

  onSubmit(parentId?: number): void{
    this.commentService.createOrUpdate(this.mappingModel({ ...this.commentForm.value, parentId }), this.id).subscribe((res) => {
      this.postSerive.getBySlug(this.slug!).subscribe((postRes) => {
      this.post = postRes;
      this.commentForm.reset();
      });
    });
  }

  setIndex(index: number): void{
    this.index = this.index == index ? -1 : index;
  }
}
