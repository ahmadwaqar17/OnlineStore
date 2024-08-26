using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class updateddeliveryinfotabke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryInfos_Users_UserEmail",
                table: "DeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryInfos_UserEmail",
                table: "DeliveryInfos");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "DeliveryInfos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "DeliveryInfos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryInfos_UserEmail",
                table: "DeliveryInfos",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryInfos_Users_UserEmail",
                table: "DeliveryInfos",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
