import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl:'./pagination.component.html',
  standalone:true,
  imports:[CommonModule]
})
export class PaginationComponent implements OnChanges {
  @Input() currentPage = 1;
  @Input() totalPages = 1;
  @Output() pageChanged = new EventEmitter<number>(); // Renamed to avoid conflict

  @Input() pages: number[] = [];

  ngOnChanges() {
    this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  onChangePage(page: number) {
    if (page >= 1 && page <= this.totalPages) {
      this.pageChanged.emit(page); // Emit the correct page number
    }
  }
}
