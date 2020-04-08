﻿// <auto-generated />
using System;
using DatabaseManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseManager.Migrations
{
    [DbContext(typeof(LudoDbContext))]
    partial class LudoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DatabaseManager.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("GameEnded")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastCheckpointTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Turn")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("games");
                });

            modelBuilder.Entity("DatabaseManager.Pawn", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("position")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("PlayerId");

                    b.ToTable("pawns");
                });

            modelBuilder.Entity("DatabaseManager.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<bool>("Won")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("players");
                });

            modelBuilder.Entity("DatabaseManager.Pawn", b =>
                {
                    b.HasOne("DatabaseManager.Player", "Player")
                        .WithMany("Pawns")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("DatabaseManager.Player", b =>
                {
                    b.HasOne("DatabaseManager.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId");
                });
#pragma warning restore 612, 618
        }
    }
}
