using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class quoteform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Qutoes",
                table: "Qutoes");

            migrationBuilder.RenameTable(
                name: "Qutoes",
                newName: "Quotes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quotes",
                table: "Quotes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Quotes",
                table: "Quotes");

            migrationBuilder.RenameTable(
                name: "Quotes",
                newName: "Qutoes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Qutoes",
                table: "Qutoes",
                column: "Id");
        }
    }
}
