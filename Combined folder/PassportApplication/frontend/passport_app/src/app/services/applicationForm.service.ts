import { Injectable, signal } from '@angular/core';
import { Address, ApplicantDetails, ApplicationDocument, EmergencyContact, FamilyDetails, MasterDetails, OtherDetails, ServiceRequired } from '../models/application.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApplicationFormStatus } from '../models/applicationstatus.model';

@Injectable({
  providedIn: 'root',
})
export class ApplicationFormService {

  private baseUrl = 'http://localhost:5172/api';

  constructor(private httpClient:HttpClient) {
  }


  // CRUD Operations for MasterDetails

  createApplication(application: MasterDetails): Observable<MasterDetails> {
    return this.httpClient.post<MasterDetails>(`${this.baseUrl}/MasterDetailsTables`, application);
  }

  getAllApplications(): Observable<MasterDetails[]> {
    return this.httpClient.get<MasterDetails[]>(`${this.baseUrl}/MasterDetailsTables`);
  }

  getApplicationById(id: number): Observable<MasterDetails> {
    return this.httpClient.get<MasterDetails>(`${this.baseUrl}/MasterDetailsTables/${id}`);
  }

  updateApplication(id: number, application: MasterDetails): Observable<void> {
    return this.httpClient.put<void>(`${this.baseUrl}/MasterDetailsTables/${id}`, application);
  }

  deleteApplication(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/MasterDetailsTables/${id}`);
  }

  getApplicationStatusByUserId(id:number):Observable<ApplicationFormStatus[]>{
    return this.httpClient.get<ApplicationFormStatus[]>(`${this.baseUrl}/MasterDetailsTables/application-status/${id}`);
  }
  // CRUD Operations for Address

  createAddress(address: Address): Observable<Address> {
    return this.httpClient.post<Address>(`${this.baseUrl}/AddressTables`, address);
  }

  getAllAddresses(): Observable<Address[]> {
    return this.httpClient.get<Address[]>(`${this.baseUrl}/AddressTables`);
  }

  getAddressById(id: number): Observable<Address> {
    return this.httpClient.get<Address>(`${this.baseUrl}/AddressTables/${id}`);
  }

  updateAddress(id: number, address: Address): Observable<void> {
    return this.httpClient.put<void>(`${this.baseUrl}/AddressTables/${id}`, address);
  }

  deleteAddress(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/AddressTables/${id}`);
  }

  // CRUD Operations for ApplicantDetails

  createApplicantDetails(details: ApplicantDetails): Observable<ApplicantDetails> {
    return this.httpClient.post<ApplicantDetails>(`${this.baseUrl}/applicantdetails`, details);
  }

  getAllApplicantDetails(): Observable<ApplicantDetails[]> {
    return this.httpClient.get<ApplicantDetails[]>(`${this.baseUrl}/applicantdetails`);
  }

  getApplicantDetailsById(id: number): Observable<ApplicantDetails> {
    return this.httpClient.get<ApplicantDetails>(`${this.baseUrl}/applicantdetails/${id}`);
  }

  updateApplicantDetails(id: number, details: ApplicantDetails): Observable<void> {
    return this.httpClient.put<void>(`${this.baseUrl}/applicantdetails/${id}`, details);
  }

  deleteApplicantDetails(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/applicantdetails/${id}`);
  }

  // CRUD Operations for Document

  createNewDocument(document: FormData): Observable<ApplicationDocument> {
    return this.httpClient.post<ApplicationDocument>(`${this.baseUrl}/DocumentTables/save-new-form`, document);
  }

  createRenewalDocument(document: FormData): Observable<ApplicationDocument> {
    return this.httpClient.post<ApplicationDocument>(`${this.baseUrl}/DocumentTables/save-renewal-form`, document);
  }

  getAllDocuments(): Observable<ApplicationDocument[]> {
    return this.httpClient.get<ApplicationDocument[]>(`${this.baseUrl}/DocumentTables`);
  }

  getDocumentById(id: number): Observable<ApplicationDocument> {
    return this.httpClient.get<ApplicationDocument>(`${this.baseUrl}/DocumentTables/${id}`);
  }

  updateDocument(id: number, document: ApplicationDocument): Observable<void> {
    return this.httpClient.put<void>(`${this.baseUrl}/DocumentTables/${id}`, document);
  }

  deleteDocument(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/documents/${id}`);
  }

  // CRUD Operations for EmergencyContact

  createEmergencyContact(contact: EmergencyContact): Observable<EmergencyContact> {
    return this.httpClient.post<EmergencyContact>(`${this.baseUrl}/EmergencyContactDetails`, contact);
  }

  getAllEmergencyContacts(): Observable<EmergencyContact[]> {
    return this.httpClient.get<EmergencyContact[]>(`${this.baseUrl}/EmergencyContactDetails`);
  }

  getEmergencyContactById(id: number): Observable<EmergencyContact> {
    return this.httpClient.get<EmergencyContact>(`${this.baseUrl}/EmergencyContactDetails/${id}`);
  }

  updateEmergencyContact(id: number, contact: EmergencyContact): Observable<void> {
    return this.httpClient.put<void>(`${this.baseUrl}/EmergencyContactDetails/${id}`, contact);
  }

  deleteEmergencyContact(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/EmergencyContactDetails/${id}`);
  }

  // CRUD Operations for FamilyDetails

  createFamilyDetails(details: FamilyDetails): Observable<FamilyDetails> {
    return this.httpClient.post<FamilyDetails>(`${this.baseUrl}/familydetails`, details);
  }

  getAllFamilyDetails(): Observable<FamilyDetails[]> {
    return this.httpClient.get<FamilyDetails[]>(`${this.baseUrl}/familydetails`);
  }

  getFamilyDetailsById(id: number): Observable<FamilyDetails> {
    return this.httpClient.get<FamilyDetails>(`${this.baseUrl}/familydetails/${id}`);
  }

  updateFamilyDetails(id: number, details: FamilyDetails): Observable<void> {
    return this.httpClient.put<void>(`${this.baseUrl}/familydetails/${id}`, details);
  }

  deleteFamilyDetails(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/familydetails/${id}`);
  }

  // CRUD Operations for OtherDetails

  createOtherDetails(details: OtherDetails): Observable<OtherDetails> {
    return this.httpClient.post<OtherDetails>(`${this.baseUrl}/otherdetails`, details);
  }

  getAllOtherDetails(): Observable<OtherDetails[]> {
    return this.httpClient.get<OtherDetails[]>(`${this.baseUrl}/otherdetails`);
  }

  getOtherDetailsById(id: number): Observable<OtherDetails> {
    return this.httpClient.get<OtherDetails>(`${this.baseUrl}/otherdetails/${id}`);
  }

  updateOtherDetails(id: number, details: OtherDetails): Observable<void> {
    return this.httpClient.put<void>(`${this.baseUrl}/otherdetails/${id}`, details);
  }

  deleteOtherDetails(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/otherdetails/${id}`);
  }

  // CRUD Operations for ServiceRequired

  createServiceRequired(service: ServiceRequired): Observable<ServiceRequired> {
    return this.httpClient.post<ServiceRequired>(`${this.baseUrl}/ServiceRequireds`, service);
  }

  getAllServiceRequired(): Observable<ServiceRequired[]> {
    return this.httpClient.get<ServiceRequired[]>(`${this.baseUrl}/ServiceRequireds`);
  }

  getServiceRequiredById(id: number): Observable<ServiceRequired> {
    return this.httpClient.get<ServiceRequired>(`${this.baseUrl}/ServiceRequireds/${id}`);
  }

  updateServiceRequired(id: number, service: ServiceRequired): Observable<void> {
    return this.httpClient.put<void>(`${this.baseUrl}/ServiceRequireds/${id}`, service);
  }

  deleteServiceRequired(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/servicerequireds/${id}`);
  }
}
