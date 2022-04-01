using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop2022.Migrations
{
    public partial class status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEMpgVMyFBwOf4HarM/2l2RlO6z3MIvjVg4YmlqJ5CPiy5YoHQW4rufSCaIVB0Jloxg==", "0fa1b68f-5e5c-408a-8b16-77f9b5163ae6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAED8HmNA5rMViATZuMVV9HpZWa4uUgn0F75a5d4RMRoENYGGjHunQNhjbt2GTLgb/YQ==", "2ac70c85-d513-4f04-9c4e-a1cddb0ff9df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8e",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAENzForicHfDXxu/CO1/tD7Vtkqxh9e61Sp4/nrBvObGRGluCeFmFoAjpIhGY2v5r/w==", "917aed37-5525-4d56-ad2a-39ce2d22b1bd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEAZ/eM/JBWI88hOdTsKR8sNb3mVKN/Vd55LvTT6gszxjZm0ftsAA1F2/QGVsPIV9Ig==", "885c95a4-a14b-4319-82c2-83b779075f2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEJrItY0Akfplv1zRy2oVgU+Jl/Vaf6yePDIR4cG0lF31P5EB/iR80mmKgT7j3Ej6eg==", "b1bb44c3-da7a-43a6-9039-105d68512860" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8e",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAELLSpemNnoi9qi+wqgzZT2kzZIntJOVuQzgoUuHqh8e7XMmj1lcZYBlUs+yQe1e9Gg==", "b537b43b-3a35-4040-8ff5-f041eefa7a93" });
        }
    }
}
