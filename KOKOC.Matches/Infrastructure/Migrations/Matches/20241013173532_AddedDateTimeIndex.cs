using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KOKOC.Matches.Infrastructure.Migrations.Matches
{
    /// <inheritdoc />
    public partial class AddedDateTimeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Matches_StartingAt",
                table: "Matches",
                column: "StartingAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_StartingAt",
                table: "Matches");
        }
    }
}
