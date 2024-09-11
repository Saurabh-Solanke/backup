export interface User {
    userId?: number;
    firstname: string;
    lastname: string;
    email: string;
    password: string;
    mobileNo: string;
    passportUserId: string;
    createdOn?: Date;
    updatedOn?: Date;
    role: string;
}