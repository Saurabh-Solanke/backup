import { Injectable } from "@angular/core";
import { User } from "../models/user.model";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'http://localhost:5172/api';

  constructor(private http:HttpClient) {
  }

  // Create a new user
  addUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.baseUrl}/Users`, user);
  }

  // Get a list of all users
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/Users`);
  }

  // Get a specific user by ID
  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/Users/${id}`);
  }

  // Update a user by ID
  updateUser(id: number | undefined, user: User): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/Users/${id}`, user);
  }

  // Delete a user by ID
  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Users/${id}`);
  }

  getNotificationsForUser(userId: number): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${this.baseUrl}/Users/${userId}`);
  }

  sendNotification(notification:Notification):Observable<Notification>{
    return this.http.post<Notification>(`${this.baseUrl}/Notifications`,notification);
  }
  markAsRead(notificationId: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/${notificationId}/mark-as-read`, {});
  }
}
