using MyTestAppBack.Domain.Aggregates;

namespace MyTestAppBack.Aggregates.Dto
{
    public class SelectionsDto
    {
        public List<CoffeeType> CoffeeTypes { get; set; } = null!;

        public List<Syrup> Syrups { get; set; } = null!;

        public List<Ingredient> Ingredients { get; set; } = null!;
    }
}
