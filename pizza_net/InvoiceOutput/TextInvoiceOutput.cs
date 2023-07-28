namespace pizza_net;

public class TextInvoiceOutput : IInvoiceOutput
{
    private Dictionary<string, Pizza> _AvailablePizza = PizzaMenu.AvailablePizza;

    public void GenerateInvoice(Dictionary<string, int> orderDetails, string targetFilePath)
    {
        string invoiceOutput = "";
        decimal totalPrice = 0;
            
        foreach (var order in orderDetails)
        {   
            var pizzaName = order.Key;
            var quantity = order.Value;

            if (_AvailablePizza.TryGetValue(pizzaName, out var pizza))
            {
                var pizzaPrice = pizza.Price * quantity;
                totalPrice += pizzaPrice;
                invoiceOutput += $"{quantity} {pizzaName} : {quantity} * {pizza.Price:C}\n";
                    
                foreach (var ingredient in pizza.Ingredients)
                {
                    invoiceOutput += ingredient.DisplayIngredient() + "\n";
                }
            }
        }
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
            Console.WriteLine("Text invoice has been generated and saved to '" + targetFilePath + "'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}