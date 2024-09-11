import { Component, OnInit } from '@angular/core';
import { Notification } from '../../../models/notification.model';
import { User } from '../../../models/user.model';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css'
})
export class NotificationComponent implements OnInit{

   currentUser: User | null = null;
  userNotifications:Notification[]=[];

  ngOnInit(): void {
    this.loadActiveUser();
      this.loadNotificationsByUserId();
  }
  
  loadNotificationsByUserId(){
    const notificationsData=localStorage.getItem('notifications');

    if(notificationsData){

      const notifications: Notification[] = JSON.parse(notificationsData);
      this.userNotifications= notifications.filter((notification: Notification) => notification.userId === this.currentUser?.userId);
    }
  }

  loadActiveUser() {
    const currentUserJson = localStorage.getItem('currentUser');
    if (currentUserJson) {
      this.currentUser = JSON.parse(currentUserJson);
      console.log("Current logged in user", this.currentUser);
      
    } else {
      this.currentUser = null;
    }
  }

  markAsRead(notificationId: number | undefined): void {
    this.userNotifications = this.userNotifications.map(notification =>
      notification.notificationID === notificationId ? { ...notification, read: true } : notification
    );
    this.loadNotificationsByUserId()
  }

  markAllAsRead(): void {
    this.userNotifications = this.userNotifications.map(notification => ({ ...notification, read: true }));
    this.loadNotificationsByUserId()
  }
}
