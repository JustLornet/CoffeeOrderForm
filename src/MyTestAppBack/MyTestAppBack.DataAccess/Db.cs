using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MyTestAppBack.Domain.Aggregates;

namespace MyTestAppBack.DataAccess
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {
            // TODO: включить, когда всё будет готово
            // отключено, чтобы не было ошибок при миграциях
            //Database.EnsureCreated();
        }

        public DbSet<CoffeeType> CoffeeTypes => Set<CoffeeType>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<Syrup> Syrups => Set<Syrup>();
        public DbSet<StandartComposition> StandartCompositions => Set<StandartComposition>();
        public DbSet<CustomComposition> CustomCompositions => Set<CustomComposition>();
        public DbSet<CoffeeOrder> CoffeeOrders => Set<CoffeeOrder>();
        public DbSet<IngredientUnit> IngredientUnits => Set<IngredientUnit>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //connection string
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<CoffeeType>(new CoffeeType());
            modelBuilder.ApplyConfiguration<Ingredient>(new Ingredient());
            modelBuilder.ApplyConfiguration<Syrup>(new Syrup());
            modelBuilder.ApplyConfiguration<StandartComposition>(new StandartComposition());
            modelBuilder.ApplyConfiguration<CustomComposition>(new CustomComposition());
            modelBuilder.ApplyConfiguration<CoffeeOrder>(new CoffeeOrder());
            modelBuilder.ApplyConfiguration<IngredientUnit>(new IngredientUnit());

            modelBuilder.Entity<CoffeeOrder>().HasMany(p => p.CustomCompositions).WithMany(p => p.CoffeeOrders).UsingEntity(j => j.ToTable("OrderCustomCompositions"));
        }
    }
}
