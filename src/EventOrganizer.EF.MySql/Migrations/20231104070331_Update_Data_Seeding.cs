using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventOrganizer.EF.MySql.Migrations
{
    /// <inheritdoc />
    public partial class Update_Data_Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventInvolvement",
                columns: new[] { "EventId", "UserId", "LeavingDate" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 2, 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventInvolvement",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "EventInvolvement",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 2, 2 });
        }
    }
}
