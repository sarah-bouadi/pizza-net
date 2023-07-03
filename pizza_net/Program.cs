using System.Text;

namespace pizza_net
{
    public class Program
    {
        private static readonly Dictionary<string, Pizza> AvailablePizzas = new Dictionary<string, Pizza>
        {
            { "Regina", 
                new Pizza("Regina", 8.0m, new List<Ingredient>
                {  
                    new Ingredient("tomate", new IngredientQuantityWeight(150)),
                    new Ingredient("mozzarella", new IngredientQuantityWeight(125)),
                    new Ingredient("fromage râpé", new IngredientQuantityWeight(100)),
                    new Ingredient("Jambon", new IngredientQuantitySlice(2)),
                    new Ingredient("champignons frais", new IngredientQuantityPiece(4)),
                    new Ingredient("huile d'olive", new IngredientQuantitySpoon(2))
                })
            },
            { "4 Saisons", 
                new Pizza("4 Saisons", 9.0m,
                    new List<Ingredient>
                    {  
                        new Ingredient("tomate", new IngredientQuantityWeight(150)),
                        new Ingredient("mozzarella", new IngredientQuantityWeight(125)),
                        new Ingredient("Jambon", new IngredientQuantitySlice(2)),
                        new Ingredient("champignons frais", new IngredientQuantityWeight(100)),
                        new Ingredient("poivron", new IngredientQuantityPiece(0.5m)),
                        new Ingredient("olives", new IngredientQuantityWrist(1))
                    })
            },
            { "Végétarienne", 
                new Pizza("Végétarienne", 7.5m, 
                    new List<Ingredient>
                    {  
                        new Ingredient("tomate", new IngredientQuantityWeight(150)),
                        new Ingredient("mozzarella", new IngredientQuantityWeight(100)),
                        new Ingredient("courgette", new IngredientQuantityPiece(0.5m)),
                        new Ingredient("poivron jaune", new IngredientQuantityPiece(1)),
                        new Ingredient("tomates cerises", new IngredientQuantityPiece(6)),
                        new Ingredient("olives", new IngredientQuantityWrist(1))
                    })
            }
        };

        public static Dictionary<string, int> ProcessOrder()
        {
            // Get order
            Console.WriteLine("Entrez votre commande :");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return null;
            
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
                    var pizzaName = parts[1];
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

            return orderDetails;
        }

        public static void EditInvoice(Dictionary<string, int> orderDetails)
        {
            Console.WriteLine("\nFacture :");
            decimal totalPrice = 0;
                
            // For each order of our list
            foreach (var order in orderDetails)
            {   
                // Get the order title and its quantity
                var pizzaName = order.Key;
                var quantity = order.Value;

                // Try to get the pizza order specification
                if (AvailablePizzas.TryGetValue(pizzaName, out var pizza))
                {
                    // Calculate and print the cost of a pizza
                    var pizzaPrice = pizza.Price * quantity;
                    totalPrice += pizzaPrice;
                    Console.WriteLine($"{quantity} {pizzaName} : {quantity} * {pizza.Price:C}");
                        
                    // Print the specification of the 
                    foreach (var ingredient in pizza.Ingredients)
                    {
                        ingredient.DisplayIngredient();
                    }
                }
            }
            // Print the total cost
            Console.WriteLine($"Prix total : {totalPrice:C}");
        }

        public static void EditInstruction(Dictionary<string, int> orderDetails)
        {
            Console.WriteLine("\nInstructions de préparation :");
                
            // For each order print how to prepare the pizza
            foreach (var order in orderDetails)
            {
                string pizzaName = order.Key;

                if (AvailablePizzas.TryGetValue(pizzaName, out var pizza))
                {
                    Console.WriteLine(pizzaName);
                    Console.WriteLine("Préparer la pâte");
                        
                    pizza.displayAddingIngredients();
                        
                    Console.WriteLine("Cuire la pizza");
                }
            }
        }

        public static void ListUsedIngredient(Dictionary<string, int> orderDetails)
        {
            // Get unique ingredients
            List<string> uniqueIngredientsNames = new List<string>();
            foreach (var order in orderDetails)
            {
                string pizzaName = order.Key;
                if (! AvailablePizzas.TryGetValue(pizzaName, out var pizza))
                {
                    // Error message
                }

                foreach (var ingredient in pizza.Ingredients)
                {
                    uniqueIngredientsNames.Add(ingredient._name);
                }
            }
            uniqueIngredientsNames = uniqueIngredientsNames.Distinct().ToList();

            StringBuilder displayListingIngredients = new StringBuilder();
            foreach (var uniqueIngredientName in uniqueIngredientsNames)
            {
                // Total quantity of the ingredient in all pizzas of all orders combined
                decimal totalQuantityValue = 0;
                foreach (var order in orderDetails)
                {
                    // Number of orders of the pizza
                    decimal orderQuantity = order.Value;
                    var pizza = AvailablePizzas[order.Key];

                    // Quantity of the ingredient in the pizza
                    decimal ingredientQuantityInPizza = 0;
                    foreach (var ingredient in pizza.Ingredients)
                    {
                        if (uniqueIngredientName.Equals(ingredient._name))
                        {
                            ingredientQuantityInPizza = ingredient._IngredientQuantity.Value;
                            displayListingIngredients.AppendLine($"- {pizza.Name} : {ingredientQuantityInPizza * orderQuantity}");
                            totalQuantityValue += (ingredientQuantityInPizza * orderQuantity);
                        }
                    }
                }
                Console.WriteLine($"{uniqueIngredientName} : {totalQuantityValue}");
                Console.WriteLine(displayListingIngredients.ToString());
                displayListingIngredients = new StringBuilder();
            }
        }
        
        public static void Main()
        {
            // 3.5 Comportement attendu du programme
            while (true)
            {
                // 3.1 Prise en compte des commandes 
                var orderDetails = ProcessOrder();

                // 3.2 Edition d'une facture 
                EditInvoice(orderDetails);
                
                // 3.3. Edition des instructions de préparation
                EditInstruction(orderDetails);
                
                // 3.6 Extension du programme : Listing des ingrédients utilisés
                ListUsedIngredient(orderDetails);
            }
        }
    }
}
