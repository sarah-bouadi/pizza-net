using System.Text;

namespace pizza_net;

public class Order
{
    public Dictionary<string, int> ProcessOrder()
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
                if (PizzaMenu.AvailablePizza.ContainsKey(pizzaName))
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

    public void EditInvoice(Dictionary<string, int> orderDetails)
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
            if (PizzaMenu.AvailablePizza.TryGetValue(pizzaName, out var pizza))
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

    public void EditInstruction(Dictionary<string, int> orderDetails)
    {
        Console.WriteLine("\nInstructions de préparation :");
            
        // For each order print how to prepare the pizza
        foreach (var order in orderDetails)
        {
            string pizzaName = order.Key;

            if (PizzaMenu.AvailablePizza.TryGetValue(pizzaName, out var pizza))
            {
                Console.WriteLine(pizzaName);
                Console.WriteLine("Préparer la pâte");
                    
                pizza.displayAddingIngredients();
                    
                Console.WriteLine("Cuire la pizza\n");
            }
        }
        Console.WriteLine();
    }

    public void ListUsedIngredient(Dictionary<string, int> orderDetails)
    {
        // Get unique ingredients
        List<string> uniqueIngredientsNames = new List<string>();
        foreach (var order in orderDetails)
        {
            string pizzaName = order.Key;
            if (! PizzaMenu.AvailablePizza.TryGetValue(pizzaName, out var pizza))
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
                var pizza = PizzaMenu.AvailablePizza[order.Key];

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
}