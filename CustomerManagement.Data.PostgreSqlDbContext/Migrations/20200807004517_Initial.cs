using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CustomerManagement.Data.PostgreSqlDbContext.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "EntityFrameworkHiLoSequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "tbl_companies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    address = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_titles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: false),
                    value = table.Column<string>(maxLength: 100, nullable: true),
                    description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_customers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    surname = table.Column<string>(maxLength: 50, nullable: true),
                    company_id = table.Column<long>(nullable: false),
                    title_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_customers_tbl_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "tbl_companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_customers_tbl_titles_title_id",
                        column: x => x.title_id,
                        principalTable: "tbl_titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_customers_company_id",
                table: "tbl_customers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_customers_title_id",
                table: "tbl_customers",
                column: "title_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_customers");

            migrationBuilder.DropTable(
                name: "tbl_companies");

            migrationBuilder.DropTable(
                name: "tbl_titles");

            migrationBuilder.DropSequence(
                name: "EntityFrameworkHiLoSequence");
        }
    }
}
