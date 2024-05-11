import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { CategoryFormComponent } from '../category-form/category-form.component';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.scss']
})
export class CategoryCreateComponent{
  @ViewChild('form')
  form!: CategoryFormComponent;
}
