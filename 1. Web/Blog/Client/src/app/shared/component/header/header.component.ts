import { Component, OnInit } from '@angular/core';
import { Category } from '../../models/category/category.model';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  categories: Category[] = [];
  constructor(
    private categorySevice: CategoryService
  ){

  } 
  ngOnInit(): void {
    this.categorySevice.getAll().subscribe(res => {
      this.categories = res;
    })
  }
}
