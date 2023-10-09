﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PainelDeTarefas.Data;

#nullable disable

namespace PainelDeTarefas.Migrations
{
    [DbContext(typeof(PainelDeTarefasContext))]
    partial class PainelDeTarefasContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PainelDeTarefas.Models.Funcionarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("PainelDeTarefas.Models.Tarefas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HoraFinalEstimada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("ResponsavelId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TempoDecorrido")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TempoEstimadoEmHoras")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TempoExecucao")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("ResponsavelId");

                    b.ToTable("Tarefa");
                });

            modelBuilder.Entity("PainelDeTarefas.Models.Tarefas", b =>
                {
                    b.HasOne("PainelDeTarefas.Models.Funcionarios", "Responsavel")
                        .WithMany("TarefasAtribuidas")
                        .HasForeignKey("ResponsavelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Responsavel");
                });

            modelBuilder.Entity("PainelDeTarefas.Models.Funcionarios", b =>
                {
                    b.Navigation("TarefasAtribuidas");
                });
#pragma warning restore 612, 618
        }
    }
}