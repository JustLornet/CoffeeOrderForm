using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MyTestAppBack.Domain.Aggregates
{
    [Table("dictStandartCompositions")]
    public class StandartComposition : IEntityTypeConfiguration<StandartComposition>
    {
        [Required]
        public long CoffeeTypeId { get; set; }
        public virtual CoffeeType CoffeeType { get; set; } = null!;

        [Required]
        public long IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; } = null!;

        // кол-во каждого ингридиента, выраженное в мг или мл
        [Required]
        public long Value { get; set; }

        public void Configure(EntityTypeBuilder<StandartComposition> builder)
        {
            builder.HasKey(p => new { p.CoffeeTypeId, p.IngredientId });
            builder.HasOne(p => p.CoffeeType).WithMany(p => p.CoffeeCompositions).HasForeignKey(p => p.CoffeeTypeId);
            builder.HasOne(p => p.Ingredient).WithMany(p => p.StandartCompositions).HasForeignKey(p => p.IngredientId);

            builder.HasData(
                new StandartComposition
                {
                    CoffeeTypeId = 9,
                    IngredientId = 1,
                    Value = 50
                },
                new StandartComposition
                {
                    CoffeeTypeId = 9,
                    IngredientId = 3,
                    Value = 150
                },
                new StandartComposition
                {
                    CoffeeTypeId = 9,
                    IngredientId = 16,
                    Value = 50
                },
                new StandartComposition
                {
                    CoffeeTypeId = 8,
                    IngredientId = 1,
                    Value = 50
                },
                new StandartComposition
                {
                    CoffeeTypeId = 8,
                    IngredientId = 3,
                    Value = 50
                },
                new StandartComposition
                {
                    CoffeeTypeId = 8,
                    IngredientId = 16,
                    Value = 50
                },
                new StandartComposition
                {
                    CoffeeTypeId = 1,
                    IngredientId = 1,
                    Value = 50
                },
                new StandartComposition
                {
                    CoffeeTypeId = 5,
                    IngredientId = 1,
                    Value = 60
                },
                new StandartComposition
                {
                    CoffeeTypeId = 5,
                    IngredientId = 2,
                    Value = 120
                });
        }
    }
}
