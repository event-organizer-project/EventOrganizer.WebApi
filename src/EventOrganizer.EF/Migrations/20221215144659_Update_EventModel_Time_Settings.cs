using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventOrganizer.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventModelTimeSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "EventModels",
                type: "time(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "EventModels",
                type: "datetime2(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "EventModels",
                type: "time(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "EventModels",
                type: "datetime2(0)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "EventModels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "EventModels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "EventModels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "EventModels",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldNullable: true);
        }
    }
}
