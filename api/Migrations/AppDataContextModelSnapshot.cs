﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.21");

            modelBuilder.Entity("api.Models.FichaRpg", b =>
                {
                    b.Property<int>("FichaRpgId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Alinhamento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Antecedência")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataAtualizado")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Deletado")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomeDoJogador")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Nível")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PontosDeExperiência")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Raça")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FichaRpgId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("FichasRpg");
                });

            modelBuilder.Entity("api.Models.Habilidade", b =>
                {
                    b.Property<int>("HabilidadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FichaRpgId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Pontos")
                        .HasColumnType("INTEGER");

                    b.HasKey("HabilidadeId");

                    b.HasIndex("FichaRpgId");

                    b.ToTable("Habilidades");
                });

            modelBuilder.Entity("api.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DataAtualizado")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Deletado")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("api.Models.FichaRpg", b =>
                {
                    b.HasOne("api.Models.Usuario", "Usuario")
                        .WithMany("FichasRpg")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("api.Models.Habilidade", b =>
                {
                    b.HasOne("api.Models.FichaRpg", "FichaRpg")
                        .WithMany("Habilidades")
                        .HasForeignKey("FichaRpgId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FichaRpg");
                });

            modelBuilder.Entity("api.Models.FichaRpg", b =>
                {
                    b.Navigation("Habilidades");
                });

            modelBuilder.Entity("api.Models.Usuario", b =>
                {
                    b.Navigation("FichasRpg");
                });
#pragma warning restore 612, 618
        }
    }
}
