import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable, filter, map, range, toArray } from 'rxjs';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {
  @Input() pageIndex!: number; // current pageIndex
  @Input() pageSize!: number; // record per page
  @Input() total!: number; // total records


  @Output() pageChange: EventEmitter<any>;
  currentPage!: number;
  totalPages!: number;
  pages: Observable<number[]> = new Observable<number[]>();
  constructor() { 
    this.pageChange = new EventEmitter<any>()
  }

 ngOnInit() {
  this.getPages(this.pageIndex, this.pageSize, this.total);
}

ngOnChanges() {
  this.getPages(this.pageIndex, this.pageSize, this.total);
}


  getCurrentPage(pageIndex: number, pageSize: number): number {
    return Math.floor(pageIndex / pageSize) + 1;
  }

  getTotalPages(pageSize: number, total: number): number {
   return Math.ceil(Math.max(total, 1) / Math.max(pageSize, 1));
  }

  getPages(pageIndex: number, pageSize: number, total: number) {
    this.currentPage = pageIndex;
    this.totalPages = total;
    this.pages = range(1, total)
    .pipe(
      // map((offset: number) => this.currentPage + offset),
      filter((page: number) => this.isValidPageNumber(page, this.totalPages)),
      toArray()
    )
  }


  isValidPageNumber(page: number, totalPages: number): boolean {
  return page > 0 && page <= totalPages;
}

selectPage(page: number, event: any) {
  event.preventDefault();
  if (this.isValidPageNumber(page, this.totalPages)) {
    this.pageChange.emit(page);
  }
}

cancelEvent(event: any) {
  event.preventDefault();
}
}
