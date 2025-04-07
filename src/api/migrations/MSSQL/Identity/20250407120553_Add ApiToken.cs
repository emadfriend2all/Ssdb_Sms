using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSH.Starter.WebApi.Migrations.MSSQL.Identity
{
    /// <inheritdoc />
    public partial class AddApiToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiToken",
                schema: "identity",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiToken",
                schema: "identity",
                table: "Users");
        }
    }
}
