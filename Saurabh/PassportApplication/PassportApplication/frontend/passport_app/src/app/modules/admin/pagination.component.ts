import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';

@Component({
  selector: 'app-pagination',
  template: `
    <nav aria-label="Page navigation">
      <ul class="pagination justify-content-end">
        <li class="page-item" [class.disabled]="currentPage === 1">
          <button class="page-link" (click)="onChangePage(currentPage - 1)" aria-label="Previous">
            <span aria-hidden="true">&laquo;</span>
          </button>
        </li>
        <li
          class="page-item"
          *ngFor="let page of pages"
          [class.active]="currentPage === page"
        >
          <button class="page-link" (click)="onChangePage(page)">{{ page }}</button>
        </li>
        <li class="page-item" [class.disabled]="currentPage === totalPages">
          <button class="page-link" (click)="onChangePage(currentPage + 1)" aria-label="Next">
            <span aria-hidden="true">&raquo;</span>
          </button>
        </li>
      </ul>
    </nav>
  `,
  standalone:true,
  imports:[CommonModule]
})
export class PaginationComponent implements OnChanges {
  @Input() currentPage = 1;
  @Input() totalPages = 1;
  @Output() pageChanged = new EventEmitter<number>(); // Renamed to avoid conflict

  pages: number[] = [];

  ngOnChanges() {
    this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  onChangePage(page: number) {
    if (page >= 1 && page <= this.totalPages) {
      this.pageChanged.emit(page); // Emit the correct page number
    }
  }
}
