using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    [Table("dictCoffeeTypes")]
    public class CoffeeType : BaseEntity, IEntityTypeConfiguration<CoffeeType>
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [JsonIgnore]
        public List<StandartComposition> CoffeeCompositions { get; set; } = new();
        [JsonIgnore]
        public List<CoffeeOrder> CoffeeOrders { get; set; } = new();

        public void Configure(EntityTypeBuilder<CoffeeType> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();

            builder.HasData(
                new CoffeeType
                {
                    Id = 1,
                    Name = "Эспрессо",
                    Description = "Как появился этот вид кофе? Во время Второй мировой войны американские солдаты искали в Италии свой традиционный фильтр-кофе, поэтому итальянские бариста решили разбавить эспрессо, в результате чего получился совершенно новый напиток."
                },
                new CoffeeType
                {
                    Id = 2,
                    Name = "Доппио",
                    Description = "Двойная порция эспрессо"
                },
                new CoffeeType
                {
                    Id = 3,
                    Name = "Лунго",
                    Description = "Эспрессо с увеличенным количеством воды"
                },
                new CoffeeType
                {
                    Id = 4,
                    Name = "Ристретто",
                    Description = "Многие считают, что в ристретто слишком много кофеина, но это заблуждение. Первые несколько секунд заваривания из кофе выделяются эфирные масла, придающие ему насыщенный вкус, а кофеин поступает в напиток позднее. Из-за этого в ристретто обычно даже меньше кофеина, чем в эспрессо."
                },
                new CoffeeType
                {
                    Id = 5,
                    Name = "Американо",
                    Description = "Эспрессо, разбавленный кипятком в пропорции 1:2 или 1:3"
                },
                new CoffeeType
                {
                    Id = 6,
                    Name = "Флэт уайт",
                    Description = "Двойная порция эспрессо, куда добавляют немного горячего молока с пеной"
                },
                new CoffeeType
                {
                    Id = 7,
                    Name = "Макиато",
                    Description = "Слово «макиато» с итальянского переводится как «пятнышко». Такое кофейное темное пятнышко образуется сверху на молочной пене после вливания эспрессо в горячее молоко."
                },
                new CoffeeType
                {
                    Id = 8,
                    Name = "Капучино",
                    Description = "Капучино в переводе с итальянского означает «капуцинский», так как его придумали монахи-капуцины. Основная составляющая капучино - это эспрессо, вторая – молоко."
                },
                new CoffeeType
                {
                    Id = 9,
                    Name = "Латте",
                    Description = "Название итальянского кофе латте в переводе означает «кофе с молоком». Как и следует из названия, основными составляющими этого напитка являются эспрессо и молоко."
                },
                new CoffeeType
                {
                    Id = 10,
                    Name = "Глясе",
                    Description = "Охлаждённый кофе с шариком мороженого. Напиток подают в айриш-бокале с соломинкой"
                },
                new CoffeeType
                {
                    Id = 11,
                    Name = "Раф",
                    Description = "Коктейль (по сути, пена) из эспрессо со сливками и ванильным сахаром, взбитого в капучинаторе"
                },
                new CoffeeType
                {
                    Id = 13,
                    Name = "Мокко",
                    Description = "Во вкусе мокко больше преобладает шоколадно-сливочный аромат, чем кофейный. А температура готового напитка не очень высокая, так как сироп и сливки добавляются в мокко холодными."
                },
                new CoffeeType
                {
                    Id = 14,
                    Name = "Бичерин",
                    Description = "Кофе по-турински, состоящий из 3 слоёв: горячего шоколада на молоке, эспрессо и взбитых сливок. Сверху кофе украшают тёртым горьким шоколадом и мятой"
                });
        }
    }
}
