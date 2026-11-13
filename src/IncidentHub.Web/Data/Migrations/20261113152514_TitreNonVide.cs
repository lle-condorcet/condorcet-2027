using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncidentHub.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class TitreNonVide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Incidents_Title_NonVide",
                table: "Incidents",
                sql: "length(trim(\"Title\")) > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Incidents_Title_NonVide",
                table: "Incidents");
        }
    }
}
