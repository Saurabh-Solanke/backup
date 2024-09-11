export interface ISignupForm{
  userFullname    : string,
  userEmail       : string,
  userPassword    : string,
  userMobileNo    : string
  userAddress     : string,
  userPincode     : string,
  
  }
  export interface ILoginForm{
    userEmail     : string,
    password      : string
  }
  export interface User{
    name:string,
    email: string,
    password: string,
    address: string,
    pincode: string,
  }

  export interface ILoggedInUser
  {
    email         : string,
    phoneNo       : string,
    userFullname  : string,
    userId        : number,
    token         : string,
    role          : string

  }