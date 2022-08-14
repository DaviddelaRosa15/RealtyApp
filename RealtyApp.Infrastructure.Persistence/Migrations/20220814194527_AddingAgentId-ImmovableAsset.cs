using Microsoft.EntityFrameworkCore.Migrations;

namespace RealtyApp.Infrastructure.Persistence.Migrations
{
    public partial class AddingAgentIdImmovableAsset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BedroomQuantity",
                table: "ImmovableAssets",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "BathroomQuantity",
                table: "ImmovableAssets",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "ImmovableAssets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "ImmovableAssets");

            migrationBuilder.AlterColumn<double>(
                name: "BedroomQuantity",
                table: "ImmovableAssets",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "BathroomQuantity",
                table: "ImmovableAssets",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
