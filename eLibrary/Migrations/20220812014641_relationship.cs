using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eLibrary.Migrations
{
    public partial class relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BookDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_ApplicationUserId",
                table: "BookDetails",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetails_AspNetUsers_ApplicationUserId",
                table: "BookDetails",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDetails_AspNetUsers_ApplicationUserId",
                table: "BookDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookDetails_ApplicationUserId",
                table: "BookDetails");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BookDetails");
        }
    }
}
