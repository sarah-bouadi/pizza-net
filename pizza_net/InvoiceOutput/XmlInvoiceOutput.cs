using System.Xml;

namespace pizza_net;

public class XmlInvoiceOutput : IInvoiceOutput
{
    private Dictionary<string, Pizza> _AvailablePizza = PizzaMenu.AvailablePizza;

    public void GenerateInvoice(Dictionary<string, int> orderDetails, string targetFilePath)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement ordersElement;
            // If the XML file already exists, load it
            if (File.Exists(targetFilePath))
            {
                xmlDoc.Load(targetFilePath);
                ordersElement = xmlDoc.DocumentElement;
            }
            else
            {
                // If the XML file doesn't exist, create a new XML document
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDeclaration);

                ordersElement = xmlDoc.CreateElement("orders");
                xmlDoc.AppendChild(ordersElement);
            }

            
            decimal totalPrice = 0;

            XmlElement orderElement = xmlDoc.CreateElement("order");
            
            foreach (var order in orderDetails)
            {
                Console.WriteLine(order.ToString());

                string pizzaName = order.Key;
                int quantity = order.Value;

                if (_AvailablePizza.TryGetValue(pizzaName, out var pizza))
                {
                    var pizzaPrice = pizza.Price * quantity;
                    totalPrice += pizzaPrice;
                    
                    XmlElement productElement = xmlDoc.CreateElement("product");
                    orderElement.AppendChild(productElement);
                    
                    XmlElement infoElement = xmlDoc.CreateElement("info");
                    productElement.AppendChild(infoElement);

                    XmlElement nameElement = xmlDoc.CreateElement("name");
                    nameElement.InnerText = pizzaName;
                    infoElement.AppendChild(nameElement);

                    XmlElement quantityElement = xmlDoc.CreateElement("quantity");
                    quantityElement.InnerText = quantity.ToString();
                    infoElement.AppendChild(quantityElement);

                    XmlElement ingredientsElement = xmlDoc.CreateElement("ingredients");
                    productElement.AppendChild(ingredientsElement);

                    foreach (var ingredient in pizza.Ingredients)
                    {
                        XmlElement ingredientElement = xmlDoc.CreateElement("ingredient");
                        ingredientsElement.AppendChild(ingredientElement);

                        XmlElement ingredientNameElement = xmlDoc.CreateElement("name");
                        ingredientNameElement.InnerText = ingredient._name;
                        ingredientElement.AppendChild(ingredientNameElement);

                        XmlElement ingredientQuantityElement = xmlDoc.CreateElement("quantity");
                        ingredientQuantityElement.InnerText = ingredient._IngredientQuantity.Value.ToString();
                        ingredientElement.AppendChild(ingredientQuantityElement);

                        XmlElement ingredientQuantityTypeElement = xmlDoc.CreateElement("quantityType");
                        ingredientQuantityTypeElement.InnerText = GetQuantityType(ingredient._IngredientQuantity);
                        ingredientElement.AppendChild(ingredientQuantityTypeElement);
                    }
                }
            }
            // Add the total price to the existing ordersElement
            XmlElement totalPriceElement = xmlDoc.CreateElement("totalPrice");
            totalPriceElement.InnerText = totalPrice.ToString();
            orderElement.AppendChild(totalPriceElement);
            ordersElement.AppendChild(orderElement);
            
            // Save the updated XML document to the file
            xmlDoc.Save(targetFilePath);

            Console.WriteLine("XML invoice has been generated and saved to '"+ targetFilePath + "'.");
            
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