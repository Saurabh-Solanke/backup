using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassportApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressTables",
                columns: table => new
                {
                    AddressTableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    THouseNo = table.Column<int>(type: "int", nullable: false),
                    TStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TPoliceStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TState = table.Column<int>(type: "int", nullable: false),
                    TPincode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    IsPermanent = table.Column<bool>(type: "bit", nullable: false),
                    HouseNo = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliceStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Pincode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTables", x => x.AddressTableId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantDetails",
                columns: table => new
                {
                    ApplicantDetailsTableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicantLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicantEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pancard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aadharcard = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    VoterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritialStatus = table.Column<int>(type: "int", nullable: false),
                    Citizenship = table.Column<int>(type: "int", nullable: false),
                    Education = table.Column<int>(type: "int", nullable: false),
                    EmployeeType = table.Column<int>(type: "int", nullable: false),
                    GovermentServent = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NonECR = table.Column<bool>(type: "bit", nullable: false),
                    DistinguishMark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameChanged = table.Column<bool>(type: "bit", nullable: false),
                    ChangedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<bool>(type: "bit", nullable: false),
                    AliasName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantDetails", x => x.ApplicantDetailsTableID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTables",
                columns: table => new
                {
                    DocumentTableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AadharCard = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Signature = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Pancard = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RecentPassport = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTables", x => x.DocumentTableId);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContactDetails",
                columns: table => new
                {
                    EmergencyContactDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContactDetails", x => x.EmergencyContactDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "FamilyDetails",
                columns: table => new
                {
                    FamilyDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FathersFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FathersLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MothersFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MothersLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SpouceFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SpouceLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsMinor = table.Column<bool>(type: "bit", nullable: false),
                    LeagalGuardianFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LeagalGuardianLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FatherPassportNo = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    MotherPassportNo = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyDetails", x => x.FamilyDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "OtherDetails",
                columns: table => new
                {
                    OtherDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriminalConvictions = table.Column<bool>(type: "bit", nullable: false),
                    RefusedPassport = table.Column<bool>(type: "bit", nullable: false),
                    ImpoundedPassport = table.Column<bool>(type: "bit", nullable: false),
                    RevokedPassport = table.Column<bool>(type: "bit", nullable: false),
                    GrantedCitizenship = table.Column<bool>(type: "bit", nullable: false),
                    HeldForeignPassport = table.Column<bool>(type: "bit", nullable: false),
                    SurrenderedIndianPassport = table.Column<bool>(type: "bit", nullable: false),
                    AppliedRenunciation = table.Column<bool>(type: "bit", nullable: false),
                    PassportSurrendered = table.Column<bool>(type: "bit", nullable: false),
                    Renunciation = table.Column<bool>(type: "bit", nullable: false),
                    EmergencyCertificate = table.Column<bool>(type: "bit", nullable: false),
                    Deported = table.Column<bool>(type: "bit", nullable: false),
                    Repatriated = table.Column<bool>(type: "bit", nullable: false),
                    RegisteredMission = table.Column<bool>(type: "bit", nullable: false),
                    RegisteredMissionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDetails", x => x.OtherDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequireds",
                columns: table => new
                {
                    ServiceRequiredId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationType = table.Column<int>(type: "int", nullable: false),
                    PagesRequired = table.Column<int>(type: "int", nullable: false),
                    ValidityReq = table.Column<int>(type: "int", nullable: false),
                    ReasonForRenewal = table.Column<int>(type: "int", nullable: true),
                    ChangeInAppearance = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequireds", x => x.ServiceRequiredId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_AspNetUsers_PassportUserId",
                        column: x => x.PassportUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackComplaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedbackComplaintType = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintStatus = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackComplaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackComplaints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterDetailsTables",
                columns: table => new
                {
                    ApplicationNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: false),
                    PassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportType = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    ServiceRequiredId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApplicantDetailsTableID = table.Column<int>(type: "int", nullable: true),
                    FamilyDetailsId = table.Column<int>(type: "int", nullable: true),
                    AddressTableId = table.Column<int>(type: "int", nullable: true),
                    EmergencyContactDetailsId = table.Column<int>(type: "int", nullable: true),
                    OtherDetailsId = table.Column<int>(type: "int", nullable: true),
                    DocumentTableId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterDetailsTables", x => x.ApplicationNo);
                    table.ForeignKey(
                        name: "FK_MasterDetailsTables_AddressTables_AddressTableId",
                        column: x => x.AddressTableId,
                        principalTable: "AddressTables",
                        principalColumn: "AddressTableId");
                    table.ForeignKey(
                        name: "FK_MasterDetailsTables_ApplicantDetails_ApplicantDetailsTableID",
                        column: x => x.ApplicantDetailsTableID,
                        principalTable: "ApplicantDetails",
                        principalColumn: "ApplicantDetailsTableID");
                    table.ForeignKey(
                        name: "FK_MasterDetailsTables_DocumentTables_DocumentTableId",
                        column: x => x.DocumentTableId,
                        principalTable: "DocumentTables",
                        principalColumn: "DocumentTableId");
                    table.ForeignKey(
                        name: "FK_MasterDetailsTables_EmergencyContactDetails_EmergencyContactDetailsId",
                        column: x => x.EmergencyContactDetailsId,
                        principalTable: "EmergencyContactDetails",
                        principalColumn: "EmergencyContactDetailsId");
                    table.ForeignKey(
                        name: "FK_MasterDetailsTables_FamilyDetails_FamilyDetailsId",
                        column: x => x.FamilyDetailsId,
                        principalTable: "FamilyDetails",
                        principalColumn: "FamilyDetailsId");
                    table.ForeignKey(
                        name: "FK_MasterDetailsTables_OtherDetails_OtherDetailsId",
                        column: x => x.OtherDetailsId,
                        principalTable: "OtherDetails",
                        principalColumn: "OtherDetailsId");
                    table.ForeignKey(
                        name: "FK_MasterDetailsTables_ServiceRequireds_ServiceRequiredId",
                        column: x => x.ServiceRequiredId,
                        principalTable: "ServiceRequireds",
                        principalColumn: "ServiceRequiredId");
                    table.ForeignKey(
                        name: "FK_MasterDetailsTables_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NotificationMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApplicationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApplicationType = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_MasterDetailsTables_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MasterDetailsTables",
                        principalColumn: "ApplicationNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackComplaints_UserId",
                table: "FeedbackComplaints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDetailsTables_AddressTableId",
                table: "MasterDetailsTables",
                column: "AddressTableId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDetailsTables_ApplicantDetailsTableID",
                table: "MasterDetailsTables",
                column: "ApplicantDetailsTableID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDetailsTables_DocumentTableId",
                table: "MasterDetailsTables",
                column: "DocumentTableId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDetailsTables_EmergencyContactDetailsId",
                table: "MasterDetailsTables",
                column: "EmergencyContactDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDetailsTables_FamilyDetailsId",
                table: "MasterDetailsTables",
                column: "FamilyDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDetailsTables_OtherDetailsId",
                table: "MasterDetailsTables",
                column: "OtherDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDetailsTables_ServiceRequiredId",
                table: "MasterDetailsTables",
                column: "ServiceRequiredId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDetailsTables_UserId",
                table: "MasterDetailsTables",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplicationId",
                table: "Payments",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassportUserId",
                table: "Users",
                column: "PassportUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FeedbackComplaints");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MasterDetailsTables");

            migrationBuilder.DropTable(
                name: "AddressTables");

            migrationBuilder.DropTable(
                name: "ApplicantDetails");

            migrationBuilder.DropTable(
                name: "DocumentTables");

            migrationBuilder.DropTable(
                name: "EmergencyContactDetails");

            migrationBuilder.DropTable(
                name: "FamilyDetails");

            migrationBuilder.DropTable(
                name: "OtherDetails");

            migrationBuilder.DropTable(
                name: "ServiceRequireds");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
