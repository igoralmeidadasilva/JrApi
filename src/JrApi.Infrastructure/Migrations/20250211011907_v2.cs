using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JrApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address_city",
                table: "users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_country",
                table: "users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_district",
                table: "users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "address_number",
                table: "users",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "address_state",
                table: "users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_street",
                table: "users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_zip_code",
                table: "users",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "birthdate",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "users",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "users",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "address_city",
                table: "users");

            migrationBuilder.DropColumn(
                name: "address_country",
                table: "users");

            migrationBuilder.DropColumn(
                name: "address_district",
                table: "users");

            migrationBuilder.DropColumn(
                name: "address_number",
                table: "users");

            migrationBuilder.DropColumn(
                name: "address_state",
                table: "users");

            migrationBuilder.DropColumn(
                name: "address_street",
                table: "users");

            migrationBuilder.DropColumn(
                name: "address_zip_code",
                table: "users");

            migrationBuilder.DropColumn(
                name: "birthdate",
                table: "users");

            migrationBuilder.DropColumn(
                name: "email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password",
                table: "users");

            migrationBuilder.DropColumn(
                name: "role",
                table: "users");
        }
    }
}
