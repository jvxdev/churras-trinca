using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChurrasTrinca.Migrations
{
    /// <inheritdoc />
    public partial class EstimativaDePessoas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstimativaPessoas",
                table: "Churrascos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimativaPessoas",
                table: "Churrascos");
        }
    }
}
