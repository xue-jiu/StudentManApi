using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManApi.Migrations
{
    public partial class newTea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyClasses_Teachers_TeacherId",
                table: "MyClasses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_MyClasses_TeacherId",
                table: "MyClasses");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId1",
                table: "MyClasses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "AspNetUserClaims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "308660dc-ae51-480f-824d-7dca6714c3e2", "2698123a-08f1-45f0-8aa6-9d0f111e117f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "90184155-dee0-40c9-bb1e-b5ed07afc04e", 0, "浙江", "9998ea60-1989-4239-9f8a-93f93b10e5fe", "Teacher", "admin@fakexiecheng.com", true, false, null, "ADMIN@FAKEXIECHENG.COM", "ADMIN@FAKEXIECHENG.COM", "AQAAAAEAACcQAAAAEOpmSSMwSbFyDAkQZ1xM3EOXtbMTy3XVAvXunr78LrtX5owdrKnqvGFn4yhzBB8Bsw==", "123456789", false, "語文", "f1802918-a79d-4801-aaf0-a7131c8ab401", false, "admin@fakexiecheng.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "308660dc-ae51-480f-824d-7dca6714c3e2", "90184155-dee0-40c9-bb1e-b5ed07afc04e" });

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_TeacherId1",
                table: "MyClasses",
                column: "TeacherId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_TeacherId",
                table: "AspNetUserTokens",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_TeacherId",
                table: "AspNetUserLogins",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_TeacherId",
                table: "AspNetUserClaims",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_TeacherId",
                table: "AspNetUserClaims",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_TeacherId",
                table: "AspNetUserLogins",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_TeacherId",
                table: "AspNetUserTokens",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyClasses_AspNetUsers_TeacherId1",
                table: "MyClasses",
                column: "TeacherId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_TeacherId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_TeacherId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_TeacherId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_MyClasses_AspNetUsers_TeacherId1",
                table: "MyClasses");

            migrationBuilder.DropIndex(
                name: "IX_MyClasses_TeacherId1",
                table: "MyClasses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_TeacherId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_TeacherId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_TeacherId",
                table: "AspNetUserClaims");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "308660dc-ae51-480f-824d-7dca6714c3e2", "90184155-dee0-40c9-bb1e-b5ed07afc04e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "308660dc-ae51-480f-824d-7dca6714c3e2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "MyClasses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "AspNetUserClaims");

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_TeacherId",
                table: "MyClasses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyClasses_Teachers_TeacherId",
                table: "MyClasses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
