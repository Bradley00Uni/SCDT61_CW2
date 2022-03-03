using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop2022.Migrations
{
    public partial class manager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEH94rrsGEclr24+4s0msZckSbvGERNhFCqmTiH+MYFrTHZrAKOtwh1CMAIug6lfTsA==", "afc944f9-bba8-47ea-b963-87bd44200759" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Sname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "27b9af34-a133-43e2-8dd2-aef04ddb2b8d", 0, "8b483dfe-e56c-4d5b-97cd-b32652794d29", "manager@manager.com", false, "Manager", false, null, null, "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEBJfOJmtBUdIh96ihd7P5zsTKGTaY1LdreveEXGNX/2X/0yeJI11K0fviI5lrBnqyw==", null, false, "9e771626-7979-4d03-ac75-777fd621f076", "Manager", false, "manager@manager.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ecfbe7ad-bb6b-49e6-ac2b-6359a73fbf02", "27b9af34-a133-43e2-8dd2-aef04ddb2b8d" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "709a40af-4a4e-40b6-887b-d30dcdf07030", "27b9af34-a133-43e2-8dd2-aef04ddb2b8d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "709a40af-4a4e-40b6-887b-d30dcdf07030", "27b9af34-a133-43e2-8dd2-aef04ddb2b8d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ecfbe7ad-bb6b-49e6-ac2b-6359a73fbf02", "27b9af34-a133-43e2-8dd2-aef04ddb2b8d" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEDjB2ydvTIXT4D3bbScPBZNDz1yIv4k/IZ//bNhIvLR7b3HPAC1JuRF5F4XO5z6rpQ==", "b3dce930-90ca-481b-b878-0209c967a6aa" });
        }
    }
}
