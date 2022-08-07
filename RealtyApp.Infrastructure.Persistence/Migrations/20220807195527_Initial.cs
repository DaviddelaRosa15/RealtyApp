using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealtyApp.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImmovableAssetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmovableAssetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Improvements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Improvements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImmovableAssets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UrlImage01 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlImage02 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlImage03 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlImage04 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meters = table.Column<double>(type: "float", nullable: false),
                    BedroomQuantity = table.Column<double>(type: "float", nullable: false),
                    BathroomQuantity = table.Column<double>(type: "float", nullable: false),
                    ImmovableAssetTypeId = table.Column<int>(type: "int", nullable: false),
                    SellTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmovableAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmovableAssets_ImmovableAssetTypes_ImmovableAssetTypeId",
                        column: x => x.ImmovableAssetTypeId,
                        principalTable: "ImmovableAssetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImmovableAssets_SellTypes_SellTypeId",
                        column: x => x.SellTypeId,
                        principalTable: "SellTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteImmovables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImmovableAssetId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteImmovables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteImmovables_ImmovableAssets_ImmovableAssetId",
                        column: x => x.ImmovableAssetId,
                        principalTable: "ImmovableAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Improvement_Immovables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImprovementId = table.Column<int>(type: "int", nullable: false),
                    ImmovableAssetId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Improvement_Immovables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Improvement_Immovables_ImmovableAssets_ImmovableAssetId",
                        column: x => x.ImmovableAssetId,
                        principalTable: "ImmovableAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Improvement_Immovables_Improvements_ImprovementId",
                        column: x => x.ImprovementId,
                        principalTable: "Improvements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteImmovables_ImmovableAssetId",
                table: "FavoriteImmovables",
                column: "ImmovableAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmovableAssets_ImmovableAssetTypeId",
                table: "ImmovableAssets",
                column: "ImmovableAssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmovableAssets_SellTypeId",
                table: "ImmovableAssets",
                column: "SellTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Improvement_Immovables_ImmovableAssetId",
                table: "Improvement_Immovables",
                column: "ImmovableAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Improvement_Immovables_ImprovementId",
                table: "Improvement_Immovables",
                column: "ImprovementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteImmovables");

            migrationBuilder.DropTable(
                name: "Improvement_Immovables");

            migrationBuilder.DropTable(
                name: "ImmovableAssets");

            migrationBuilder.DropTable(
                name: "Improvements");

            migrationBuilder.DropTable(
                name: "ImmovableAssetTypes");

            migrationBuilder.DropTable(
                name: "SellTypes");
        }
    }
}
