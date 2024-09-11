import { Component, inject, NgModule, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { CommonModule, NgClass, NgFor } from '@angular/common';
import { FormsModule, NgModel } from '@angular/forms';
import { ToastService } from '../../services/toast.service';
import Swal from 'sweetalert2';

import { Notification } from '../../models/notification.model';
import { Payment } from '../../models/payment.model';
import { ApplicationForm, MasterDetails } from '../../models/application.model';
import { FeedbackComplaint } from '../../models/feedback.model';
import { AdminService } from '../../services/admin.service';
import { PaginationComponent } from './pagination.component';
import { ApplicationStatus, ComplaintStatus, FeedbackComplaintType, PassportType } from '../../models/enums/enums';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  standalone: true,
  imports: [NgClass, CommonModule, FormsModule,PaginationComponent],
})
export class AdminComponent {
  users: User[] = [];
  filteredUsers: User[] = [];

  applications: ApplicationForm[] = [];
  filteredApplications: ApplicationForm[] = [];

  renewalApplications: ApplicationForm[] = [];
  filteredRenewalApplications: ApplicationForm[] = [];

  complaints: FeedbackComplaint[] = [];
  filteredComplaints: FeedbackComplaint[] = [];

  feedbacks: FeedbackComplaint[] = [];
  filteredFeedbacks: FeedbackComplaint[] = [];

  payments:Payment[]=[];
  filteredPayments:Payment[]=[];

  private toastService = inject(ToastService);
  private adminService = inject(AdminService);

  activeTab = 'users';

  currentPage: number = 1;
  itemsPerPage: number = 3;
  totalPages: number = 0;
  pages: number[] = [];

  searchQuery: string = '';
  sortColumn: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  editingNewApplication: boolean = false;
  editingRenewalApplication: boolean = false;
  editingComplaint:boolean=false;
  editingFeedback:boolean=false;

  notifications:Notification[]=[];
   
  userId:number=0;

  ngOnInit(): void {
    this.loadUsersList();
    this.loadApplications();
    this.loadRenewalApplications();
    this.loadFeedbacks();
    this.loadComplaints();
    this.loadPayments();
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
    this.applyFilter();
  }

  isActiveTab(tab: string): boolean {
    return this.activeTab === tab;
  }


  loadUsersList() {
    this.adminService.getUsers().subscribe((data)=>{
      this.users=data;
      this.filteredUsers=[...this.users];
      this.preparePages();
    })
  }

  loadApplications() {
    this.adminService.getApplications().subscribe({
      next:(data:ApplicationForm[]) => {
        this.applications = data.filter(
          app => PassportType[app.passportType] == "New"
        )
      }
    });
  }

  loadRenewalApplications() {
    this.adminService.getApplications().subscribe({
      next:(data:ApplicationForm[]) => {
        this.renewalApplications = data.filter(
          app => PassportType[app.passportType] == "Renewal"
        )
      }
    });
  }

  loadComplaints() {
    this.adminService.getComplaints().subscribe({
      next:((data:FeedbackComplaint[])=>{
        this.complaints=data.filter(
          app=>FeedbackComplaintType[app.feedbackComplaintType]=="Complaint"
        )
      })
    });
  }

  loadFeedbacks() {
    this.adminService.getComplaints().subscribe({
      next:((data:FeedbackComplaint[])=>{
        this.feedbacks=data.filter(
          app=>FeedbackComplaintType[app.feedbackComplaintType]=="Feedback"
        )
      })
    });
  }

  loadPayments(){
    this.adminService.getPayments().subscribe((data) => {
      this.payments = data;
      this.filteredPayments = [...this.payments];
      this.preparePages();
    });
  }
  applyFilter() {
    const query = this.searchQuery.toLowerCase();
    this.filterData(query);
    this.sortData();
    this.currentPage = 1;
    this.preparePages();
  }

  filterData(query: string) {
    if (this.activeTab === 'users') {
      this.filteredUsers = this.users.filter(
        (user) =>
          user.firstname.toLowerCase().includes(query) ||
          user.lastname.toLowerCase().includes(query) ||
          user.email.toLowerCase().includes(query)
      );
    } else if (this.activeTab === 'applications') {
      this.filteredApplications = this.applications.filter(
        (application) =>
          application.applicantName
            ?.toLowerCase()
            .includes(query) ||
          application.passportType.toString().toLowerCase().includes(query) ||
          application.applicationStatus.toString().toLowerCase().includes(query)
      );
    } else if (this.activeTab === 'renewal') {
      this.filteredRenewalApplications = this.renewalApplications.filter(
        (renewalApplication) =>
          renewalApplication.applicantName
            ?.toLowerCase()
            .includes(query) ||
          renewalApplication.passportType.toString().toLowerCase().includes(query) ||
          renewalApplication.applicationStatus.toString().toLowerCase().includes(query)
      );
    } else if (this.activeTab === 'complaint') {
      this.filteredComplaints = this.complaints.filter((complaint) =>
        complaint.description.toLowerCase().includes(query) ||
        complaint.title.toLowerCase().includes(query)
      );
    } else if (this.activeTab === 'feedback') {
      this.filteredFeedbacks = this.feedbacks.filter((feedback) =>
        feedback.title.toLowerCase().includes(query) ||
        feedback.description.toLowerCase().includes(query)
      );
    }
  }

  preparePages() {
    let dataLength = 0;

    if (this.activeTab === 'users') {
      dataLength = this.filteredUsers.length;
    } else if (this.activeTab === 'applications') {
      dataLength = this.filteredApplications.length;
    } else if (this.activeTab === 'renewal') {
      dataLength = this.filteredRenewalApplications.length;
    } else if (this.activeTab === 'complaint') {
      dataLength = this.filteredComplaints.length;
    } else if (this.activeTab === 'feedback') {
      dataLength = this.filteredFeedbacks.length;
    }

    this.totalPages = Math.ceil(dataLength / 3); //3 items per page
    this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  changePage(page: number) {
    if (page < 1 || page > this.totalPages) {
      return;
    }
    this.currentPage = page;
  }

  onSearchChange(query: string) {
    this.searchQuery = query;
    this.applyFilter();
  }

  getPageData<T>(data:T[]):T[]{
    const start =(this.currentPage - 1)*this.itemsPerPage;
    const end=this.currentPage*this.itemsPerPage;
    return data.slice(start,end);
  }
 
  sortData() {
     const sortBy = (a: any, b: any, column: string) => {
      const valueA = a[column];
      const valueB = b[column];
      return (valueA < valueB ? -1 : 1) * (this.sortDirection === 'asc' ? 1 : -1);
    };

    if (this.activeTab === 'users') {
      this.filteredUsers.sort((a, b) => sortBy(a, b, this.sortColumn));
    } else if (this.activeTab === 'applications') {
      this.filteredApplications.sort((a, b) => sortBy(a, b, this.sortColumn));
    } else if (this.activeTab === 'renewal') {
      this.filteredRenewalApplications.sort((a, b) => sortBy(a, b, this.sortColumn));
    } else if (this.activeTab === 'complaint') {
      this.filteredComplaints.sort((a, b) => sortBy(a, b, this.sortColumn));
    } else if (this.activeTab === 'feedback') {
      this.filteredFeedbacks.sort((a, b) => sortBy(a, b, this.sortColumn));
    }
  }

  onSortColumn(column: string) {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    this.sortData();
  }

  //edit idhar hai
  onEditApplication(application: ApplicationForm) {
    this.editingNewApplication = true;
  }

  onEditRenewalApplication(renewalApplication: ApplicationForm) {
    this.editingRenewalApplication = true;
  }

  onEditComplaint(complaint: FeedbackComplaint) {
    this.editingComplaint = true;
}

  onEditFeedback(feedback: FeedbackComplaint) {
    this.editingFeedback = true;
}

//save yaha hai
onSaveApplication(application: ApplicationForm) {
  this.adminService.updateApplication(application).subscribe( {
    next:(data:number)=>{
      this.userId=data
        console.log(application)
        this.toastService.showSuccess('Application updated successfully');
        this.loadApplications();
        this.editingNewApplication = false;
    }
  });

  const notification:Notification={
    userId:application.userId,
    notificationMessage: `Your application with application Number ${application.applicationNo}'s Status is changed to ${application.applicationStatus}`,
    isRead: false
  }
}

onSaveRenewalApplication(renewalApplication: ApplicationForm) {
  this.adminService.updateRenewalApplication(renewalApplication).subscribe(() => {
    this.toastService.showSuccess('Renewal application updated successfully');
    this.loadRenewalApplications();
    this.editingRenewalApplication = false;
  });

  const notification:Notification={
    userId:renewalApplication.userId,
    notificationMessage: `Your application with application Number ${renewalApplication.applicationNo}'s Status is changed to ${renewalApplication.applicationStatus}`,
    isRead: false
  }
}

onSaveComplaint(complaint: FeedbackComplaint) {
  this.adminService.updateComplaint(complaint).subscribe(() => {
    this.toastService.showSuccess('Complaint updated successfully');
    this.loadComplaints();
    this.editingComplaint = false;
  });

  const notification:Notification={
    userId:complaint.userId,
    notificationMessage: `Your compliant got ${complaint.feedbackComplaintType}`,
    isRead: false
  }
}

onSaveFeedback(feedback: FeedbackComplaint) {
  this.adminService.updateFeedback(feedback).subscribe(() => {
    this.toastService.showSuccess('Feedback updated successfully');
    this.loadFeedbacks();
    this.editingFeedback = false;
  });
}
  //deletes idhar hai
  onDeleteUser(userId: number | undefined) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      background: '#050505',
      color: '#2ce20c',
      showCancelButton: true,
      confirmButtonColor: '#076dcc',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        const id =userId ?? 0;
        this.adminService.deleteUser(id).subscribe(() => {
          this.toastService.showSuccess('User deleted successfully');
          this.loadUsersList();
        });
      }
    });
  }

  onDeleteApplication(applicationNo: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      background: '#050505',
      color: '#2ce20c',
      showCancelButton: true,
      confirmButtonColor: '#076dcc',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.adminService.deleteApplication(applicationNo).subscribe(() => {
          this.toastService.showSuccess('Application deleted successfully');
          this.loadApplications();
        });
      }
    });
  }

  onDeleteRenewalApplication(applicationNo: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      background: '#050505',
      color: '#2ce20c',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.adminService.deleteRenewalApplication(applicationNo).subscribe(() => {
          this.toastService.showSuccess('Renewal application deleted successfully');
          this.loadRenewalApplications();
        });
      }
    });
  }

  onDeleteComplaint(complaintId: number | undefined) {
    Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      background: "#050505",
      color: "#2ce20c",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
    }).then((result) => {
      if (result.isConfirmed) {
        const id =complaintId ?? 0
        this.adminService.deleteComplaint(id).subscribe(() => {
          this.toastService.showSuccess('Complaint deleted successfully');
          this.loadComplaints();
        });
      }
    });
  }

  onDeleteFeedback(feedbackId: number | undefined) {
    Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      background: "#050505",
      color: "#2ce20c",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
    }).then((result) => {
      if (result.isConfirmed) {
        const id =feedbackId ?? 0
        this.adminService.deleteFeedback(id).subscribe(() => {
          this.toastService.showSuccess('Feedback deleted successfully');
          this.loadFeedbacks();
        });
      }
    });
  }

loadNotifications() {
  // Retrieve the item from localStorage
  const notificationsData = localStorage.getItem('notifications');

  if (notificationsData) {
    // Parse the JSON string back to an array if it exists
    this.notifications = JSON.parse(notificationsData);
  } else {
    // Log the absence of data
    console.log('No notifications found in localStorage. Initializing empty array.');

    // Initialize notifications with an empty array
    this.notifications = [];

    // Save the empty array back to localStorage
    localStorage.setItem('notifications', JSON.stringify(this.notifications));
  }
}

trackById(index: number, item: any): number {
  return item.id || item.applicationNo || item.paymentId || item.complaintId || item.userId; // Ensure to match with your actual unique identifiers
}

getPassportType(passportType: number): string {
  return PassportType[passportType];
}

getStatusName(applicationStatus: number): string {
  return ApplicationStatus[applicationStatus];
}
getComplaintStatus(complaintStatus: number): string {
  return ComplaintStatus[complaintStatus];
}
}
 