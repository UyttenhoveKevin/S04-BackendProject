using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spinning.Models.Migrations
{
    public partial class AddedPenalties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_SpinningUsersId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Timeslots_TimeslotId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "SpinningUsersId",
                table: "Reservations",
                newName: "SpinningUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_SpinningUsersId",
                table: "Reservations",
                newName: "IX_Reservations_SpinningUserId");

            migrationBuilder.AlterColumn<int>(
                name: "TimeslotId",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpinningUserId = table.Column<string>(nullable: true),
                    StartdatePenalty = table.Column<DateTime>(nullable: false),
                    EnddatePenalty = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalties_AspNetUsers_SpinningUserId",
                        column: x => x.SpinningUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_SpinningUserId",
                table: "Penalties",
                column: "SpinningUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_SpinningUserId",
                table: "Reservations",
                column: "SpinningUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Timeslots_TimeslotId",
                table: "Reservations",
                column: "TimeslotId",
                principalTable: "Timeslots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_SpinningUserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Timeslots_TimeslotId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.RenameColumn(
                name: "SpinningUserId",
                table: "Reservations",
                newName: "SpinningUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_SpinningUserId",
                table: "Reservations",
                newName: "IX_Reservations_SpinningUsersId");

            migrationBuilder.AlterColumn<int>(
                name: "TimeslotId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_SpinningUsersId",
                table: "Reservations",
                column: "SpinningUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Timeslots_TimeslotId",
                table: "Reservations",
                column: "TimeslotId",
                principalTable: "Timeslots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
