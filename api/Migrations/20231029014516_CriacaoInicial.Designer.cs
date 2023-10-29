﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20231029014516_CriacaoInicial")]
    partial class CriacaoInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.24");

            modelBuilder.Entity("api.Models.Usuario", b =>
                {
                    b.Property<long>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id_usuario");

                    b.Property<DateTime?>("DataAtualizado")
                        .HasColumnType("TEXT")
                        .HasColumnName("data_atualizado");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT")
                        .HasColumnName("data_criacao");

                    b.Property<bool?>("Deletado")
                        .HasColumnType("INTEGER")
                        .HasColumnName("deletado");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("nome");

                    b.HasKey("IdUsuario")
                        .HasName("pk_usuarios");

                    b.ToTable("usuarios", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}