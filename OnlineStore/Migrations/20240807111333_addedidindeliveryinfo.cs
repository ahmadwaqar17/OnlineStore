using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Migrations
{
    /// <inheritdoc />
    public partial class addedidindeliveryinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryInfos",
                table: "DeliveryInfos");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "DeliveryInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DeliveryInfos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryInfos",
                table: "DeliveryInfos",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryInfos",
                table: "DeliveryInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DeliveryInfos");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "DeliveryInfos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryInfos",
                table: "DeliveryInfos",
                column: "Email");
        }
    }
}
