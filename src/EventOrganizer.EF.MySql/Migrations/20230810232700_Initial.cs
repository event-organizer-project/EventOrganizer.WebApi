using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventOrganizer.EF.MySql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventTags",
                columns: table => new
                {
                    Keyword = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTags", x => x.Keyword);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Nickname = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    RecurrenceType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    IsMessagingAllowed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false),
                    Location = table.Column<string>(type: "longtext", nullable: true),
                    MeetingLink = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventModels_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DialogueMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "longtext", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogueMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialogueMessages_EventModels_EventId",
                        column: x => x.EventId,
                        principalTable: "EventModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DialogueMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventInvolvement",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    LeavingDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInvolvement", x => new { x.EventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_EventInvolvement_EventModels_EventId",
                        column: x => x.EventId,
                        principalTable: "EventModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventInvolvement_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventResults_EventModels_EventId",
                        column: x => x.EventId,
                        principalTable: "EventModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TagToEvent",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Keyword = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagToEvent", x => new { x.EventId, x.Keyword });
                    table.ForeignKey(
                        name: "FK_TagToEvent_EventModels_EventId",
                        column: x => x.EventId,
                        principalTable: "EventModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagToEvent_EventTags_Keyword",
                        column: x => x.Keyword,
                        principalTable: "EventTags",
                        principalColumn: "Keyword",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Nickname" },
                values: new object[,]
                {
                    { 1, "mikita.n@godeltech.com", "Mikita", "N", "mikita.n" },
                    { 2, "john.doe@gmail.com", "John", "Doe", "john.doe" }
                });

            migrationBuilder.InsertData(
                table: "EventModels",
                columns: new[] { "Id", "Description", "Discriminator", "EndDate", "EndTime", "MeetingLink", "OwnerId", "StartDate", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, "Mastery completion and presentation of the final product", "OnlineEvent", new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 0, 0, 0), null, 1, new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 18, 0, 0, 0), "Event organizer presentation" },
                    { 2, "Description created by John", "OnlineEvent", new DateTime(2023, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 17, 0, 0, 0), null, 2, new DateTime(2023, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 0, 0, 0), "Event created by John" }
                });

            migrationBuilder.InsertData(
                table: "TagToEvent",
                columns: new[] { "EventId", "Keyword" },
                values: new object[,]
                {
                    { 1, "godel" },
                    { 1, "online" },
                    { 2, "godel" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DialogueMessages_EventId",
                table: "DialogueMessages",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueMessages_SenderId",
                table: "DialogueMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_EventInvolvement_UserId",
                table: "EventInvolvement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventModels_OwnerId",
                table: "EventModels",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventResults_EventId",
                table: "EventResults",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_TagToEvent_Keyword",
                table: "TagToEvent",
                column: "Keyword");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialogueMessages");

            migrationBuilder.DropTable(
                name: "EventInvolvement");

            migrationBuilder.DropTable(
                name: "EventResults");

            migrationBuilder.DropTable(
                name: "TagToEvent");

            migrationBuilder.DropTable(
                name: "EventModels");

            migrationBuilder.DropTable(
                name: "EventTags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
