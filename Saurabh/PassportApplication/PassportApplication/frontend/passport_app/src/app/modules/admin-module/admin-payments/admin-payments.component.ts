import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CommonModule } from '@angular/common';

// Define the structure of a Payment DTO
interface PaymentDTO {
  paymentId: number;
  applicationId: number;
  applicationFee: number;
  applicationType: number;
  paymentDate: string;
  paymentMethod?: string;
  transactionId?: string;
  paymentStatus: string;
}

@Component({
  selector: 'app-admin-payments',
  standalone: true,
  templateUrl: './admin-payments.component.html',
  styleUrls: ['./admin-payments.component.css'],
  imports: [CommonModule]
})
export class AdminPaymentsComponent implements OnInit {
  payments: PaymentDTO[] = [];
  apiUrl = 'https://localhost:5172/api/Payments';  // Adjust URL as needed

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchPayments();
  }

  fetchPayments(): void {
    this.http.get<PaymentDTO[]>(this.apiUrl)
      .pipe(
        map(response => response),
        catchError(error => {
          console.error('Error fetching payments', error);
          return [];
        })
      )
      .subscribe(data => {
        this.payments = data;
      });
  }
}
