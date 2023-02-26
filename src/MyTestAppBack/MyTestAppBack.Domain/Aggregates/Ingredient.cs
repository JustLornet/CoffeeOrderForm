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
    [Table("dictIngredients")]
    public class Ingredient : BaseEntity, IEntityTypeConfiguration<Ingredient>
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public long IngredientUnitId { get; set; }
        public virtual IngredientUnit IngredientUnit { get; set; } = null!;

        // можно ли данный компонент при желании клиента убрать / добавить
        public bool IsOptional { get; set; } = false;

        [JsonIgnore]
        public List<StandartComposition> StandartCompositions { get; set; } = new();
        [JsonIgnore]
        public List<CustomComposition> CustomCompositions { get; set; } = new();

        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasOne(p => p.IngredientUnit).WithMany(p => p.Ingredients).HasForeignKey(p => p.IngredientUnitId);
            builder.Property(p => p.IngredientUnitId).IsRequired();

            builder.HasData(
                new Ingredient
                {
                    Id = 1,
                    Name = "Эспрессо",
                    IngredientUnitId = 2,
                },
                new Ingredient
                {
                    Id = 2,
                    Name = "Вода",
                    IngredientUnitId = 2,
                },
                new Ingredient
                {
                    Id = 3,
                    Name = "Молоко",
                    IngredientUnitId = 2,
                },
                new Ingredient
                {
                    Id = 4,
                    Name = "Сливки",
                    IngredientUnitId = 2,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 5,
                    Name = "Сахар",
                    IngredientUnitId = 1,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 6,
                    Name = "Взбитые сливки",
                    IngredientUnitId = 2,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 7,
                    Name = "Мороженое",
                    IngredientUnitId = 1,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 8,
                    Name = "Тертый шоколад",
                    IngredientUnitId = 1,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 9,
                    Name = "Лимон",
                    IngredientUnitId = 1,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 10,
                    Name = "Молотый орех",
                    IngredientUnitId = 1,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 11,
                    Name = "Виски",
                    IngredientUnitId = 3,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 12,
                    Name = "Мед",
                    IngredientUnitId = 1,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 13,
                    Name = "Коньяк",
                    IngredientUnitId = 3,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 14,
                    Name = "Ликер",
                    IngredientUnitId = 3,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 15,
                    Name = "Корица",
                    IngredientUnitId = 1,
                    IsOptional = true
                },
                new Ingredient
                {
                    Id = 16,
                    Name = "Вспененное молоко",
                    IngredientUnitId = 2,
                },
                new Ingredient
                {
                    Id = 17,
                    Name = "Карамель",
                    IngredientUnitId = 1,
                    IsOptional = true
                });
        }
    }
}
