using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChurrasTrinca.Migrations
{
    /// <inheritdoc />
    public partial class GetContribuicaoTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorContribuicaoBebidas",
                table: "Churrascos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorContribuicaoChurras",
                table: "Churrascos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorContribuicaoBebidas",
                table: "Churrascos");

            migrationBuilder.DropColumn(
                name: "ValorContribuicaoChurras",
                table: "Churrascos");
        }
    }
}
