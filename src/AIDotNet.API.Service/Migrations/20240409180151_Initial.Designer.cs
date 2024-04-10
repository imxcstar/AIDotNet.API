﻿// <auto-generated />
using System;
using AIDotNet.API.Service.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AIDotNet.API.Service.Migrations
{
    [DbContext(typeof(TokenApiDbContext))]
    [Migration("20240409180151_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("AIDotNet.API.Service.Domain.ChatChannel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Creator")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Disable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Models")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Modifier")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Other")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quota")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RemainQuota")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ResponseTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Creator");

                    b.HasIndex("Name");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("AIDotNet.API.Service.Domain.ChatLogger", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChannelId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChannelName")
                        .HasColumnType("TEXT");

                    b.Property<int>("CompletionTokens")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Creator")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Modifier")
                        .HasColumnType("TEXT");

                    b.Property<int>("PromptTokens")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quota")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TokenName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Creator");

                    b.HasIndex("ModelName");

                    b.HasIndex("TokenName");

                    b.HasIndex("UserName");

                    b.ToTable("Loggers");
                });

            modelBuilder.Entity("AIDotNet.API.Service.Domain.Token", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("AccessedTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Creator")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Disabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ExpiredTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(42)
                        .HasColumnType("TEXT");

                    b.Property<string>("Modifier")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("RemainQuota")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("UnlimitedExpired")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("UnlimitedQuota")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<long>("UsedQuota")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Creator");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("AIDotNet.API.Service.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<long>("ConsumeToken")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Creator")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Modifier")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHas")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("RequestCount")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ResidualCredit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "5dac91d8-0ed4-4727-a534-e6f9c52434e3",
                            ConsumeToken = 0L,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "239573049@qq.com",
                            IsDelete = false,
                            Password = "76c359997c2ec8ac655aeb7a7917304b",
                            PasswordHas = "37747d67812549bcae99032d020efd3f",
                            RequestCount = 0L,
                            ResidualCredit = 0L,
                            Role = "admin",
                            UserName = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}