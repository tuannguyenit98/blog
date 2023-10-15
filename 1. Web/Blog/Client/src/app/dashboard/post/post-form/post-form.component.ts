import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { forkJoin } from 'rxjs';
import ValidationHelper from 'src/app/shared/helpers/validation.helper';
import { Category } from 'src/app/shared/models/category/category.model';
import { PostCreateModel } from 'src/app/shared/models/post/post-create.model';
import { Post } from 'src/app/shared/models/post/post.model';
import { CategoryService } from 'src/app/shared/services/category.service';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.scss']
})
export class PostFormComponent {
  @Input() id: number | undefined;
  postForm!: FormGroup;
  post: PostCreateModel = new PostCreateModel();
  categories: Category[] = [];
  invalidMessages: string[] = [];
  formErrors = {
    category: '',
    title: '',
    metaTitle: '',
    content: '',
    image: '',
    status: '',
  };
  constructor(
    private postService: PostService,
    private formBuilder: FormBuilder,
    private router: Router,
    private nzNotificationService: NzNotificationService,
    private categoryService: CategoryService,
  ) {}

  ngOnInit(): void {
    if (this.id)
    {
      forkJoin([
        this.categoryService.getAll(),
        this.postService.getById(this.id),
      ]).subscribe(([res1, res2]) => {
        this.categories = res1;
        this.post = res2;
        this.createForm();
      });
    }
    else{
      this.categoryService.getAll().subscribe((res) => {
        this.categories = res;
        this.post = this.mappingProduct();
        this.createForm();
      });
    }
  }

  createForm(): void {
    this.postForm = this.formBuilder.group({
      category: [this.categories.find(x => x.id == this.post.fK_CategoryId), Validators.required],
      title: [this.post.title, Validators.required],
      metaTitle: [this.post.metaTitle, Validators.required],
      content: [this.post.content, Validators.required],
      image: [this.post.image, Validators.required],
      status: [this.post.status, Validators.required],
    });
  }

  validateForm(): boolean {
    this.invalidMessages = ValidationHelper.getInvalidMessages(
      this.postForm,
      this.formErrors
    );
    return this.invalidMessages.length === 0;
  }

  mappingProduct(): any{
    return {
      title: '',
      metaTitle: '',
      Post: null
    };
  }

  mappingModel(form: any): PostCreateModel{
    return {
      title: form.title,
      metaTitle: form.metaTitle,
      fK_CategoryId: form.fK_CategoryId,
      content: form.content,
      image: form.image,
      status: form.status
    };
  }

  onSubmit(): void{
    this.postService.createOrUpdate(this.mappingModel(this.postForm.value)).subscribe((res) =>
    {
      if (res)
      {
        this.router.navigate([`/dashboard/post/list`]);
        this.nzNotificationService.success('Thông báo', 'Thêm sản phẩm thành công!', { nzPlacement: 'bottomRight'});
      }
    });
  }

  compareData(o1: any, o2: any): boolean {
    return o1 && o2 ? o1.id.toString() === o2.id.toString() : o1 === o2;
  }
}
