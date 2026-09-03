using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[] { new Guid("7f3b2c91-4d68-4a15-9e27-81c6f5b90342"), "admin@gmail.com", "Admin", "$2a$11$tcNLAG/B/Hu/4jqUmCrw3eBV6OsxWtSD8JLUhS6duobkNIGkgieWW", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7f3b2c91-4d68-4a15-9e27-81c6f5b90342"));
        }
    }
}
