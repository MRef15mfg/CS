#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Games.Data.Migrations;

public partial class AddGameProperties : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "Mode",
            table: "Games",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<long>(
            name: "SoldCopies",
            table: "Games",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0L);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Mode",
            table: "Games");

        migrationBuilder.DropColumn(
            name: "SoldCopies",
            table: "Games");
    }
}
