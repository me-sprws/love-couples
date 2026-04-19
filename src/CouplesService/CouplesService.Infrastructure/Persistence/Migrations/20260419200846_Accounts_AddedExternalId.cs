using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CouplesService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accounts_AddedExternalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Accounts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Accounts");
        }
    }
}
