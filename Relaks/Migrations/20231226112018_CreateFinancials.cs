using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class CreateFinancials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialAccountCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAccountCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialAccountCategories_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialTransactionCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TreePath = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialTransactionCategories_FinancialTransactionCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FinancialTransactionCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FinancialAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    FinancialCurrencyId = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Balance = table.Column<decimal>(type: "TEXT", precision: 19, scale: 4, nullable: false),
                    BaseEntryId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialAccounts_Entries_BaseEntryId",
                        column: x => x.BaseEntryId,
                        principalTable: "Entries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinancialAccounts_FinancialAccountCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "FinancialAccountCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialAccounts_FinancialCurrencies_FinancialCurrencyId",
                        column: x => x.FinancialCurrencyId,
                        principalTable: "FinancialCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsPlus = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", precision: 19, scale: 4, nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", precision: 19, scale: 4, nullable: false),
                    SecondAccountId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ReverseTransactionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_FinancialAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_FinancialAccounts_SecondAccountId",
                        column: x => x.SecondAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialTransactionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", precision: 19, scale: 4, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialTransactionItems_FinancialTransactionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "FinancialTransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialTransactionItems_FinancialTransactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "FinancialTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAccountCategories_EntryId",
                table: "FinancialAccountCategories",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAccounts_BaseEntryId",
                table: "FinancialAccounts",
                column: "BaseEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAccounts_CategoryId",
                table: "FinancialAccounts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAccounts_FinancialCurrencyId",
                table: "FinancialAccounts",
                column: "FinancialCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactionCategories_ParentId",
                table: "FinancialTransactionCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactionItems_CategoryId",
                table: "FinancialTransactionItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactionItems_TransactionId",
                table: "FinancialTransactionItems",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_AccountId",
                table: "FinancialTransactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_EntryId",
                table: "FinancialTransactions",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_SecondAccountId",
                table: "FinancialTransactions",
                column: "SecondAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialTransactionItems");

            migrationBuilder.DropTable(
                name: "FinancialTransactionCategories");

            migrationBuilder.DropTable(
                name: "FinancialTransactions");

            migrationBuilder.DropTable(
                name: "FinancialAccounts");

            migrationBuilder.DropTable(
                name: "FinancialAccountCategories");
        }
    }
}
