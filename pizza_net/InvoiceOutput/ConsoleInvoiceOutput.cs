namespace pizza_net;

public class ConsoleInvoiceOutput : IInvoiceOutput
{
    private Dictionary<string, Pizza> _AvailablePizza = PizzaMenu.AvailablePizza;

    public void GenerateInvoice(Dictionary<string, int> orderDetails, string targetFilePath)
    {
        decimal totalPrice = 0;
            
        // For each order of our list
        foreach (var order in orderDetails)
        {   
            // Get the order title and its quantity
            var pizzaName = order.Key;
            var quantity = order.Value;

            // Try to get the pizza order specification
            if (_AvailablePizza.TryGetValue(pizzaName, out var pizza))
            {
                // Calculate and print the cost of a pizza
                var pizzaPrice = pizza.Price * quantity;
                totalPrice += pizzaPrice;
                Console.WriteLine($"{quantity} {pizzaName} : {quantity} * {pizza.Price:C}");
                    
                // Print the specification of the 
                foreach (var ingredient in pizza.Ingredients)
                {
                    Console.WriteLine(ingredient.DisplayIngredient());
                }
            }
        }
        // Print the total cost
        Console.WriteLine($"Prix total : {totalPrice:C}");
    }
}