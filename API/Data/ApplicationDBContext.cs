using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<PizzaIngredient> PizzaIngredients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PizzaIngredient>()
                .HasKey(pi => new { pi.PizzaId, pi.IngredientId });

            modelBuilder.Entity<PizzaIngredient>()
                .HasOne(pi => pi.Pizza)
                .WithMany(p => p.PizzaIngredients)
                .HasForeignKey(pi => pi.PizzaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PizzaIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.PizzaIngredients)
                .HasForeignKey(pi => pi.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(pi => new { pi.PizzaId, pi.OrderId });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(pi => pi.Pizza)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(pi => pi.PizzaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(pi => pi.Order)
                .WithMany(i => i.OrderDetails)
                .HasForeignKey(pi => pi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
