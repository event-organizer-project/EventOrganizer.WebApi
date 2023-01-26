using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventOrganizer.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventModels",
                columns: new[] { "Id", "Description", "Discriminator", "EndDate", "EndTime", "MeetingLink", "OwnerId", "StartDate", "StartTime", "Title" },
                values: new object[] { 1, "Mastery completion and presentation of the final product", "OnlineEvent", new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 0, 0, 0), null, null, new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 18, 0, 0, 0), "Event organizer presentation" });

            migrationBuilder.InsertData(
                table: "EventTags",
                column: "Keyword",
                values: new object[]
                {
                    "entertainment",
                    "godel",
                    "online"
                });

            migrationBuilder.InsertData(
                table: "TagToEvent",
                columns: new[] { "EventId", "Keyword" },
                values: new object[,]
                {
                    { 1, "godel" },
                    { 1, "online" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumn: "Keyword",
                keyValue: "entertainment");

            migrationBuilder.DeleteData(
                table: "TagToEvent",
                keyColumns: new[] { "EventId", "Keyword" },
                keyValues: new object[] { 1, "godel" });

            migrationBuilder.DeleteData(
                table: "TagToEvent",
                keyColumns: new[] { "EventId", "Keyword" },
                keyValues: new object[] { 1, "online" });

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumn: "Keyword",
                keyValue: "godel");

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumn: "Keyword",
                keyValue: "online");

            migrationBuilder.DeleteData(
                table: "EventModels",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
