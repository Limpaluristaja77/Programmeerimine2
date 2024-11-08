using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class BuildingsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Materials_MaterialsId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Panels_PanelsId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_MaterialsId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_PanelsId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "MaterialsId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "PanelsId",
                table: "Buildings");

            migrationBuilder.AddColumn<int>(
                name: "BuildingsId",
                table: "Panels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuildingsId",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PanelId",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialId",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Panels_BuildingsId",
                table: "Panels",
                column: "BuildingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_BuildingsId",
                table: "Materials",
                column: "BuildingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Buildings_BuildingsId",
                table: "Materials",
                column: "BuildingsId",
                principalTable: "Buildings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Panels_Buildings_BuildingsId",
                table: "Panels",
                column: "BuildingsId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Buildings_BuildingsId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Panels_Buildings_BuildingsId",
                table: "Panels");

            migrationBuilder.DropIndex(
                name: "IX_Panels_BuildingsId",
                table: "Panels");

            migrationBuilder.DropIndex(
                name: "IX_Materials_BuildingsId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "BuildingsId",
                table: "Panels");

            migrationBuilder.DropColumn(
                name: "BuildingsId",
                table: "Materials");

            migrationBuilder.AlterColumn<int>(
                name: "PanelId",
                table: "Buildings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialId",
                table: "Buildings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "MaterialsId",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PanelsId",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_MaterialsId",
                table: "Buildings",
                column: "MaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PanelsId",
                table: "Buildings",
                column: "PanelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Materials_MaterialsId",
                table: "Buildings",
                column: "MaterialsId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Panels_PanelsId",
                table: "Buildings",
                column: "PanelsId",
                principalTable: "Panels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
