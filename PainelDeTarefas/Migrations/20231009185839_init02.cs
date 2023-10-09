using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PainelDeTarefas.Migrations
{
    /// <inheritdoc />
    public partial class init02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TempoExecucao",
                table: "Tarefa",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempoExecucao",
                table: "Tarefa");
        }
    }
}
