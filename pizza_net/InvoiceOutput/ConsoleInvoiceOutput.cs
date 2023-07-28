namespace pizza_net;

public class ConsoleInvoiceOutput : IInvoiceOutput
{
    private Dictionary<string, Pizza> _AvailablePizza = PizzaMenu.AvailablePizza;

    public void GenerateInvoice(Dictionary<string, int> orderDetails, string targetFilePath)
    {
        decimal totalPrice = 0;
            
        foreach (var order in orderDetails)
        {   
            var pizzaName = order.Key;
            var quantity = order.Value;

            if (_AvailablePizza.TryGetValue(pizzaName, out var pizza))
            {
                var pizzaPrice = pizza.Price * quantity;
                totalPrice += pizzaPrice;
                Console.WriteLine($"{quantity} {pizzaName} : {quantity} * {pizza.Price:C}");
                    
                foreach (var ingredient in pizza.Ingredients)
                {
                    Console.WriteLine(ingredient.DisplayIngredient());
                }
            }
        }
        Console.WriteLine($"Prix total : {totalPrice:C}");
    }
}