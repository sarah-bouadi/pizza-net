using System;
using System.Collections.Generic;

namespace PizzeriaApp
{
    public class Program
    {
        private static readonly Dictionary<string, Pizza> AvailablePizzas = new Dictionary<string, Pizza>
        {
            { "Regina", new Pizza("Regina", 8.0m, new Dictionary<string, decimal>
                {
                    { "tomate", 150 },
                    { "mozzarella", 125 },
                    { "fromage râpé", 100 },
                    { "Jambon", 2 },
                    { "champignons frais", 4 },
                    { "huile d'olive", 2 }
                })
            },
            { "4 Saisons", new Pizza("4 Saisons", 9.0m, new Dictionary<string, decimal>
                {
                    { "tomate", 150 },
                    { "mozzarella", 125 },
                    { "Jambon", 2 },
                    { "champignons frais", 100 },
                    { "poivron", 0.5m },
                    { "olives", 1 }
                })
            },
            { "Végétarienne", new Pizza("Végétarienne", 7.5m, new Dictionary<string, decimal>
                {
                    { "tomate", 150 },
                    { "mozzarella", 100 },
                    { "courgette", 0.5m },
                    { "poivron jaune", 1 },
                    { "tomates cerises", 6 },
                    { "olives", 0 }
                })
            }
        };

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Entrez votre commande :");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                string[] orders = input.Split(',');

                var orderDetails = new Dictionary<string, int>();

                foreach (var order in orders)
                {
                    string[] parts = order.Trim().Split(' ');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int quantity))
                    {
                        string pizzaName = parts[1];
                        if (AvailablePizzas.ContainsKey(pizzaName))
                        {
                            if (orderDetails.ContainsKey(pizzaName))
                            {
                                orderDetails[pizzaName] += quantity;
                            }
                            else
                            {
                                orderDetails[pizzaName] = quantity;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Erreur : La pizza '{pizzaName}' n'est pas disponible.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Erreur : Commande invalide : '{order}'.");
                    }
                }

                Console.WriteLine("Facture :");
                decimal totalPrice = 0;

                foreach (var order in orderDetails)
                {
                    string pizzaName = order.Key;
                    int quantity = order.Value;

                    if (AvailablePizzas.TryGetValue(pizzaName, out var pizza))
                    {
                        decimal pizzaPrice = pizza.Price * quantity;
                        totalPrice += pizzaPrice;

                        Console.WriteLine($"{quantity} {pizzaName} : {quantity} * {pizza.Price:C}");
                        foreach (var ingredient in pizza.Ingredients)
                        {
                            Console.WriteLine($"{ingredient.Key} {ingredient.Value}g");
                        }
                    }
                }

                Console.WriteLine($"Prix total : {totalPrice:C}");

                Console.WriteLine("Instructions de préparation :");
                foreach (var order in orderDetails)
                {
                    string pizzaName = order.Key;

                    if (AvailablePizzas.TryGetValue(pizzaName, out var pizza))
                    {
                        Console.WriteLine(pizzaName);
                        Console.WriteLine("Préparer la pâte");
                        foreach (var ingredient in pizza.Ingredients)
                        {
                            Console.WriteLine($"Ajouter {ingredient.Key}");
                        }
                        Console.WriteLine("Cuire la pizza");
                    }
                }
            }
        }
    }
}
