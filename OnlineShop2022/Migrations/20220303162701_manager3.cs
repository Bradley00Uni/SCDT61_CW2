using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop2022.Migrations
{
    public partial class manager3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEJa28ptbtxVMevBeJApiAIfp9gevgbbf7yztlPXEjbj11fbCZFZMvrcFLr9bP5Lg9g==", "ce5fd568-46c0-4480-abb6-09fa1941568a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8d",
                columns: new[] { "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEFkz/KrbH6Uu65/PFe19GPbv5NXBEFJyESQBC09HTYtFyH23J2xIBIjtyh8rLxLmjw==", "5cd0a573-61c3-49c0-84cc-ed6d877c1ef2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "AQAAAAEAACcQAAAAEPlryIH5BE+ejBv9L/WsyW1gtLGCZ+679O/ZqZ4tVRAu/zqzrsoKigNgGiGgUviDnA==", "a2a82072-3383-4a60-9a5d-4e97cc68ab8c" });
        }
    }
}
