export interface ISignupForm {
    fullname: string,
    email: string,
    password: string,
    mobileNo: string
}
export interface ILoginForm {
    email: string,
    password: string
}
export interface User {
    fullName: string,
    email: string,
    password: string,
    mobileNo: number
}

export interface ILoggedInUser {
    email: string,
    mobileNo: string,
    fullName: string,
    userId: string,
    token: string,
    role: string,
    isActive: boolean
}

export interface Option {
    optionId: number;
    optionText: string;
    isCorrect: boolean;
}

export interface Question {
    questionId: number;
    sectionId: number;
    questionText: string;
    isMultipleChoice: boolean;
    options: Option[];
    mediaType: number;
    mediaUrl: string;
}


export interface Option {
    optionId: number;
    optionText: string;
    isCorrect: boolean;
}

export interface Question {
    questionId: number;
    sectionId: number;
    questionText: string;
    isMultipleChoice: boolean;
    options: Option[];
    mediaType: number;
    mediaUrl: string;
}

export interface Section {
    sectionId: number;
    sectionName: string;
}

export interface OptionDto {
    optionId: number;
    optionText: string;
    isCorrect: boolean;
}

export interface QuestionDto {
    questionId: number;
    questionText: string;
    isMultipleChoice: boolean;
    options: OptionDto[];
}


export interface UserDetailsInUserManager {
    fullname: string;
    mobileNo: string;
    isActive: boolean;
    createdOn: string; // ISO Date format
    updatedOn: string; // ISO Date format
    examsCreated: any[] | null; // Assuming it's an array, modify as per actual structure
    examResults: any[] | null; // Assuming it's an array, modify as per actual structure
    id: string; // UUID
    userName: string;
    normalizedUserName: string;
    email: string;
    normalizedEmail: string;
    emailConfirmed: boolean;
    passwordHash: string;
    securityStamp: string;
    concurrencyStamp: string;
    phoneNumber: string | null;
    phoneNumberConfirmed: boolean;
    twoFactorEnabled: boolean;
    lockoutEnd: string | null; // ISO Date format or null
    lockoutEnabled: boolean;
    accessFailedCount: number;
}