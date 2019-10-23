﻿// <auto-generated />
using System;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20191023151231_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Models.BuyOffer", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<int>("Amount")
                        .HasColumnName("amount");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsValid")
                        .HasColumnName("is_valid");

                    b.Property<decimal>("MaxPrice")
                        .HasColumnName("max_price")
                        .HasColumnType("numeric(10, 4)");

                    b.Property<int>("ResourceId")
                        .HasColumnName("resource_id");

                    b.Property<int>("StartAmount")
                        .HasColumnName("start_amount");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("Buy_Offers");
                });

            modelBuilder.Entity("Data.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Data.Models.Configuration", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.Property<int>("Value")
                        .HasColumnName("value");

                    b.HasKey("Name");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("Data.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<int>("Amount")
                        .HasColumnName("amount");

                    b.Property<int>("CompId")
                        .HasColumnName("comp_id");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("CompId");

                    b.HasIndex("UserId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Data.Models.SellOffer", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<int>("Amount")
                        .HasColumnName("amount");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsValid")
                        .HasColumnName("is_valid");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("numeric(10, 4)");

                    b.Property<int>("ResourceId")
                        .HasColumnName("resource_id");

                    b.Property<int>("StartAmount")
                        .HasColumnName("start_amount");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("Sell_Offers");
                });

            modelBuilder.Entity("Data.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<int>("Amount")
                        .HasColumnName("amount");

                    b.Property<int>("BuyOfferId")
                        .HasColumnName("buy_offer_id");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("numeric(10, 4)");

                    b.Property<int>("SellOfferId")
                        .HasColumnName("sell_offer_id");

                    b.HasKey("Id");

                    b.HasIndex("BuyOfferId");

                    b.HasIndex("SellOfferId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<decimal>("Cash")
                        .HasColumnName("cash")
                        .HasColumnType("numeric(10, 2)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Models.BuyOffer", b =>
                {
                    b.HasOne("Data.Models.Resource", "Resource")
                        .WithMany("BuyOffers")
                        .HasForeignKey("ResourceId")
                        .HasConstraintName("FK__Buy_Offer__resou__31EC6D26");
                });

            modelBuilder.Entity("Data.Models.Resource", b =>
                {
                    b.HasOne("Data.Models.Company", "Comp")
                        .WithMany("Resources")
                        .HasForeignKey("CompId")
                        .HasConstraintName("FK__Resources__comp___300424B4");

                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Resources")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Resources__user___2F10007B");
                });

            modelBuilder.Entity("Data.Models.SellOffer", b =>
                {
                    b.HasOne("Data.Models.Resource", "Resource")
                        .WithMany("SellOffers")
                        .HasForeignKey("ResourceId")
                        .HasConstraintName("FK__Sell_Offe__resou__30F848ED");
                });

            modelBuilder.Entity("Data.Models.Transaction", b =>
                {
                    b.HasOne("Data.Models.BuyOffer", "BuyOffer")
                        .WithMany("Transactions")
                        .HasForeignKey("BuyOfferId")
                        .HasConstraintName("FK__Transacti__buy_o__33D4B598");

                    b.HasOne("Data.Models.SellOffer", "SellOffer")
                        .WithMany("Transactions")
                        .HasForeignKey("SellOfferId")
                        .HasConstraintName("FK__Transacti__sell___32E0915F");
                });
#pragma warning restore 612, 618
        }
    }
}
