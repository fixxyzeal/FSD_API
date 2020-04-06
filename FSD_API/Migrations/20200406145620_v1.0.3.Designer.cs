﻿// <auto-generated />
using System;
using DAL_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FSD_API.Migrations
{
    [DbContext(typeof(SystemContext))]
    [Migration("20200406145620_v1.0.3")]
    partial class v103
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BO.Models.Action", b =>
                {
                    b.Property<Guid>("ActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ActionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Platform")
                        .HasColumnType("text");

                    b.Property<Guid>("UpdateBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserDisplayName")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<string>("UserId")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("ActionId");

                    b.ToTable("Action");
                });

            modelBuilder.Entity("BO.Models.PhoneRanking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CPU")
                        .HasColumnType("integer");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeviceName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int>("GPU")
                        .HasColumnType("integer");

                    b.Property<int>("MEM")
                        .HasColumnType("integer");

                    b.Property<string>("OS")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Ram")
                        .HasColumnType("integer");

                    b.Property<int>("Ranking")
                        .HasColumnType("integer");

                    b.Property<int>("StorageSize")
                        .HasColumnType("integer");

                    b.Property<int>("TotalScore")
                        .HasColumnType("integer");

                    b.Property<int>("UX")
                        .HasColumnType("integer");

                    b.Property<Guid>("UpdateBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Ranking", "DeviceName", "OS", "Ram", "StorageSize");

                    b.ToTable("PhoneRanking");
                });

            modelBuilder.Entity("BO.Models.PhoneRankingHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CPU")
                        .HasColumnType("integer");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeviceName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int>("GPU")
                        .HasColumnType("integer");

                    b.Property<int>("MEM")
                        .HasColumnType("integer");

                    b.Property<string>("OS")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Ram")
                        .HasColumnType("integer");

                    b.Property<int>("Ranking")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RankingDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("StorageSize")
                        .HasColumnType("integer");

                    b.Property<int>("TotalScore")
                        .HasColumnType("integer");

                    b.Property<int>("UX")
                        .HasColumnType("integer");

                    b.Property<Guid>("UpdateBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Ranking", "DeviceName", "OS", "Ram", "StorageSize", "RankingDate");

                    b.ToTable("PhoneRankingHistorie");
                });

            modelBuilder.Entity("BO.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("UpdateBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("UserId");

                    b.HasIndex("UserName", "Email");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
