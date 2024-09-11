
import { ApplicationStatus, PassportType } from "./enums/enums";

export interface Address {
  addressTableId?: number;
  tHouseNo: number;
  tStreet: string;
  tDistrict?: string;
  tPoliceStation?: string;
  tState: number;  // enum
  tPincode: string;
  isPermanent: boolean;
  temporaryAddress?: string;
  houseNo?: number;
  street?: string;
  district?: string;
  policeStation?: string;
  state?: number;  // enum
  pincode?: string;
}

export interface ApplicantDetails {
  applicantDetailsTableID?: number;
  applicantFirstName: string;
  applicantLastName: string;
  applicantEmail: string;
  mobileNo: string;
  dob: Date;
  gender: number;  // enum
  placeOfBirth: string;
  district?: string;
  state?: string;
  country?: string;
  pancard?: string;
  aadharcard: string;
  voterId?: string;  // optional
  maritialStatus: number;  // enum
  citizenshipBy: number;  // enum
  education: number;  // enum
  employeeType: number;  // enum
  govermentServent: boolean;
  organizationalName?: string;
  nonECR: boolean;
  distinguishMark: string;
  nameChanged: boolean;
  changedName?: string;
  alias: boolean;
  aliasName?: string;
  passportNo?: string;
  dateOfIssue?: Date;
  placeOfIssue?: string;
}

export interface ApplicationDocument {
  documentTableId?: number;
  aadharCard: Uint8Array;
  photo: Uint8Array;
  signature: Uint8Array;
  pancard?: Uint8Array;
}

export interface EmergencyContact {
  emergencyContactDetailsId?: number;
  contactName: string;
  mobile: string;
  email: string;
  address: string;
  city: string;
  state: string;
  pincode: string;
  country: string;
}

export interface FamilyDetails {
  familyDetailsId?: number;
  fathersFirstName: string;
  fathersLastName: string;
  mothersFirstName: string;
  mothersLastName: string;
  spouceFirstName?: string;  // optional
  spouceLastName?: string;  // optional
  isMinor: boolean;
  leagalGuardianFirstName?: string;
  leagalGuardianLastName?: string;
  fatherPassportNo?: string;
  motherPassportNo?: string;
}

export interface MasterDetails {
  applicationNo?: number;
  applicationStatus: number;  // enum
  passportNo: string;
  passportType: number;  // N/R
  paymentStatus: number;  // enum
  serviceRequiredId?: number;
  userId: number;
  applicantDetailsTableID?: number;
  familyDetailsId?: number;
  addressTableId?: number;
  emergencyContactDetailsId?: number;
  otherDetailsId?: number;
  documentTableId?: number;
  paymentId?: string;
  createdOn?: Date;
  updatedOn?: Date;
}

export interface OtherDetails {
  otherDetailsId?: number;
  criminalConvictions: boolean;
  refusedPassport: boolean;
  impoundedPassport: boolean;
  revokedPassport: boolean;
  grantedCitizenship: boolean;
  heldForeignPassport: boolean;
  surrenderedIndianPassport: boolean;
  appliedRenunciation: boolean;
  passportSurrendered: boolean;
  renunciation?: boolean;
  emergencyCertificate: boolean;
  deported: boolean;
  repatriated: boolean;
  registeredMission: boolean;
  registeredMissionName?: string;
}

export interface ServiceRequired {
  serviceRequiredId?: number;
  applicationType: number;  // enum
  pagesRequired: number;  // enum
  validityReq: number;  // enum
  reasonForRenewal?: number;  // optional
  changeInAppearance?: number;  // enum
}

export interface ApplicationForm{
  applicationNo:number;
  applicantName:string;
  createdOn:Date;
  userId:number;
  passportType:PassportType;
  applicationStatus:ApplicationStatus;
}