import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import * as XLSX from 'xlsx'; // for excel file reading
import { PassportOffice } from '../../../interfaces/PassportOffice.interface';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-passport-office-list',
  standalone: true,
  templateUrl: './passport-office-list.component.html',
  styleUrls: ['./passport-office-list.component.css'],
  imports: [CommonModule, HttpClientModule, FormsModule],
})
export class PassportOfficeListComponent implements OnInit {
  offices: PassportOffice[] = [];
  displayedOffices: PassportOffice[] = [];
  selectedOffice: PassportOffice | null = null;
  page = 1;
  pageSize = 6; // Show 6 entries per page
  totalOffices = 0;
  states: string[] = [];
  filterState = 'all';
  uploadStatus: string | null = null;
  fetchStatus: string | null = null;
  file: File | null = null;
  formattedData: any[] = [];

  constructor(private http: HttpClient, private modalService: NgbModal) {}

  ngOnInit(): void {
    this.loadOffices();
  }

  loadOffices(): void {
    this.http
      .get<PassportOffice[]>('https://localhost:7072/api/PassportOffices')
      .subscribe(
        (data) => {
          this.offices = data;
          this.states = Array.from(new Set(data.map((office) => office.state)));
          this.totalOffices = this.offices.length;
          this.updateDisplayedOffices();
        }
        // ,
        // (error) => {
        //   this.fetchStatus =
        //     '🏖️  Servers are on vacation!. They’ll be back to work soon.';
        //   console.error(error);
        // }
      );
  }

  applyFilter(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.filterState = selectElement.value;
    this.page = 1; // Reset to first page after filter change
    this.updateDisplayedOffices();
  }

  updateDisplayedOffices(): void {
    let filteredOffices = this.offices;
    if (this.filterState !== 'all') {
      filteredOffices = this.offices.filter(
        (office) => office.state === this.filterState
      );
    }
    this.totalOffices = filteredOffices.length;
    const start = (this.page - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.displayedOffices = filteredOffices.slice(start, end);
  }
  toggleSelectAll(event: any): void {
    const checked = event.target.checked;
    this.displayedOffices.forEach((office) => (office.selected = checked));
  }

  deleteSelectedOffices(): void {
    const selectedIds = this.displayedOffices
      .filter((office) => office.selected)
      .map((office) => office.id);

    this.http
      .delete(`https://localhost:7072/api/PassportOffices/bulkDelete`, {
        body: selectedIds,
        headers: { 'Content-Type': 'application/json' },
      })
      .subscribe(() => {
        this.loadOffices(); // Reload the offices after deletion
      });
  }

  hasSelectedOffices(): boolean {
    return this.displayedOffices.some((office) => office.selected);
  }
  previousPage(): void {
    if (this.page > 1) {
      this.page--;
      this.updateDisplayedOffices();
    }
  }

  nextPage(): void {
    if (this.page * this.pageSize < this.totalOffices) {
      this.page++;
      this.updateDisplayedOffices();
    }
  }

  goToPage(pageNumber: number): void {
    if (pageNumber > 0 && pageNumber <= this.totalPages()) {
      this.page = pageNumber;
      this.updateDisplayedOffices();
    }
  }

  openModal(content: any, office: PassportOffice): void {
    this.selectedOffice = office;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  closeModal(): void {
    this.modalService.dismissAll();
  }

  openUploadModal(content: any): void {
    this.modalService.open(content, { ariaLabelledBy: 'upload-modal-title' });
  }

  onFileChange(event: any): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      this.file = input.files[0];
    }
  }

  uploadFile(): void {
    if (!this.file) {
      this.uploadStatus = 'No file selected.';
      return;
    }

    const reader = new FileReader();
    reader.onload = (e: any) => {
      const data = new Uint8Array(e.target.result);
      const workbook = XLSX.read(data, { type: 'array' });
      const sheetName = workbook.SheetNames[0];
      const worksheet = workbook.Sheets[sheetName];
      const jsonData: any[] = XLSX.utils.sheet_to_json(worksheet, {
        header: 1,
      });

      // Processing jsonData to match the PassportOffice model
      this.formattedData = jsonData.slice(1).map((row) => ({
        officeName: row[0],
        city: row[1],
        state: row[2],
        country: row[3],
        contactNumber: row[4].toString(), // Convert contactNumber to string
        email: row[5] || '', // Email is included, defaulting to an empty string if not present
      }));

      // Log formatted data to console
      console.log('Formatted Data:', this.formattedData);

      this.http
        .post(
          'https://localhost:7072/api/PassportOffices/bulk',
          this.formattedData,
          {
            headers: { 'Content-Type': 'application/json' },
          }
        )
        .subscribe(
          (res: any) => {
            this.uploadStatus = 'Upload successful!';
            this.loadOffices(); // Reload the offices to include the newly added ones
          },
          (error) => {
            this.uploadStatus = 'Upload failed. Please try again.';
            console.error(error);
          }
        );
    };
    reader.readAsArrayBuffer(this.file);
  }

  totalPages(): number {
    return Math.ceil(this.totalOffices / this.pageSize);
  }

  pageNumbers(): number[] {
    return Array.from({ length: this.totalPages() }, (_, i) => i + 1);
  }
}
