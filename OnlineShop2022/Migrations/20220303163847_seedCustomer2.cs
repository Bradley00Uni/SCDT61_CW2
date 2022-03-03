using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop2022.Migrations
{
    public partial class seedCustomer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Sname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "27b9af34-a133-43e2-8dd2-aef04ddb2b8e", 0, "9b483dfe-e56c-4d5b-97cd-b32652794d29", "customer@customer.com", false, "Customer", false, null, "CUSTOMER@CUSTOMER.COM", "CUSTOMER@CUSTOMER.COM", "AQAAAAEAACcQAAAAELLSpemNnoi9qi+wqgzZT2kzZIntJOVuQzgoUuHqh8e7XMmj1lcZYBlUs+yQe1e9Gg==", null, false, "b537b43b-3a35-4040-8ff5-f041eefa7a93", "Customer", false, "customer@customer.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ecfbe7ad-bb6b-49e6-ac2b-6359a73fbf02", "27b9af34-a133-43e2-8dd2-aef04ddb2b8e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ecfbe7ad-bb6b-49e6-ac2b-6359a73fbf02", "27b9af34-a133-43e2-8dd2-aef04ddb2b8e" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAELVwomm5cw/myKxMLEgJMCUD1EgjFbQjgMcMwGP3p+9nVCFjcqYcXFgh6sLW15YObw==", "fe72f9c9-7e74-4910-83a7-261007602065" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8d",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEPpe55T1VXKUOOrgBLR55LCEGP+rheEKVYiU/BCq/1xrGf0mzuBpnKKAD0WnrlH0VA==", "0833c50f-1386-44d8-a716-f5a749a1fb37" });
        }
    }
}
