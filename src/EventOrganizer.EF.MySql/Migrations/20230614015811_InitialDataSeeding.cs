using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventOrganizer.EF.MySql.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventModels_Users_OwnerId",
                table: "EventModels");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "EventModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EventModels",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_EventModels_Users_OwnerId",
                table: "EventModels",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventModels_Users_OwnerId",
                table: "EventModels");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "EventModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "EventModels",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_EventModels_Users_OwnerId",
                table: "EventModels",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
