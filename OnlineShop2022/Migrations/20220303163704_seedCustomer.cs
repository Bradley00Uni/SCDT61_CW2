using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop2022.Migrations
{
    public partial class seedCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEFkz/KrbH6Uu65/PFe19GPbv5NXBEFJyESQBC09HTYtFyH23J2xIBIjtyh8rLxLmjw==", "5cd0a573-61c3-49c0-84cc-ed6d877c1ef2" });
        }
    }
}
