using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Persistence.Migrations
{
    public partial class DatesPropAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUpdatedOn",
                table: "tbl_Genres",
                newName: "IsUpdatedon");

            migrationBuilder.RenameColumn(
                name: "IsCreated",
                table: "tbl_Genres",
                newName: "IsCreatedOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "IsCreatedOn",
                table: "tbl_Authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tbl_Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "IsUpdatedon",
                table: "tbl_Authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCreatedOn",
                table: "tbl_Authors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tbl_Authors");

            migrationBuilder.DropColumn(
                name: "IsUpdatedon",
                table: "tbl_Authors");

            migrationBuilder.RenameColumn(
                name: "IsUpdatedon",
                table: "tbl_Genres",
                newName: "IsUpdatedOn");

            migrationBuilder.RenameColumn(
                name: "IsCreatedOn",
                table: "tbl_Genres",
                newName: "IsCreated");
        }
    }
}
