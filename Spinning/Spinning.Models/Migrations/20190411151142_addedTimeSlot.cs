using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spinning.Models.Migrations
{
    public partial class addedTimeSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionDate",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "TimeslotId",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Timeslots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeslots", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TimeslotId",
                table: "Reservations",
                column: "TimeslotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Timeslots_TimeslotId",
                table: "Reservations",
                column: "TimeslotId",
                principalTable: "Timeslots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Timeslots_TimeslotId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Timeslots");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TimeslotId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TimeslotId",
                table: "Reservations");

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionDate",
                table: "Reservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
