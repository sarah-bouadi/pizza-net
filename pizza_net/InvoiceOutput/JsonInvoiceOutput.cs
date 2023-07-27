using System.Text.Json;

namespace pizza_net
{
    public class JsonInvoiceOutput : IInvoiceOutput
    {
        private Dictionary<string, Pizza> _AvailablePizza = PizzaMenu.AvailablePizza;

        public class Invoice
        {
            public List<Order> Orders { get; set; } = new List<Order>();
        }

        public class Order
        {
            public List<Product> Products { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public class Product
        {
            public Info Info { get; set; }
            public List<Ingredient> Ingredients { get; set; }
        }

        public class Info
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
        }

        public class Ingredient
        {
            public string Name { get; set; }
            public decimal Quantity { get; set; }
            public string QuantityType { get; set; }
        }

        public void GenerateInvoice(Dictionary<string, int> orderDetails, string targetFilePath)
        {
            try
            {
                Invoice invoice = new Invoice();

                // Check if the target JSON file exists
                if (File.Exists(targetFilePath))
                {
                    // Read existing JSON data from the file
                    using (StreamReader reader = new StreamReader(targetFilePath))
                    {
                        string jsonData = reader.ReadToEnd();

                        // Deserialize the existing JSON data to the invoice object
                        invoice = JsonSerializer.Deserialize<Invoice>(jsonData);
                    }
                }

                // Create a list to store the orders with product information
                List<Product> products = new List<Product>();
                decimal totalPrice = 0;

                // Iterate through the orderDetails dictionary to create the orders
                foreach (var order in orderDetails)
                {
                    string pizzaName = order.Key;
                    int quantity = order.Value;

                    if (_AvailablePizza.TryGetValue(pizzaName, out var pizza))
                    {
                        var pizzaPrice = pizza.Price * quantity;
                        totalPrice += pizzaPrice;

                        // Create the product info
                        Product product = new Product
                        {
                            Info = new Info
                            {
                                Name = pizzaName,
                                Quantity = quantity
                            },
                            Ingredients = new List<Ingredient>()
                        };

                        // Add the pizza's ingredients to the product info
                        foreach (var ingredient in pizza.Ingredients)
                        {
                            product.Ingredients.Add(new Ingredient
                            {
                                Name = ingredient._name,
                                Quantity = ingredient._IngredientQuantity.Value,
                                QuantityType = GetQuantityType(ingredient._IngredientQuantity)
                            });
                        }

                        products.Add(product);
                    }
                }

                // Add the new order to the existing list
                invoice.Orders.Add(new Order
                {
                    Products = products,
                    TotalPrice = totalPrice
                });

                var serializerOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                // Serialize the invoice object to JSON format
                string jsonDataOutput = JsonSerializer.Serialize(invoice, serializerOptions);

                // Write the JSON output to the target file
                using (StreamWriter writer = new StreamWriter(targetFilePath))
                {
                    writer.Write(jsonDataOutput);
                }

                Console.WriteLine("JSON invoice has been generated and saved to '" + targetFilePath + "'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private string GetQuantityType(IngredientQuantity ingredientQuantity)
        {
            return ingredientQuantity.getQuantityType();
        }
    }
}
