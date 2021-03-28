using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class CodeFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiscData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idnp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    PartyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartyId);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegiondId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegiondId);
                });

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    BlockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    PreviousHash = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YearBirth = table.Column<int>(type: "int", nullable: false),
                    Idbd = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.BlockId);
                    table.ForeignKey(
                        name: "FK_Blocks_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blocks_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegiondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdvnAccounts",
                columns: table => new
                {
                    Idvn = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    VnPassword = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StatusNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdvnAccounts", x => x.Idvn);
                    table.ForeignKey(
                        name: "FK_IdvnAccounts_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegiondId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoteStatuses",
                columns: table => new
                {
                    Idvn = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    VoteState = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteStatuses", x => x.Idvn);
                    table.ForeignKey(
                        name: "FK_VoteStatuses_IdvnAccounts_Idvn",
                        column: x => x.Idvn,
                        principalTable: "IdvnAccounts",
                        principalColumn: "Idvn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_PartyId",
                table: "Blocks",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_RegionId",
                table: "Blocks",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_IdvnAccounts_RegionId",
                table: "IdvnAccounts",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "FiscData");

            migrationBuilder.DropTable(
                name: "VoteStatuses");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "IdvnAccounts");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
