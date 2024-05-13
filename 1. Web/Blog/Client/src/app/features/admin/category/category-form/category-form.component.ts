import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { forkJoin } from 'rxjs';
import ValidationHelper from 'src/app/shared/helpers/validation.helper';
import { CategoryCreateModel } from 'src/app/shared/models/category/category-create.model';
import { Category } from 'src/app/shared/models/category/category.model';
import { CategoryService } from 'src/app/shared/services/category.service';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.scss']
})
export class CategoryFormComponent implements OnInit {
  @Input() id: number | undefined;
  categoryForm!: FormGroup;
  category: CategoryCreateModel = new CategoryCreateModel();
  categories: Category[] = [];
  invalidMessages: string[] = [];
  formErrors = {
    category: '',
    title: '',
    metaTitle: ''
  };
  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private router: Router,
    private nzNotificationService: NzNotificationService,
  ) {}

  ngOnInit(): void {
    if (this.id)
    {
      forkJoin([
        this.categoryService.getAll(),
        this.categoryService.getById(this.id),
      ]).subscribe(([res1, res2]) => {
        this.categories = res1;
        this.category = res2;
        this.createForm();
      });
    }
    else{
      this.categoryService.getAll().subscribe((res) => {
        this.categories = res;
        this.category = this.mappingProduct();
        this.createForm();
      });
    }
  }

  createForm(): void {
    this.categoryForm = this.formBuilder.group({
      category: [this.categories.find(x => x.id == this.category.parentId), Validators.required],
      title: [this.category.title, Validators.required],
      metaTitle: [this.category.metaTitle, Validators.required]
    });
  }

  validateForm(): boolean {
    this.invalidMessages = ValidationHelper.getInvalidMessages(
      this.categoryForm,
      this.formErrors
    );
    return this.invalidMessages.length === 0;
  }

  mappingProduct(): any{
    return {
      title: '',
      metaTitle: '',
      category: null
    };
  }

  mappingModel(form: any): CategoryCreateModel{
    return {
      title: form.title,
      metaTitle: form.metaTitle,
      parentId: form.category ? form.category.id : null
    };
  }

  onSubmit(): void{
    this.categoryService.createOrUpdate(this.mappingModel(this.categoryForm.value)).subscribe((res) =>
    {
      if (res)
      {
        this.router.navigate([`/admin/category/list`]);
        this.nzNotificationService.success('Thông báo', 'Thêm danh mục thành công!', { nzPlacement: 'bottomRight'});
      }
    });
  }

  compareData(o1: any, o2: any): boolean {
    return o1 && o2 ? o1.id.toString() === o2.id.toString() : o1 === o2;
  }
}
