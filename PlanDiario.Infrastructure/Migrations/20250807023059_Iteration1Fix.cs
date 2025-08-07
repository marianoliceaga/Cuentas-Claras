using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanDiario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Iteration1Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_idSubcategory",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_idSubcategory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "idSubcategory",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SubcategoryId", "SubcategoryId1" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SubcategoryId", "SubcategoryId1" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SubcategoryId", "SubcategoryId1" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "SubcategoryId", "SubcategoryId1" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "SubcategoryId", "SubcategoryId1" },
                values: new object[] { 3, null });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId1",
                table: "Products",
                column: "SubcategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId1",
                table: "Products",
                column: "SubcategoryId1",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubcategoryId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubcategoryId1",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "idSubcategory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SubcategoryId", "idSubcategory" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SubcategoryId", "idSubcategory" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SubcategoryId", "idSubcategory" },
                values: new object[] { null, 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "SubcategoryId", "idSubcategory" },
                values: new object[] { null, 2 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "SubcategoryId", "idSubcategory" },
                values: new object[] { null, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Products_idSubcategory",
                table: "Products",
                column: "idSubcategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_idSubcategory",
                table: "Products",
                column: "idSubcategory",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
