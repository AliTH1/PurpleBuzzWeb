using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurpleBuzzWeb.Migrations
{
    public partial class addedImagePathColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "TeamMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "TeamMembers");
        }
    }
}
