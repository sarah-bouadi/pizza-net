using System.Text;

namespace pizza_net;

public class OrderProcessor
{
    private Dictionary<string, Pizza> _AvailablePizza;
    private IInvoiceOutput _invoiceOutput;

    public OrderProcessor(Dictionary<string, Pizza> availablePizza)
    {
        _AvailablePizza = availablePizza;
    }
    
    public void SetInvoiceOutput(IInvoiceOutput invoiceOutput)
    {
        _invoiceOutput = invoiceOutput;
    }

    public Dictionary<string, int> ParseOrders(string inputOrders)
    {
        // Get orders
        if (string.IsNullOrWhiteSpace(inputOrders))
            return null;
        
        // Transform command line input to list of order
        string[] orders = inputOrders.Split(',');

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
                if (_AvailablePizza.ContainsKey(pizzaName))
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


    public void EditInvoice(Dictionary<string, int> orderDetails, string targetFilePath)
    {
        Console.WriteLine("-- Edition de la commande --");

        Console.WriteLine("\nFacture :");

        // Generate Invoice
        _invoiceOutput.GenerateInvoice(orderDetails, targetFilePath);
    }

    public void EditInstruction(Dictionary<string, int> orderDetails)
    {
        Console.WriteLine("-- Edition des intructions de préparation la commande --");

        Console.WriteLine("\nInstructions de préparation :");
            
        // For each order print how to prepare the pizza
        foreach (var order in orderDetails)
        {
            string pizzaName = order.Key;

            if (_AvailablePizza.TryGetValue(pizzaName, out var pizza))
            {
                Console.WriteLine(pizzaName);
                Console.WriteLine("Préparer la pâte");
                    
                pizza.DisplayAddingIngredients();
                    
                Console.WriteLine("Cuire la pizza\n");
            }
        }
        Console.WriteLine();
    }

    public void ListUsedIngredient(Dictionary<string, int> orderDetails)
    {
        Console.WriteLine("-- Liste des ingrédients utilisées --");

        // Get unique ingredients
        List<string> uniqueIngredientsNames = new List<string>();
        foreach (var order in orderDetails)
        {
            string pizzaName = order.Key;
            if (! _AvailablePizza.TryGetValue(pizzaName, out var pizza))
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
                var pizza = _AvailablePizza[order.Key];

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