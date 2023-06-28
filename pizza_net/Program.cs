using System;
using System.Collections.Generic;

namespace pizza_net
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
                // 3.1 Prise en compte des commandes 
                // Get order
                Console.WriteLine("Entrez votre commande :");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;
                
                // Transform command line input to list of order
                string[] orders = input.Split(',');

                var orderDetails = new Dictionary<string, int>();

                // For each order
                foreach (var order in orders)
                {
                    // Treat the format of number of order and its title
                    string[] parts = order.Trim().Split(' ');
                    
                    // Check if the input format is correct
                    if (parts.Length == 2 && int.TryParse(parts[0], out int quantity))
                    {
                        // Get order Title (pizza name)
                        string pizzaName = parts[1];
                        // Check if the pizza is available
                        if (AvailablePizzas.ContainsKey(pizzaName))
                        {
                            // If order already passed, Increment the quantity
                            if (orderDetails.ContainsKey(pizzaName))
                            {
                                orderDetails[pizzaName] += quantity;
                            }
                            // Add it in order list
                            else
                            {
                                orderDetails[pizzaName] = quantity;
                            }
                        }
                        // Print error if the pizza is not available
                        else
                        {
                            Console.WriteLine($"Erreur : La pizza '{pizzaName}' n'est pas disponible.");
                        }
                    }
                    // Print error if the input format is incorrect
                    else
                    {
                        Console.WriteLine($"Erreur : Commande invalide : '{order}'.");
                    }
                }

                // 3.2 Edition d'une facture 
                    
                Console.WriteLine("\nFacture :");
                decimal totalPrice = 0;
                
                // For each order of our list
                foreach (var order in orderDetails)
                {   
                    // Get the order title and its quantity
                    string pizzaName = order.Key;
                    int quantity = order.Value;

                    // Try to get the pizza order specification
                    if (AvailablePizzas.TryGetValue(pizzaName, out var pizza))
                    {
                        // Calculate and print the cost of a pizza
                        decimal pizzaPrice = pizza.Price * quantity;
                        totalPrice += pizzaPrice;
                        Console.WriteLine($"{quantity} {pizzaName} : {quantity} * {pizza.Price:C}");
                        
                        // Print the specification of the 
                        foreach (var ingredient in pizza.Ingredients)
                        {
                            Console.WriteLine($"{ingredient.Key} {ingredient.Value}g");
                        }
                    }
                }
                // Print the total cost
                Console.WriteLine($"Prix total : {totalPrice:C}");

                
                // 3.3. Edition des instructions de préparation
                
                Console.WriteLine("\nInstructions de préparation :");
                
                // For each order print how to prepare the pizza
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
