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
    [Table("dictIngredientUnits")]
    public class IngredientUnit : BaseEntity, IEntityTypeConfiguration<IngredientUnit>
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [JsonIgnore]
        public List<Ingredient> Ingredients { get; set; } = new();

        public void Configure(EntityTypeBuilder<IngredientUnit> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();

            builder.HasData(
                new IngredientUnit
                {
                    Id = 1,
                    Name = "мг",
                    Description = "милиграмм",
                },
                new IngredientUnit
                {
                    Id = 2,
                    Name = "мл",
                    Description = "милилитр",
                },
                new IngredientUnit
                {
                    Id = 3,
                    Name = "л",
                    Description = "литр",
                });
        }
    }
}
