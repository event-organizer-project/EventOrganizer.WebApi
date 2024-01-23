using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventOrganizer.EF.MySql.Migrations
{
    /// <inheritdoc />
    public partial class Update_DateTime_To_DateTimeOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "EventModels");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "EventModels");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartDate",
                table: "EventModels",
                type: "DATETIME(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndDate",
                table: "EventModels",
                type: "DATETIME(6)",
                nullable: false,
                defaultValueSql: "(DATE_ADD(CURRENT_TIMESTAMP(6), INTERVAL 2 HOUR))",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastEndDate",
                table: "EventModels",
                type: "DATETIME(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EventModels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "LastEndDate", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 24, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 1, 24, 12, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "EventModels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "LastEndDate", "StartDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 26, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2024, 1, 26, 18, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEndDate",
                table: "EventModels");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "EventModels",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIME(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "EventModels",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIME(6)",
                oldDefaultValueSql: "(DATE_ADD(CURRENT_TIMESTAMP(6), INTERVAL 2 HOUR))");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "EventModels",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "EventModels",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "EventModels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "EndTime", "StartDate", "StartTime" },
                values: new object[] { new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 0, 0, 0), new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 18, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "EventModels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "EndTime", "StartDate", "StartTime" },
                values: new object[] { new DateTime(2023, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 17, 0, 0, 0), new DateTime(2023, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 0, 0, 0) });
        }
    }
}
