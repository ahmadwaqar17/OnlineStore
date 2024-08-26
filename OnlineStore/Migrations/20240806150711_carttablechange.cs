using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class carttablechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_Pid",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_Email",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_Email",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_Pid",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Email",
                table: "Carts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Pid",
                table: "Carts",
                column: "Pid");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_Pid",
                table: "Carts",
                column: "Pid",
                principalTable: "Products",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_Email",
                table: "Carts",
                column: "Email",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
