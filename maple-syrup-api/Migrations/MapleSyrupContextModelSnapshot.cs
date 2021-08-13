﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using maple_syrup_api.Context;

namespace maple_syrup_api.Migrations
{
    [DbContext(typeof(MapleSyrupContext))]
    partial class MapleSyrupContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("maple_syrup_api.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EventStatus")
                        .HasColumnType("integer");

                    b.Property<int>("EventType")
                        .HasColumnType("integer");

                    b.Property<string>("FightName")
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int?>("RequirementId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("maple_syrup_api.Models.EventRequirement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("AllowBlueMage")
                        .HasColumnType("boolean");

                    b.Property<List<int>>("ClassRequirement")
                        .HasColumnType("integer[]");

                    b.Property<bool>("DPSRequiredByType")
                        .HasColumnType("boolean");

                    b.Property<List<int>>("DPSTypeRequirement")
                        .HasColumnType("integer[]");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int>("MinILevel")
                        .HasColumnType("integer");

                    b.Property<int>("MinLevel")
                        .HasColumnType("integer");

                    b.Property<bool>("OnePerJob")
                        .HasColumnType("boolean");

                    b.Property<List<int>>("PerJobRequirement")
                        .HasColumnType("integer[]");

                    b.Property<int>("PlayerCount")
                        .HasColumnType("integer");

                    b.Property<int>("PlayerLimit")
                        .HasColumnType("integer");

                    b.Property<bool>("PreciseJob")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.ToTable("EventRequirements");
                });

            modelBuilder.Entity("maple_syrup_api.Models.GuildConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("GuildId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GuildConfigs");
                });

            modelBuilder.Entity("maple_syrup_api.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Class")
                        .HasColumnType("integer");

                    b.Property<int>("DPSType")
                        .HasColumnType("integer");

                    b.Property<int>("EventRequirementId")
                        .HasColumnType("integer");

                    b.Property<int>("Job")
                        .HasColumnType("integer");

                    b.Property<string>("PlayerName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventRequirementId");

                    b.HasIndex("UserId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("maple_syrup_api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("DiscordId")
                        .HasColumnType("bigint");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("maple_syrup_api.Models.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("maple_syrup_api.Models.EventRequirement", b =>
                {
                    b.HasOne("maple_syrup_api.Models.Event", "Event")
                        .WithOne("Requirement")
                        .HasForeignKey("maple_syrup_api.Models.EventRequirement", "EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("maple_syrup_api.Models.Player", b =>
                {
                    b.HasOne("maple_syrup_api.Models.EventRequirement", "EventRequirement")
                        .WithMany("Players")
                        .HasForeignKey("EventRequirementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("maple_syrup_api.Models.User", "User")
                        .WithMany("UserPlayerList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventRequirement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("maple_syrup_api.Models.UserToken", b =>
                {
                    b.HasOne("maple_syrup_api.Models.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("maple_syrup_api.Models.Event", b =>
                {
                    b.Navigation("Requirement");
                });

            modelBuilder.Entity("maple_syrup_api.Models.EventRequirement", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("maple_syrup_api.Models.User", b =>
                {
                    b.Navigation("UserPlayerList");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
