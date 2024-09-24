using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JrApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: true),
                    last_name = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    password = table.Column<string>(type: "TEXT", nullable: true),
                    street = table.Column<string>(type: "TEXT", nullable: true, defaultValue: ""),
                    city = table.Column<string>(type: "TEXT", nullable: true, defaultValue: ""),
                    district = table.Column<string>(type: "TEXT", nullable: true, defaultValue: ""),
                    number = table.Column<int>(type: "INTEGER", nullable: true, defaultValue: 0),
                    state = table.Column<string>(type: "TEXT", nullable: true, defaultValue: ""),
                    country = table.Column<string>(type: "TEXT", nullable: true, defaultValue: ""),
                    zip_code = table.Column<string>(type: "TEXT", nullable: true, defaultValue: ""),
                    birthdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    role = table.Column<string>(type: "TEXT", nullable: false),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    deleted_on_utc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
