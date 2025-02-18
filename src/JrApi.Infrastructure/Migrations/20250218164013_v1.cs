using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JrApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    address_street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, defaultValue: ""),
                    address_city = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, defaultValue: ""),
                    address_district = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, defaultValue: ""),
                    address_number = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    address_state = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, defaultValue: ""),
                    address_country = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, defaultValue: ""),
                    address_zip_code = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true, defaultValue: ""),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_is_deleted",
                table: "users",
                column: "is_deleted",
                filter: "is_deleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
