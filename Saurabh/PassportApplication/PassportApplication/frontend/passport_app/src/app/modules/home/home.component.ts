import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import Swal from 'sweetalert2';
import { ToastService } from '../../services/toast.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink,RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

  private route=inject(Router)
  private toastService=inject(ToastService)
  currentUser: User | null = null;
  isActivate: boolean = false;



  ngOnInit() {
    this.loadActiveUsers();
  }


  loadActiveUsers() {
    const currentUserJson = localStorage.getItem('currentUser');
    if (currentUserJson) {
      this.currentUser = JSON.parse(currentUserJson);
      console.log("Current logged in user", this.currentUser);
      this.isActivate = true;
    } else {
      this.currentUser = null;
      this.isActivate = false;
    }
  }
}
