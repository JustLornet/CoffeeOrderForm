using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyTestAppBack.Domain.Aggregates
{
    [Table("dictSyrups")]
    public class Syrup : BaseEntity, IEntityTypeConfiguration<Syrup>
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [JsonIgnore]
        public List<CoffeeOrder> CoffeeOrders { get; set; } = new();

        public void Configure(EntityTypeBuilder<Syrup> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();

            builder.HasData(
                new Syrup
                {
                    Id = 1,
                    Name = "Карамель",
                    Description = "Сироп BARINOFF \"Соленая карамель\" обладает аппетитной густой консистенцией и покоряет мягкими нюансами карамели и ванили"
                },
                new Syrup
                {
                    Id = 2,
                    Name = "Амаретто",
                    Description = "Сироп Амаретто – безалкогольный вариант традиционного миндального ликера, обладающий богатым вкусом, красивым золотистым оттенком"
                },
                new Syrup
                {
                    Id = 3,
                    Name = "Лесной орех",
                    Description = "Сироп «Лесной орех» имеет желтовато-оливковый оттенок, насыщенный аромат фундука и маслянистый, дымчатый вкус натуральных лесных орехов"
                },
                new Syrup
                {
                    Id = 4,
                    Name = "Шоколад",
                    Description = "Шоколодный сироп для кофе - не канон"
                },
                new Syrup
                {
                    Id = 5,
                    Name = "Соленая карамель",
                    Description = "Сироп «Соленая карамель» обладает сильным, глубоким и ярким ароматом солоноватой карамели"
                },
                new Syrup
                {
                    Id = 6,
                    Name = "Кокос",
                    Description = "Кокосовый сироп — это концентрированный сладкий продукт с ароматом знаменитого тропического фрукта, который способен подарить очарование любому напитку"
                });
        }
    }
}
