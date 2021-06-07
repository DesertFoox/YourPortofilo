using Microsoft.EntityFrameworkCore.Migrations;

namespace Infranstructure.Migrations
{
    public partial class Addedisverifiedtoblogcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "BlogComments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "BlogComments");
        }
    }
}
