using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
        }
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

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
