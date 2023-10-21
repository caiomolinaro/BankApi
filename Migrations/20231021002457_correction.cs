using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApi.Migrations
{
    /// <inheritdoc />
    public partial class correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_BankAccountType_BankAccountTypeAccountTypeId",
                table: "BankAccount");

            migrationBuilder.DropTable(
                name: "BankAccountType");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_BankAccountTypeAccountTypeId",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "BankAccountTypeAccountTypeId",
                table: "BankAccount");

            migrationBuilder.AddColumn<bool>(
                name: "JuridicPersonAccount",
                table: "BankAccount",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PhisicalPersonAccount",
                table: "BankAccount",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JuridicPersonAccount",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "PhisicalPersonAccount",
                table: "BankAccount");

            migrationBuilder.AddColumn<int>(
                name: "AccountTypeId",
                table: "BankAccount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BankAccountTypeAccountTypeId",
                table: "BankAccount",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankAccountType",
                columns: table => new
                {
                    AccountTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountType", x => x.AccountTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_BankAccountTypeAccountTypeId",
                table: "BankAccount",
                column: "BankAccountTypeAccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_BankAccountType_BankAccountTypeAccountTypeId",
                table: "BankAccount",
                column: "BankAccountTypeAccountTypeId",
                principalTable: "BankAccountType",
                principalColumn: "AccountTypeId");
        }
    }
}
