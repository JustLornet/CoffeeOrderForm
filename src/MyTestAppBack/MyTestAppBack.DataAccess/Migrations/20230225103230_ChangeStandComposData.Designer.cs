﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyTestAppBack.DataAccess;

#nullable disable

namespace MyTestAppBack.DataAccess.Migrations
{
    [DbContext(typeof(Db))]
    [Migration("20230225103230_ChangeStandComposData")]
    partial class ChangeStandComposData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("CoffeeOrderCustomComposition", b =>
                {
                    b.Property<long>("CoffeeOrdersId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("CustomCompositionsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CoffeeOrdersId", "CustomCompositionsId");

                    b.HasIndex("CustomCompositionsId");

                    b.ToTable("OrderCustomCompositions", (string)null);
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.CoffeeOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CoffeeTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("OrderDateTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderExecutionDateTime")
                        .HasColumnType("TEXT");

                    b.Property<long?>("SyrupId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CoffeeTypeId");

                    b.HasIndex("SyrupId");

                    b.ToTable("CoffeeOrders");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.CoffeeType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("dictCoffeeTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "Как появился этот вид кофе? Во время Второй мировой войны американские солдаты искали в Италии свой традиционный фильтр-кофе, поэтому итальянские бариста решили разбавить эспрессо, в результате чего получился совершенно новый напиток.",
                            Name = "Эспрессо"
                        },
                        new
                        {
                            Id = 2L,
                            Description = "Двойная порция эспрессо",
                            Name = "Доппио"
                        },
                        new
                        {
                            Id = 3L,
                            Description = "Эспрессо с увеличенным количеством воды",
                            Name = "Лунго"
                        },
                        new
                        {
                            Id = 4L,
                            Description = "Многие считают, что в ристретто слишком много кофеина, но это заблуждение. Первые несколько секунд заваривания из кофе выделяются эфирные масла, придающие ему насыщенный вкус, а кофеин поступает в напиток позднее. Из-за этого в ристретто обычно даже меньше кофеина, чем в эспрессо.",
                            Name = "Ристретто"
                        },
                        new
                        {
                            Id = 5L,
                            Description = "Эспрессо, разбавленный кипятком в пропорции 1:2 или 1:3",
                            Name = "Американо"
                        },
                        new
                        {
                            Id = 6L,
                            Description = "Двойная порция эспрессо, куда добавляют немного горячего молока с пеной",
                            Name = "Флэт уайт"
                        },
                        new
                        {
                            Id = 7L,
                            Description = "Слово «макиато» с итальянского переводится как «пятнышко». Такое кофейное темное пятнышко образуется сверху на молочной пене после вливания эспрессо в горячее молоко.",
                            Name = "Макиато"
                        },
                        new
                        {
                            Id = 8L,
                            Description = "Капучино в переводе с итальянского означает «капуцинский», так как его придумали монахи-капуцины. Основная составляющая капучино - это эспрессо, вторая – молоко.",
                            Name = "Капучино"
                        },
                        new
                        {
                            Id = 9L,
                            Description = "Название итальянского кофе латте в переводе означает «кофе с молоком». Как и следует из названия, основными составляющими этого напитка являются эспрессо и молоко.",
                            Name = "Латте"
                        },
                        new
                        {
                            Id = 10L,
                            Description = "Охлаждённый кофе с шариком мороженого. Напиток подают в айриш-бокале с соломинкой",
                            Name = "Глясе"
                        },
                        new
                        {
                            Id = 11L,
                            Description = "Коктейль (по сути, пена) из эспрессо со сливками и ванильным сахаром, взбитого в капучинаторе",
                            Name = "Раф"
                        },
                        new
                        {
                            Id = 13L,
                            Description = "Во вкусе мокко больше преобладает шоколадно-сливочный аромат, чем кофейный. А температура готового напитка не очень высокая, так как сироп и сливки добавляются в мокко холодными.",
                            Name = "Мокко"
                        },
                        new
                        {
                            Id = 14L,
                            Description = "Кофе по-турински, состоящий из 3 слоёв: горячего шоколада на молоке, эспрессо и взбитых сливок. Сверху кофе украшают тёртым горьким шоколадом и мятой",
                            Name = "Бичерин"
                        });
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.CustomComposition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("IngredientId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId", "Value")
                        .IsUnique();

                    b.ToTable("CustomCompositions");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.Ingredient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("IngredientUnitId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOptional")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IngredientUnitId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("dictIngredients");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IngredientUnitId = 2L,
                            IsOptional = false,
                            Name = "Эспрессо"
                        },
                        new
                        {
                            Id = 2L,
                            IngredientUnitId = 2L,
                            IsOptional = false,
                            Name = "Вода"
                        },
                        new
                        {
                            Id = 3L,
                            IngredientUnitId = 2L,
                            IsOptional = false,
                            Name = "Молоко"
                        },
                        new
                        {
                            Id = 4L,
                            IngredientUnitId = 2L,
                            IsOptional = true,
                            Name = "Сливки"
                        },
                        new
                        {
                            Id = 5L,
                            IngredientUnitId = 1L,
                            IsOptional = true,
                            Name = "Сахар"
                        },
                        new
                        {
                            Id = 6L,
                            IngredientUnitId = 2L,
                            IsOptional = true,
                            Name = "Взбитые сливки"
                        },
                        new
                        {
                            Id = 7L,
                            IngredientUnitId = 1L,
                            IsOptional = true,
                            Name = "Мороженое"
                        },
                        new
                        {
                            Id = 8L,
                            IngredientUnitId = 1L,
                            IsOptional = true,
                            Name = "Тертый шоколад"
                        },
                        new
                        {
                            Id = 9L,
                            IngredientUnitId = 1L,
                            IsOptional = true,
                            Name = "Лимон"
                        },
                        new
                        {
                            Id = 10L,
                            IngredientUnitId = 1L,
                            IsOptional = true,
                            Name = "Молотый орех"
                        },
                        new
                        {
                            Id = 11L,
                            IngredientUnitId = 3L,
                            IsOptional = true,
                            Name = "Виски"
                        },
                        new
                        {
                            Id = 12L,
                            IngredientUnitId = 1L,
                            IsOptional = true,
                            Name = "Мед"
                        },
                        new
                        {
                            Id = 13L,
                            IngredientUnitId = 3L,
                            IsOptional = true,
                            Name = "Коньяк"
                        },
                        new
                        {
                            Id = 14L,
                            IngredientUnitId = 3L,
                            IsOptional = true,
                            Name = "Ликер"
                        },
                        new
                        {
                            Id = 15L,
                            IngredientUnitId = 1L,
                            IsOptional = true,
                            Name = "Корица"
                        },
                        new
                        {
                            Id = 16L,
                            IngredientUnitId = 2L,
                            IsOptional = false,
                            Name = "Вспененное молоко"
                        },
                        new
                        {
                            Id = 17L,
                            IngredientUnitId = 1L,
                            IsOptional = true,
                            Name = "Карамель"
                        });
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.IngredientUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("dictIngredientUnits");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "милиграмм",
                            Name = "мг"
                        },
                        new
                        {
                            Id = 2L,
                            Description = "милилитр",
                            Name = "мл"
                        },
                        new
                        {
                            Id = 3L,
                            Description = "литр",
                            Name = "л"
                        });
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.StandartComposition", b =>
                {
                    b.Property<long>("CoffeeTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("IngredientId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("CoffeeTypeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("dictStandartCompositions");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.Syrup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("dictSyrups");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "Сироп BARINOFF \"Соленая карамель\" обладает аппетитной густой консистенцией и покоряет мягкими нюансами карамели и ванили",
                            Name = "Карамель"
                        },
                        new
                        {
                            Id = 2L,
                            Description = "Сироп Амаретто – безалкогольный вариант традиционного миндального ликера, обладающий богатым вкусом, красивым золотистым оттенком",
                            Name = "Амаретто"
                        },
                        new
                        {
                            Id = 3L,
                            Description = "Сироп «Лесной орех» имеет желтовато-оливковый оттенок, насыщенный аромат фундука и маслянистый, дымчатый вкус натуральных лесных орехов",
                            Name = "Лесной орех"
                        },
                        new
                        {
                            Id = 4L,
                            Description = "Шоколодный сироп для кофе - не канон",
                            Name = "Шоколад"
                        },
                        new
                        {
                            Id = 5L,
                            Description = "Сироп «Соленая карамель» обладает сильным, глубоким и ярким ароматом солоноватой карамели",
                            Name = "Соленая карамель"
                        },
                        new
                        {
                            Id = 6L,
                            Description = "Мммм (^^)",
                            Name = "Кокос"
                        });
                });

            modelBuilder.Entity("CoffeeOrderCustomComposition", b =>
                {
                    b.HasOne("MyTestAppBack.Domain.Aggregates.CoffeeOrder", null)
                        .WithMany()
                        .HasForeignKey("CoffeeOrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyTestAppBack.Domain.Aggregates.CustomComposition", null)
                        .WithMany()
                        .HasForeignKey("CustomCompositionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.CoffeeOrder", b =>
                {
                    b.HasOne("MyTestAppBack.Domain.Aggregates.CoffeeType", "CoffeeType")
                        .WithMany("CoffeeOrders")
                        .HasForeignKey("CoffeeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyTestAppBack.Domain.Aggregates.Syrup", "Syrup")
                        .WithMany("CoffeeOrders")
                        .HasForeignKey("SyrupId");

                    b.Navigation("CoffeeType");

                    b.Navigation("Syrup");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.CustomComposition", b =>
                {
                    b.HasOne("MyTestAppBack.Domain.Aggregates.Ingredient", "Ingredient")
                        .WithMany("CustomCompositions")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.Ingredient", b =>
                {
                    b.HasOne("MyTestAppBack.Domain.Aggregates.IngredientUnit", "IngredientUnit")
                        .WithMany("Ingredients")
                        .HasForeignKey("IngredientUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IngredientUnit");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.StandartComposition", b =>
                {
                    b.HasOne("MyTestAppBack.Domain.Aggregates.CoffeeType", "CoffeeType")
                        .WithMany("CoffeeCompositions")
                        .HasForeignKey("CoffeeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyTestAppBack.Domain.Aggregates.Ingredient", "Ingredient")
                        .WithMany("StandartCompositions")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoffeeType");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.CoffeeType", b =>
                {
                    b.Navigation("CoffeeCompositions");

                    b.Navigation("CoffeeOrders");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.Ingredient", b =>
                {
                    b.Navigation("CustomCompositions");

                    b.Navigation("StandartCompositions");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.IngredientUnit", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("MyTestAppBack.Domain.Aggregates.Syrup", b =>
                {
                    b.Navigation("CoffeeOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
