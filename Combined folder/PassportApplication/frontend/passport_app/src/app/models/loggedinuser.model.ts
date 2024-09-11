export interface LoggedInUser {
  userId:number;
  token: string;
  expiration: Date;
  role: string;
  firstName: string;
  lastName: string;
  email: string;
  mobileNo:number;
  }
  