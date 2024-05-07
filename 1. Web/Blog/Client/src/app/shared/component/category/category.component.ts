import { Component, OnInit } from '@angular/core';
import { Category } from '../../models/category/category.model';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  categories: Category[] = [];
  constructor(
    private categoryService: CategoryService
  ){

  } 
  ngOnInit(): void {
    this.categoryService.getAll().subscribe((res) => {
      this.categories = res;
    })
  }
}
