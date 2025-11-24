using API.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace API.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDBContext db)
        {
            if (!db.Users.Any())
            {
                var hasher = new PasswordHasher<User>();
                var users = new List<User>
                {
                    new User
                {
                    Id = "1",
                    UserName = "Admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    
                },
                    new User
                    {
                        Id = "2",
                        UserName = "user@example.com",
                        Email = "user@example.com",
                        EmailConfirmed = true
                    }
                };
                users[0].PasswordHash = hasher.HashPassword(users[0], "Admin123!");
                db.Users.AddRange(users);
                db.SaveChanges();

                var userRoles = new List<IdentityUserRole<string>>
                {
                    new IdentityUserRole<string>
                    {
                        UserId = "1",
                        RoleId = "1"
                    }
                };

                db.UserRoles.AddRange(userRoles);
                db.SaveChanges();

                // Ingredients
                var ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = "Ciasto", Price = 15, Weight = 200, Calories = 325 },
                    new Ingredient { Name = "Sos pomidorowy", Price = 5, Weight = 80, Calories = 30 },
                    new Ingredient { Name = "Mozzarella", Price = 10, Weight = 80, Calories = 280 },
                    new Ingredient { Name = "Oliwa", Price = 3, Weight = 10, Calories = 880 },
                    new Ingredient { Name = "Prosciutto crudo", Price = 7, Weight = 40, Calories = 225 },
                    new Ingredient { Name = "Gorgonzola", Price = 7, Weight = 40, Calories = 350 },
                    new Ingredient { Name = "Parmezan", Price = 3, Weight = 15, Calories = 430 },
                    new Ingredient { Name = "Spianata piccante", Price = 7, Weight = 40, Calories = 370 },
                    new Ingredient { Name = "Salami milano", Price = 7, Weight = 40, Calories = 370 },
                    new Ingredient { Name = "Czosnek + oregano (marinara)", Price = 5, Weight = 15, Calories = 50 },
                    new Ingredient { Name = "Gruszka", Price = 5, Weight = 30, Calories = 55 },
                    new Ingredient { Name = "Orzechy włoskie", Price = 5, Weight = 10, Calories = 650 },
                    new Ingredient { Name = "Czerwona cebula", Price = 5, Weight = 30, Calories = 40 },
                    new Ingredient { Name = "Czerwona papryka", Price = 5, Weight = 50, Calories = 40 },
                    new Ingredient { Name = "Tuńczyk", Price = 6, Weight = 40, Calories = 60 },
                    new Ingredient { Name = "Pieczarki", Price = 5, Weight = 40, Calories = 30 },
                    new Ingredient { Name = "Oliwki", Price = 5, Weight = 20, Calories = 115 },
                    new Ingredient { Name = "Biały sos", Price = 8, Weight = 70, Calories = 200 },
                    new Ingredient { Name = "Pomidorki koktajlowe", Price = 5, Weight = 50, Calories = 20 },
                    new Ingredient { Name = "Rukola", Price = 5, Weight = 20, Calories = 25 },
                    new Ingredient { Name = "Nduja", Price = 8, Weight = 30, Calories = 600 },
                    new Ingredient { Name = "Miód", Price = 4, Weight = 20, Calories = 300 },
                    new Ingredient { Name = "Mascarpone", Price = 6, Weight = 50, Calories = 400 }
                };
                db.Ingredients.AddRange(ingredients);
                db.SaveChanges();

                // Pizzas
                var pizzas = new List<Pizza>
                {
                    new Pizza { Name = "Margherita", Weight = 0, Calories = 0, Price = 0 },
                    new Pizza { Name = "Marinara", Weight = 0, Calories = 0, Price = 0 },
                    new Pizza { Name = "Mare", Weight = 0, Calories = 0, Price = 0 },
                    new Pizza { Name = "Dolce", Weight = 0, Calories = 0, Price = 0 },
                    new Pizza { Name = "Santiago", Weight = 0, Calories = 0, Price = 0 },
                    new Pizza { Name = "Ruccoli", Weight = 0, Calories = 0, Price = 0 },
                    new Pizza { Name = "Speziato", Weight = 0, Calories = 0, Price = 0 },
                    new Pizza { Name = "Ordinario", Weight = 0, Calories = 0, Price = 0 }
                };
                db.Pizzas.AddRange(pizzas);
                db.SaveChanges();

                // PizzaIngredients
                var map = new Dictionary<string, string[]>
                {
                    ["Margherita"] = new[] { "Ciasto", "Sos pomidorowy", "Mozzarella", "Oliwa" },
                    ["Marinara"] = new[] { "Ciasto", "Sos pomidorowy", "Czosnek + oregano (marinara)", "Oliwa" },
                    ["Mare"] = new[] { "Ciasto", "Sos pomidorowy", "Mozzarella", "Tuńczyk", "Czerwona cebula" },
                    ["Dolce"] = new[] { "Ciasto", "Biały sos", "Mozzarella", "Gorgonzola", "Gruszka", "Orzechy włoskie", "Miód" },
                    ["Santiago"] = new[] { "Ciasto", "Sos pomidorowy", "Mozzarella", "Spianata piccante", "Czerwona cebula", "Czerwona papryka" },
                    ["Ruccoli"] = new[] { "Ciasto", "Sos pomidorowy", "Mozzarella", "Prosciutto crudo", "Pomidorki koktajlowe", "Rukola" },
                    ["Speziato"] = new[] { "Ciasto", "Sos pomidorowy", "Mozzarella", "Nduja", "Czerwona cebula", "Mascarpone" },
                    ["Ordinario"] = new[] { "Ciasto", "Sos pomidorowy", "Salami milano", "Oliwki", "Pieczarki", "Oliwa" }
                };

                var pizzaIngredients = new List<PizzaIngredient>();

                foreach (var pizza in pizzas)
                {
                    var ingredientNames = map[pizza.Name];

                    foreach (var ing in ingredientNames)
                    {
                        var ingredient = ingredients.First(i => i.Name == ing);

                        pizzaIngredients.Add(new PizzaIngredient
                        {
                            PizzaId = pizza.Id,
                            IngredientId = ingredient.Id,
                            IngredientAmount = 1 
                        });
                    }
                }

                db.PizzaIngredients.AddRange(pizzaIngredients);
                db.SaveChanges();

                foreach (var pizza in pizzas)
                {
                    var assignedIngredients = pizzaIngredients
                        .Where(pi => pi.PizzaId == pizza.Id)
                        .Select(pi => ingredients.First(i => i.Id == pi.IngredientId))
                        .ToList();

                    pizza.Price = assignedIngredients.Sum(i => i.Price);
                    pizza.Weight = assignedIngredients.Sum(i => i.Weight);
                    pizza.Calories = assignedIngredients.Sum(i => i.Calories);
                }

                db.Pizzas.UpdateRange(pizzas);
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
                    PaymentMethod = PaymentMethod.CASH,
                    Status = OrderStatus.COMPLETED
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
                    PaymentMethod = PaymentMethod.CARD,
                    Status = OrderStatus.IN_PROGRESS
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
