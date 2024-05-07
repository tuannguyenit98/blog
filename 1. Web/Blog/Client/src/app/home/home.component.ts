import { Component, OnInit } from '@angular/core';
import { Category } from '../shared/models/category/category.model';
import { CategoryService } from '../shared/services/category.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
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
