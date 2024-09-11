import { User } from "./user.model";

export interface Notification {
  notificationID?: number | undefined;
  userId: number;
  notificationMessage: string;
  createdOn?: Date;
  isRead: boolean;
}