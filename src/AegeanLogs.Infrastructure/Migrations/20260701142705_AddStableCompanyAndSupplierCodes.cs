using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace AegeanLogs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStableCompanyAndSupplierCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Suppliers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ClientCompanies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.Sql(
                                        """
                                        UPDATE [ClientCompanies]
                                        SET [Code] =
                                            CASE
                                                WHEN [ContactEmail] = N'ops@aegeanblue.example'
                                                     OR [Name] = N'Aegean Blue Shipping Ltd'
                                                    THEN N'AEGEAN_BLUE'

                                                WHEN [ContactEmail] = N'operations@hellenicbulk.example'
                                                     OR [Name] = N'Hellenic Bulk Operators'
                                                    THEN N'HELLENIC_BULK'

                                                ELSE CONCAT(N'CLIENT_', [Id])
                                            END
                                        WHERE [Code] IS NULL;
                                        """);

            migrationBuilder.Sql(
                                        """
                                        UPDATE [Suppliers]
                                        SET [Code] =
                                            CASE
                                                WHEN [ContactEmail] = N'dispatch@piraeusmarinesupplies.example'
                                                     OR [Name] = N'Piraeus Marine Supplies'
                                                    THEN N'PIRAEUS_MARINE_SUPPLIES'

                                                WHEN [ContactEmail] = N'ops@atticawaste.example'
                                                     OR [Name] = N'Attica Waste Services'
                                                    THEN N'ATTICA_WASTE_SERVICES'

                                                WHEN [ContactEmail] = N'booking@aegeancrewtransport.example'
                                                     OR [Name] = N'Aegean Crew Transport'
                                                    THEN N'AEGEAN_CREW_TRANSPORT'

                                                WHEN [ContactEmail] = N'service@hellastechmarine.example'
                                                     OR [Name] = N'Hellas Technical Marine'
                                                    THEN N'HELLAS_TECHNICAL_MARINE'

                                                ELSE CONCAT(N'SUPPLIER_', [Id])
                                            END
                                        WHERE [Code] IS NULL;
                                        """);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Suppliers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ClientCompanies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Code",
                table: "Suppliers",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientCompanies_Code",
                table: "ClientCompanies",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suppliers_Code",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_ClientCompanies_Code",
                table: "ClientCompanies");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ClientCompanies");
        }
    }
}
