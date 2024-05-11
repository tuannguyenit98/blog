import { Component, OnInit, ViewChild } from '@angular/core';
import { CategoryFormComponent } from '../category-form/category-form.component';
import { ActivatedRoute, Route } from '@angular/router';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent implements OnInit {
  @ViewChild('form')
  form!: CategoryFormComponent;
  id: number = 0;
  constructor(
    private route: ActivatedRoute,
  ){

  }
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
  }

}
