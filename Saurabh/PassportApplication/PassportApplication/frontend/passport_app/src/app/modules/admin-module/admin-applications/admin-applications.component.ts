import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ApplicationStatusPipe } from "../Pipes/application-status.pipe";
import { PassportTypePipe } from "../Pipes/passport-type.pipe";

interface MasterDetailsTableRespDTO {
  applicationNo: number;
  applicantName: string;
  applicationStatus: number;
  passportType: number;
  createdOn: Date;
  userId: number;
}

@Component({
  selector: 'app-master-details',
  templateUrl: 'admin-applications.component.html',
  styleUrls: ['admin-applications.component.css'],
  standalone: true,
  imports: [CommonModule, ApplicationStatusPipe, PassportTypePipe]
})
export class AdminApplicationsComponent {
  masterDetailsTables: MasterDetailsTableRespDTO[] = [];
  private apiUrl = 'http://localhost:5172/api/MasterDetailsTables';

  constructor(private http: HttpClient) {
    this.fetchMasterDetailsTables();
  }

  fetchMasterDetailsTables(): void {
    this.http.get<MasterDetailsTableRespDTO[]>(this.apiUrl)
      .subscribe(
        (data) => this.masterDetailsTables = data,
        (error) => console.error('Error fetching master details tables:', error)
      );
  }

  deleteMasterDetailsTable(applicationNo: number): void {
    const url = `${this.apiUrl}/${applicationNo}`;
    this.http.delete(url).subscribe(
      () => {
        this.masterDetailsTables = this.masterDetailsTables.filter(master => master.applicationNo !== applicationNo);
        console.log(`Deleted application with no: ${applicationNo}`);
      },
      (error) => console.error('Error deleting master details table:', error)
    );
  }
}
