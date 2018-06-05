using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContatosWebAPI.Migrations
{
    public partial class PopularContatoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Contatos(Nome,Email,Telefone) " +
              "VALUES('Macoratti','macoratti@yahoo.com','11-9988-5544')");
            migrationBuilder.Sql("INSERT INTO Contatos(Nome,Email,Telefone) " +
                "VALUES('Janice','janjan@yahoo.com','11-5548-3322')");
            migrationBuilder.Sql("INSERT INTO Contatos(Nome,Email,Telefone) " +
                "VALUES('Bianca','bibi@uol.com.br','21-6648-1100')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Contatos");
        }
    }
}
