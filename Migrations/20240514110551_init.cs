using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dental_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ChiefComplain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procedure = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    PatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatId",
                        column: x => x.PatId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DentalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Up_Right_Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Up_Left_Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Down_Right_Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Down_Left_Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Up_Right_IsWorkingOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Up_Left_IsWorkingOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Down_Right_IsWorkingOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Down_Left_IsWorkingOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DentalHistories_Patients_PatId",
                        column: x => x.PatId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSessions = table.Column<int>(type: "int", nullable: false),
                    DoneSessions = table.Column<int>(type: "int", nullable: false),
                    RemainSessions = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Patients_PatId",
                        column: x => x.PatId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Patients_PatId",
                        column: x => x.PatId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    DentalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_DentalHistories_DentalHistoryId",
                        column: x => x.DentalHistoryId,
                        principalTable: "DentalHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatId",
                table: "Appointments",
                column: "PatId");

            migrationBuilder.CreateIndex(
                name: "IX_DentalHistories_PatId",
                table: "DentalHistories",
                column: "PatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_DentalHistoryId",
                table: "Images",
                column: "DentalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PatId",
                table: "Invoices",
                column: "PatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PatId",
                table: "MedicalHistories",
                column: "PatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Mobile",
                table: "Patients",
                column: "Mobile",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MedicalHistories");

            migrationBuilder.DropTable(
                name: "DentalHistories");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
