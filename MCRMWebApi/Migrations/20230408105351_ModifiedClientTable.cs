using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCRMWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Clients",
                newName: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Clients",
                newName: "Owner");
        }
    }
}
