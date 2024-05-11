import { Component, OnInit } from '@angular/core';
import { Category } from '../../models/category/category.model';
import { CategoryService } from '../../services/category.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  categories: Category[] = [];
  searchTerm: string = '';
  constructor(
    private categorySevice: CategoryService,
    private router: Router
  ){

  } 
  ngOnInit(): void {
    this.categorySevice.getAll().subscribe(res => {
      this.categories = res;
    })
  }

  handleSearch(): void{
    if(this.searchTerm){
      this.router.navigate(['/search'], {
        queryParams: {searchTerm: this.searchTerm}
      })
    }
  }
}
