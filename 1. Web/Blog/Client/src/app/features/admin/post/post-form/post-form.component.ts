import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { AngularEditorConfig } from '@kolkov/angular-editor';
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
  postCreate: PostCreateModel = new PostCreateModel();
  post: Post = new Post();
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
  editor: any = ClassicEditor;

  editorConfig: AngularEditorConfig = {
    editable: true,
      spellcheck: true,
      height: 'auto',
      minHeight: '0',
      maxHeight: 'auto',
      width: 'auto',
      minWidth: '0',
      translate: 'yes',
      enableToolbar: true,
      showToolbar: true,
      placeholder: 'Enter text here...',
      defaultParagraphSeparator: '',
      defaultFontName: '',
      defaultFontSize: '',
      fonts: [
        {class: 'arial', name: 'Arial'},
        {class: 'times-new-roman', name: 'Times New Roman'},
        {class: 'calibri', name: 'Calibri'},
        {class: 'comic-sans-ms', name: 'Comic Sans MS'}
      ],
      customClasses: [
      {
        name: 'quote',
        class: 'quote',
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1',
      },
    ],
    uploadUrl: 'v1/image',
    uploadWithCredentials: false,
    sanitize: true,
    toolbarPosition: 'top',
    toolbarHiddenButtons: [
      ['bold', 'italic'],
      ['fontSize']
    ]
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
        this.post = this.mapping();
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
      image: [this.post.image, this.id ? null : Validators.required],
    });
  }

  validateForm(): boolean {
    this.invalidMessages = ValidationHelper.getInvalidMessages(
      this.postForm,
      this.formErrors
    );
    return this.invalidMessages.length === 0;
  }

  mapping(): any{
    return {
      title: '',
      metaTitle: '',
      category: null,
      content: '',
      image: '',
    };
  }

  mappingModel(form: any): PostCreateModel{
    return {
      title: form.title,
      metaTitle: form.metaTitle,
      fK_CategoryId: form.category.id,
      content: form.content,
      file: this.postCreate.file,
    };
  }

  onSelectFile(event: any): void {
    const fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      const file: File = fileList[0];
      this.postCreate.file = file;
    }
  }

  onSubmit(): void{
    this.postService.createOrUpdate(this.mappingModel(this.postForm.value), this.id).subscribe((res) =>
    {
      this.router.navigate([`/dashboard/post/list`]);
        this.nzNotificationService.success('Thông báo', `${this.id ? 'Cập nhật' : 'Thêm bài'} thành công!`, { nzPlacement: 'bottomRight'});
    });
  }

  compareData(o1: any, o2: any): boolean {
    return o1 && o2 ? o1.id.toString() === o2.id.toString() : o1 === o2;
  }

  removeImg(): void{
    this.post.image = '';
  }

  showPreview(event: any) {
    const file = (event.target as HTMLInputElement).files![0];
    this.postCreate.file = file;
    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.post.image = reader.result as string;
    }
    reader.readAsDataURL(file)
  }
}
