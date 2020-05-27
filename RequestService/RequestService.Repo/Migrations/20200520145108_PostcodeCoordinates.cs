using System;
using HelpMyStreet.PostcodeCoordinates.EF.Extensions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Repo.Migrations
{
    public partial class PostcodeCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Address");

            migrationBuilder.EnsureSchema(
                name: "Staging");

            migrationBuilder.CreateTable(
                name: "Postcode",
                schema: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Postcode = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postcode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Postcode_Old",
                schema: "Staging",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Postcode = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postcode_Old", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Postcode_Staging",
                schema: "Staging",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Postcode = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postcode_Staging", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Postcode_Switch",
                schema: "Staging",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Postcode = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "GetUtcDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postcode_Switch", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UX_Postcode_Postcode",
                schema: "Address",
                table: "Postcode",
                column: "Postcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postcode_Postcode_IsActive",
                schema: "Address",
                table: "Postcode",
                columns: new[] { "Postcode", "IsActive" })
                .Annotation("SqlServer:Include", new[] { "Latitude", "Longitude" });

            migrationBuilder.CreateIndex(
                name: "IX_Postcode_Latitude_Longitude_IsActive",
                schema: "Address",
                table: "Postcode",
                columns: new[] { "Latitude", "Longitude", "IsActive" })
                .Annotation("SqlServer:Include", new[] { "Postcode" });

            migrationBuilder.CreateIndex(
                name: "UX_Postcode_Postcode",
                schema: "Staging",
                table: "Postcode_Old",
                column: "Postcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postcode_Postcode_IsActive",
                schema: "Staging",
                table: "Postcode_Old",
                columns: new[] { "Postcode", "IsActive" })
                .Annotation("SqlServer:Include", new[] { "Latitude", "Longitude" });

            migrationBuilder.CreateIndex(
                name: "IX_Postcode_Latitude_Longitude_IsActive",
                schema: "Staging",
                table: "Postcode_Old",
                columns: new[] { "Latitude", "Longitude", "IsActive" })
                .Annotation("SqlServer:Include", new[] { "Postcode" });

            migrationBuilder.CreateIndex(
                name: "UX_Postcode_Postcode",
                schema: "Staging",
                table: "Postcode_Switch",
                column: "Postcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postcode_Postcode_IsActive",
                schema: "Staging",
                table: "Postcode_Switch",
                columns: new[] { "Postcode", "IsActive" })
                .Annotation("SqlServer:Include", new[] { "Latitude", "Longitude" });

            migrationBuilder.CreateIndex(
                name: "IX_Postcode_Latitude_Longitude_IsActive",
                schema: "Staging",
                table: "Postcode_Switch",
                columns: new[] { "Latitude", "Longitude", "IsActive" })
                .Annotation("SqlServer:Include", new[] { "Postcode" });

            migrationBuilder.DropPostcodeLoadProcIfItExists();
            migrationBuilder.CreatePostcodeLoadProc();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Postcode",
                schema: "Address");

            migrationBuilder.DropTable(
                name: "Postcode_Old",
                schema: "Staging");

            migrationBuilder.DropTable(
                name: "Postcode_Staging",
                schema: "Staging");

            migrationBuilder.DropTable(
                name: "Postcode_Switch",
                schema: "Staging");


            migrationBuilder.DropPostcodeLoadProcIfItExists();
        }
    }
}
