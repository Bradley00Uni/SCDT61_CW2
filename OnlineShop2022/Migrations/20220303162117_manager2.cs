using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop2022.Migrations
{
    public partial class manager2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEDhz81Fqv0bLOXByrOiL50k58ZIAuSB45nhnW/WXnWByvQLnOiAQN+atHSiYFPEvOQ==", "86ed4dd1-5241-4d04-be66-dac45f0efd57" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEPlryIH5BE+ejBv9L/WsyW1gtLGCZ+679O/ZqZ4tVRAu/zqzrsoKigNgGiGgUviDnA==", "a2a82072-3383-4a60-9a5d-4e97cc68ab8c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEH94rrsGEclr24+4s0msZckSbvGERNhFCqmTiH+MYFrTHZrAKOtwh1CMAIug6lfTsA==", "afc944f9-bba8-47ea-b963-87bd44200759" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEBJfOJmtBUdIh96ihd7P5zsTKGTaY1LdreveEXGNX/2X/0yeJI11K0fviI5lrBnqyw==", "9e771626-7979-4d03-ac75-777fd621f076" });
        }
    }
}
