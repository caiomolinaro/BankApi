using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApi.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameClient = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneClient = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    DocumentClient = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailClient = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BalanceClient = table.Column<decimal>(type: "decimal(18,4)", maxLength: 255, nullable: false),
                    AccountTypeId = table.Column<int>(type: "int", nullable: false),
                    BankAccountTypeAccountTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_BankAccount_BankAccountType_BankAccountTypeAccountTypeId",
                        column: x => x.BankAccountTypeAccountTypeId,
                        principalTable: "BankAccountType",
                        principalColumn: "AccountTypeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_BankAccountTypeAccountTypeId",
                table: "BankAccount",
                column: "BankAccountTypeAccountTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "BankAccountType");
        }
    }
}
