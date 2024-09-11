-- Users Table with DateOfBirth and CompanyName
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    UserFullname NVARCHAR(100) NOT NULL,
    UserEmail NVARCHAR(100) NOT NULL,
    UserPassword NVARCHAR(255) NOT NULL,
    UserMobileNo NVARCHAR(10) NOT NULL,
    UserAddress NVARCHAR(100) NOT NULL,
    UserPincode NVARCHAR(6) NOT NULL,
    IsActive BIT NOT NULL,
    DateOfBirth DATE, 
    CompanyName NVARCHAR(100),  
    CreatedOn DATETIME NOT NULL,
    UpdatedOn DATETIME NOT NULL
);
GO


-- Subjects Table
CREATE TABLE Subjects (
    SubjectId INT PRIMARY KEY IDENTITY,
    SubjectName NVARCHAR(255) NOT NULL
);
GO

-- Tests Table
CREATE TABLE Tests (
    TestId INT PRIMARY KEY IDENTITY,
    SubjectName NVARCHAR(255),
    ConductedOn DATETIME NOT NULL,
    UserId INT NOT NULL,
    SubjectId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId)
);
GO

-- Results Table
CREATE TABLE Results (
    ResultId INT PRIMARY KEY IDENTITY,
    TotalQuestions INT NOT NULL,
    AttemptedQuestions INT NOT NULL,
    UnAttemptedQuestions INT NOT NULL,
    CorrectQuestions INT NOT NULL,
    InCorrectQuestions INT NOT NULL,
    PercentageObtained DECIMAL(5, 2) NOT NULL,
    CreatedOn DATETIME NOT NULL,
    UserId INT NOT NULL,
    TestId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (TestId) REFERENCES Tests(TestId)
);
GO

-- Questions Table
CREATE TABLE Questions (
    QuestionId INT PRIMARY KEY IDENTITY,
    QuestionText NVARCHAR(500) NOT NULL,
    SubjectId INT NOT NULL,
    FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId)
);
GO

-- Options Table
CREATE TABLE Options (
    OptionId INT PRIMARY KEY IDENTITY,
    OptionText NVARCHAR(500) NOT NULL,
    IsCorrect BIT NOT NULL,
    UpdatedOn DATETIME NOT NULL,
    QuestionId INT NOT NULL,
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId)
);
GO

-- AuditLog Table
CREATE TABLE AuditLogs (
    AuditLogId INT PRIMARY KEY IDENTITY,
    EntityName NVARCHAR(50) NOT NULL,
    ChangeType NVARCHAR(255) NOT NULL,
    ChangedBy NVARCHAR(100) NOT NULL,
    ChangeDate DATETIME NOT NULL DEFAULT GETDATE(),
    OriginalValue NVARCHAR(400),
    ModifiedValue NVARCHAR(400),
    Description NVARCHAR(400)
);
GO

-- Sections Table
CREATE TABLE Sections (
    SectionId INT PRIMARY KEY IDENTITY,
    SectionName NVARCHAR(100) NOT NULL,
    TestId INT NOT NULL,
    FOREIGN KEY (TestId) REFERENCES Tests(TestId)
);
GO

-- TestQuestions Table (Tracking questions used in each test)
CREATE TABLE TestQuestions (
    TestQuestionId INT PRIMARY KEY IDENTITY,
    TestId INT NOT NULL,
    SectionId INT NOT NULL,
    QuestionId INT NOT NULL,
    IsAnswered BIT NOT NULL DEFAULT 0,
    IsMarkedForReview BIT NOT NULL DEFAULT 0,
    IsCurrent BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (TestId) REFERENCES Tests(TestId),
    FOREIGN KEY (SectionId) REFERENCES Sections(SectionId),
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId)
);
GO

-- PassingThreshold Table (For admin-configured thresholds per section)
CREATE TABLE PassingThresholds (
    ThresholdId INT PRIMARY KEY IDENTITY,
    SectionId INT NOT NULL,
    ThresholdPercentage DECIMAL(5, 2) NOT NULL,
    FOREIGN KEY (SectionId) REFERENCES Sections(SectionId)
);
GO

-- QuestionBank Table (Managing reusable questions)
CREATE TABLE QuestionBank (
    QuestionBankId INT PRIMARY KEY IDENTITY,
    QuestionId INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedOn DATETIME NOT NULL,
    UpdatedOn DATETIME NOT NULL,
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId)
);
GO

-- TestHistory Table (Tracking user's test history)
CREATE TABLE TestHistory (
    TestHistoryId INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
    TestId INT NOT NULL,
    ConductedOn DATETIME NOT NULL,
    FinalScore DECIMAL(5, 2) NOT NULL,
    Passed BIT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (TestId) REFERENCES Tests(TestId)
);
GO

-- SectionQuestions Table (Mapping questions to sections)
CREATE TABLE SectionQuestions (
    SectionQuestionId INT PRIMARY KEY IDENTITY,
    SectionId INT NOT NULL,
    QuestionId INT NOT NULL,
    FOREIGN KEY (SectionId) REFERENCES Sections(SectionId),
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId)
);
GO

-- UserTestAnswers Table (Storing user answers)
CREATE TABLE UserTestAnswers (
    UserTestAnswerId INT PRIMARY KEY IDENTITY,
    TestId INT NOT NULL,
    QuestionId INT NOT NULL,
    OptionId INT NOT NULL,
    IsSelected BIT NOT NULL,
    FOREIGN KEY (TestId) REFERENCES Tests(TestId),
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId),
    FOREIGN KEY (OptionId) REFERENCES Options(OptionId)
);
GO

-- CustomQuestions Table (For admin to add custom questions)
CREATE TABLE CustomQuestions (
    CustomQuestionId INT PRIMARY KEY IDENTITY,
    QuestionText NVARCHAR(500) NOT NULL,
    CreatedBy NVARCHAR(100) NOT NULL,
    CreatedOn DATETIME NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- Testimonials Table (For landing page feedback)
CREATE TABLE Testimonials (
    TestimonialId INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(100) NOT NULL,
    Feedback NVARCHAR(1000) NOT NULL,
    CreatedOn DATETIME NOT NULL
);
GO

-- CertificatesIssued Table (Tracking certificates issued with unique ID)
CREATE TABLE CertificatesIssued (
    CertificateId INT PRIMARY KEY IDENTITY,
    CertificateUniqueId NVARCHAR(50) NOT NULL UNIQUE,  -- Custom unique identifier
    UserId INT NOT NULL,
    TestId INT NOT NULL,
    IssuedOn DATETIME NOT NULL,
    CertificatePath NVARCHAR(255) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (TestId) REFERENCES Tests(TestId)
);
GO
