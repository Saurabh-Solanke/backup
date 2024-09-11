import { MasterDetails } from "./application.model";
import { User } from "./user.model";

export interface Payment {
    paymentId?: number;
    applicationId: number;
    applicationFee: number;
    applicationType: number;  // enum
    paymentDate: Date;
    paymentMethod?: number;  // enum
    transactionId?: string;
    paymentStatus: number;  // enum
}