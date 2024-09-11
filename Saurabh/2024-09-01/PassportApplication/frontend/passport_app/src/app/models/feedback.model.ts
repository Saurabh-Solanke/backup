import { ComplaintStatus } from "./enums/enums";
import { User } from "./user.model";

export interface FeedbackComplaint {
    id?: number;
    feedbackComplaintType: number;  // enum
    email: string;
    userName: string;
    title: string;
    description: string;
    complaintStatus: ComplaintStatus;  // enum
    userId: number;
}