using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Helmes.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinalBag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalBag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LetterBag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BagNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    LetterCount = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false),
                    FinalBagId = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterBag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterBag_FinalBag_FinalBagId",
                        column: x => x.FinalBagId,
                        principalTable: "FinalBag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParcelBag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BagNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false),
                    FinalBagId = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelBag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParcelBag_FinalBag_FinalBagId",
                        column: x => x.FinalBagId,
                        principalTable: "FinalBag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Airport = table.Column<int>(type: "int", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BagListId = table.Column<int>(type: "int", nullable: true),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipment_FinalBag_BagListId",
                        column: x => x.BagListId,
                        principalTable: "FinalBag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parcel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcelNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RecipientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DestinationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ParcelBagId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcel_ParcelBag_ParcelBagId",
                        column: x => x.ParcelBagId,
                        principalTable: "ParcelBag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LetterBag_BagNumber",
                table: "LetterBag",
                column: "BagNumber",
                unique: true,
                filter: "[BagNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LetterBag_FinalBagId",
                table: "LetterBag",
                column: "FinalBagId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_ParcelBagId",
                table: "Parcel",
                column: "ParcelBagId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_ParcelNumber",
                table: "Parcel",
                column: "ParcelNumber",
                unique: true,
                filter: "[ParcelNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelBag_BagNumber",
                table: "ParcelBag",
                column: "BagNumber",
                unique: true,
                filter: "[BagNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelBag_FinalBagId",
                table: "ParcelBag",
                column: "FinalBagId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_BagListId",
                table: "Shipment",
                column: "BagListId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_ShipmentNumber",
                table: "Shipment",
                column: "ShipmentNumber",
                unique: true,
                filter: "[ShipmentNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterBag");

            migrationBuilder.DropTable(
                name: "Parcel");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "ParcelBag");

            migrationBuilder.DropTable(
                name: "FinalBag");
        }
    }
}
