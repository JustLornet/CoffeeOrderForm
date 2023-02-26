using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyTestAppBack.Domain.Aggregates
{
    public class CustomComposition : IEntityTypeConfiguration<CustomComposition>
    {
        [JsonIgnore]
        [Key]
        public long Id { get; set; }

        [Required]
        public long IngredientId { get; set; }
        [JsonIgnore]
        public virtual Ingredient Ingredient { get; set; } = null!;

        // кол-во каждого ингридиента, выраженное в мг или мл
        [Required]
        public long Value { get; set; }

        [JsonIgnore]
        public List<CoffeeOrder> CoffeeOrders { get; set; } = null!;

        public void Configure(EntityTypeBuilder<CustomComposition> builder)
        {
            builder.HasOne(p => p.Ingredient).WithMany(p => p.CustomCompositions).HasForeignKey(p => p.IngredientId);
            // чтобы таблица бесконечно не расширялась и быстрее находить повторяющиеся составы
            builder.HasIndex(p => new { p.IngredientId, p.Value }).IsUnique();

            builder.HasData(
                new CustomComposition
                {
                    Id = 1,
                    IngredientId = 1,
                    Value = 1,
                },
                new CustomComposition
                {
                    Id = 2,
                    IngredientId = 2,
                    Value = 2,
                });
        }
    }
}
