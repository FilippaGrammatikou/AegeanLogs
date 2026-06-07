using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AegeanLogs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnLocode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RequiresExternalSupplier = table.Column<bool>(type: "bit", nullable: false),
                    RequiresCompletionEvidence = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ServiceCategory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vessels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientCompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ImoNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VesselType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vessels_ClientCompanies_ClientCompanyId",
                        column: x => x.ClientCompanyId,
                        principalTable: "ClientCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequirementRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortCallPurpose = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    RequirementLevel = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ReadinessImpact = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DeadlineAnchor = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DeadlineOffsetHours = table.Column<int>(type: "int", nullable: false),
                    Rationale = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequirementRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequirementRules_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    ClientCompanyId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_ClientCompanies_ClientCompanyId",
                        column: x => x.ClientCompanyId,
                        principalTable: "ClientCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortCalls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VesselId = table.Column<int>(type: "int", nullable: false),
                    PortId = table.Column<int>(type: "int", nullable: false),
                    AssignedAgentUserId = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Eta = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Etd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActualArrivalTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ActualDepartureTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ClosedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortCalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortCalls_ApplicationUsers_AssignedAgentUserId",
                        column: x => x.AssignedAgentUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortCalls_Ports_PortId",
                        column: x => x.PortId,
                        principalTable: "Ports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortCalls_Vessels_VesselId",
                        column: x => x.VesselId,
                        principalTable: "Vessels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortCallId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ActionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogEntries_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogEntries_PortCalls_PortCallId",
                        column: x => x.PortCallId,
                        principalTable: "PortCalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortCallDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortCallId = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UploadedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UploadedByUserId = table.Column<int>(type: "int", nullable: true),
                    CheckedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CheckedByUserId = table.Column<int>(type: "int", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortCallDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortCallDocuments_ApplicationUsers_CheckedByUserId",
                        column: x => x.CheckedByUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortCallDocuments_ApplicationUsers_UploadedByUserId",
                        column: x => x.UploadedByUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortCallDocuments_PortCalls_PortCallId",
                        column: x => x.PortCallId,
                        principalTable: "PortCalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortCallId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RequirementLevel = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ReadinessImpact = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Deadline = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SupplierNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    EvidenceFileName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    StartedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CompletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CheckedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CheckedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceJobs_ApplicationUsers_CheckedByUserId",
                        column: x => x.CheckedByUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceJobs_PortCalls_PortCallId",
                        column: x => x.PortCallId,
                        principalTable: "PortCalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceJobs_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceJobs_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_ClientCompanyId",
                table: "ApplicationUsers",
                column: "ClientCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_Role",
                table: "ApplicationUsers",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_SupplierId",
                table: "ApplicationUsers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogEntries_ActionType",
                table: "AuditLogEntries",
                column: "ActionType");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogEntries_EntityName_EntityId",
                table: "AuditLogEntries",
                columns: new[] { "EntityName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogEntries_PortCallId_CreatedAt",
                table: "AuditLogEntries",
                columns: new[] { "PortCallId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogEntries_UserId_CreatedAt",
                table: "AuditLogEntries",
                columns: new[] { "UserId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCompanies_IsActive",
                table: "ClientCompanies",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCompanies_Name",
                table: "ClientCompanies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortCallDocuments_CheckedByUserId",
                table: "PortCallDocuments",
                column: "CheckedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PortCallDocuments_PortCallId",
                table: "PortCallDocuments",
                column: "PortCallId");

            migrationBuilder.CreateIndex(
                name: "IX_PortCallDocuments_PortCallId_Status",
                table: "PortCallDocuments",
                columns: new[] { "PortCallId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_PortCallDocuments_Status",
                table: "PortCallDocuments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PortCallDocuments_UploadedByUserId",
                table: "PortCallDocuments",
                column: "UploadedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PortCalls_AssignedAgentUserId",
                table: "PortCalls",
                column: "AssignedAgentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PortCalls_Eta",
                table: "PortCalls",
                column: "Eta");

            migrationBuilder.CreateIndex(
                name: "IX_PortCalls_Etd",
                table: "PortCalls",
                column: "Etd");

            migrationBuilder.CreateIndex(
                name: "IX_PortCalls_PortId_Status_Eta",
                table: "PortCalls",
                columns: new[] { "PortId", "Status", "Eta" });

            migrationBuilder.CreateIndex(
                name: "IX_PortCalls_Status",
                table: "PortCalls",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PortCalls_VesselId_Eta",
                table: "PortCalls",
                columns: new[] { "VesselId", "Eta" });

            migrationBuilder.CreateIndex(
                name: "IX_Ports_IsActive",
                table: "Ports",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Ports_UnLocode",
                table: "Ports",
                column: "UnLocode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceJobs_CheckedByUserId",
                table: "ServiceJobs",
                column: "CheckedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceJobs_Deadline",
                table: "ServiceJobs",
                column: "Deadline");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceJobs_PortCallId",
                table: "ServiceJobs",
                column: "PortCallId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceJobs_PortCallId_Status",
                table: "ServiceJobs",
                columns: new[] { "PortCallId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceJobs_ReadinessImpact",
                table: "ServiceJobs",
                column: "ReadinessImpact");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceJobs_ServiceTypeId",
                table: "ServiceJobs",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceJobs_Status",
                table: "ServiceJobs",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceJobs_SupplierId",
                table: "ServiceJobs",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceJobs_SupplierId_Status_Deadline",
                table: "ServiceJobs",
                columns: new[] { "SupplierId", "Status", "Deadline" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequirementRules_IsActive",
                table: "ServiceRequirementRules",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequirementRules_PortCallPurpose_ServiceTypeId",
                table: "ServiceRequirementRules",
                columns: new[] { "PortCallPurpose", "ServiceTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequirementRules_ServiceTypeId",
                table: "ServiceRequirementRules",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_Category",
                table: "ServiceTypes",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_Code",
                table: "ServiceTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_IsActive",
                table: "ServiceTypes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_IsActive",
                table: "Suppliers",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Name",
                table: "Suppliers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ServiceCategory",
                table: "Suppliers",
                column: "ServiceCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Vessels_ClientCompanyId",
                table: "Vessels",
                column: "ClientCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vessels_ImoNumber",
                table: "Vessels",
                column: "ImoNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vessels_IsActive",
                table: "Vessels",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogEntries");

            migrationBuilder.DropTable(
                name: "PortCallDocuments");

            migrationBuilder.DropTable(
                name: "ServiceJobs");

            migrationBuilder.DropTable(
                name: "ServiceRequirementRules");

            migrationBuilder.DropTable(
                name: "PortCalls");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Ports");

            migrationBuilder.DropTable(
                name: "Vessels");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "ClientCompanies");
        }
    }
}
