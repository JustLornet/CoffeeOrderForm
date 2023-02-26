using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyTestAppBack.Domain.Aggregates
{
    public class CoffeeOrder : BaseEntity, IEntityTypeConfiguration<CoffeeOrder>
    {
        [JsonIgnore]
        public long CoffeeTypeId { get; set; }

        public virtual CoffeeType CoffeeType { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string CustomerName { get; set; } = null!;

        public string? Comments { get; set; }

        // дата-время совершения заказа
        public DateTime? OrderCreationDateTime { get; set; }
        
        // дата-время, на которое сделан заказ
        [Required]
        public DateTime OrderExecutionDateTime { get; set; }

        [JsonIgnore]
        public long? SyrupId { get; set; }

        public virtual Syrup? Syrup { get; set; }

        public List<CustomComposition> CustomCompositions { get; set; } = new();

        public void Configure(EntityTypeBuilder<CoffeeOrder> builder)
        {
            builder.HasOne(p => p.CoffeeType).WithMany(p => p.CoffeeOrders).HasForeignKey(p => p.CoffeeTypeId);
            builder.HasOne(p => p.Syrup).WithMany(p => p.CoffeeOrders).HasForeignKey(p => p.SyrupId);

            builder.Property(p => p.OrderCreationDateTime).IsRequired();
            builder.Property(p => p.CoffeeTypeId).IsRequired();
        }
    }
}
