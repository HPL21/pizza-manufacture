using API.Models;
using System;

namespace API.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDBContext db)
        {
            if (!db.Users.Any())
            {
                var users = new List<User>
            {
                new User
                {
                    Id = "1",
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                },
                new User
                {
                    Id = "2",
                    UserName = "user@example.com",
                    Email = "user@example.com",
                    EmailConfirmed = true
                }
            };
                db.Users.AddRange(users);
                db.SaveChanges();

                // Ingredients
                var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Cheese", Price = 5.0, Weight = 100, Calories = 300 },
                new Ingredient { Name = "Tomato", Price = 2.0, Weight = 50, Calories = 20 }
            };
                db.Ingredients.AddRange(ingredients);
                db.SaveChanges();

                // Pizzas
                var pizzas = new List<Pizza>
            {
                new Pizza { Name = "Margherita", Weight = 200, Calories = 500, Price = 15 },
                new Pizza { Name = "Cheese Delight", Weight = 250, Calories = 600, Price = 18 }
            };
                db.Pizzas.AddRange(pizzas);
                db.SaveChanges();

                // PizzaIngredients
                var pizzaIngredients = new List<PizzaIngredient>
            {
                new PizzaIngredient { PizzaId = pizzas[0].Id, IngredientId = ingredients[0].Id, IngredientAmount = 100 },
                new PizzaIngredient { PizzaId = pizzas[0].Id, IngredientId = ingredients[1].Id, IngredientAmount = 50 },
                new PizzaIngredient { PizzaId = pizzas[1].Id, IngredientId = ingredients[0].Id, IngredientAmount = 150 },
                new PizzaIngredient { PizzaId = pizzas[1].Id, IngredientId = ingredients[1].Id, IngredientAmount = 50 }
            };
                db.PizzaIngredients.AddRange(pizzaIngredients);
                db.SaveChanges();

                // Orders
                var orders = new List<Order>
            {
                new Order
                {
                    UserId = "1",
                    PlacedAt = DateTime.UtcNow,
                    CompletedAt = DateTime.UtcNow.AddHours(1),
                    TotalPrice = 15,
                    TotalWeight = 200,
                    TotalCalories = 500,
                    RecipientName = "Alice",
                    RecipientPhone = "123456789",
                    PaymentMethod = PaymentMethod.CASH
                },
                new Order
                {
                    UserId = "1",
                    PlacedAt = DateTime.UtcNow,
                    CompletedAt = DateTime.UtcNow.AddHours(1),
                    TotalPrice = 18,
                    TotalWeight = 250,
                    TotalCalories = 600,
                    RecipientName = "Bob",
                    RecipientPhone = "987654321",
                    PaymentMethod = PaymentMethod.CARD
                }
            };
                db.Orders.AddRange(orders);
                db.SaveChanges();

                // OrderDetails
                var orderDetails = new List<OrderDetail>
            {
                new OrderDetail { OrderId = orders[0].Id, PizzaId = pizzas[0].Id },
                new OrderDetail { OrderId = orders[1].Id, PizzaId = pizzas[1].Id }
            };
                db.OrderDetails.AddRange(orderDetails);
                db.SaveChanges();
            }
        }
    }

}
