using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMGT.Migrations
{
    public partial class GymDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodGroups",
                columns: table => new
                {
                    BloodGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroups", x => x.BloodGroupID);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salaire = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingLevels",
                columns: table => new
                {
                    TrainingLevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLevels", x => x.TrainingLevelID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GymTrainee_result",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: false),
                    ContactNo = table.Column<string>(type: "varchar(50)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<string>(type: "varchar(100)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    BloodGroupID = table.Column<int>(type: "int", nullable: false),
                    TrainingLevelID = table.Column<int>(type: "int", nullable: false),
                    MonthlyFee = table.Column<int>(type: "int", nullable: false),
                    Feepaid_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymTrainee_result", x => x.TraineeId);
                    table.ForeignKey(
                        name: "FK_GymTrainee_result_BloodGroups_BloodGroupID",
                        column: x => x.BloodGroupID,
                        principalTable: "BloodGroups",
                        principalColumn: "BloodGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GymTrainee_result_TrainingLevels_TrainingLevelID",
                        column: x => x.TrainingLevelID,
                        principalTable: "TrainingLevels",
                        principalColumn: "TrainingLevelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: false),
                    ContactNo = table.Column<string>(type: "varchar(50)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<string>(type: "varchar(100)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloodGroupID = table.Column<int>(type: "int", nullable: false),
                    TrainingLevelID = table.Column<int>(type: "int", nullable: false),
                    MonthlyFee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.TraineeId);
                    table.ForeignKey(
                        name: "FK_Trainees_BloodGroups_BloodGroupID",
                        column: x => x.BloodGroupID,
                        principalTable: "BloodGroups",
                        principalColumn: "BloodGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainees_TrainingLevels_TrainingLevelID",
                        column: x => x.TrainingLevelID,
                        principalTable: "TrainingLevels",
                        principalColumn: "TrainingLevelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyFeeVouchers",
                columns: table => new
                {
                    MonthlyFeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyFeeVouchers", x => x.MonthlyFeeID);
                    table.ForeignKey(
                        name: "FK_MonthlyFeeVouchers_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "TraineeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymTrainee_result_BloodGroupID",
                table: "GymTrainee_result",
                column: "BloodGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_GymTrainee_result_TrainingLevelID",
                table: "GymTrainee_result",
                column: "TrainingLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyFeeVouchers_TraineeId",
                table: "MonthlyFeeVouchers",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_BloodGroupID",
                table: "Trainees",
                column: "BloodGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_TrainingLevelID",
                table: "Trainees",
                column: "TrainingLevelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymTrainee_result");

            migrationBuilder.DropTable(
                name: "MonthlyFeeVouchers");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "BloodGroups");

            migrationBuilder.DropTable(
                name: "TrainingLevels");
        }
    }
}
