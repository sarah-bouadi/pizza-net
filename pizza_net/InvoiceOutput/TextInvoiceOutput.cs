namespace pizza_net;

public class TextInvoiceOutput : IInvoiceOutput
{
    private Dictionary<string, Pizza> _AvailablePizza = PizzaMenu.AvailablePizza;

    public void GenerateInvoice(Dictionary<string, int> orderDetails, string targetFilePath)
    {
        string invoiceOutput = "";
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
                invoiceOutput += $"{quantity} {pizzaName} : {quantity} * {pizza.Price:C}\n";
                    
                // Print the specification of the 
                foreach (var ingredient in pizza.Ingredients)
                {
                    invoiceOutput += ingredient.DisplayIngredient() + "\n";
                }
            }
        }
        // Print the total cost
        invoiceOutput += $"Prix total : {totalPrice:C}\n\n";

        try
        {
            if (File.Exists(targetFilePath))
            {
                using (StreamWriter writer = new StreamWriter(targetFilePath, append: true))
                {
                    writer.WriteLine(invoiceOutput);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(targetFilePath))
                {
                    writer.WriteLine(invoiceOutput);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}