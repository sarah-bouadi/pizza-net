namespace pizza_net;

public class PizzaNet
{
    public void launchPizzeria(string filePath)
    {
        var fileReader = FileReaderFactory.CreateFileReader(GetFileType(filePath));
            
        List<string> orders = fileReader.ReadFileLines(filePath);

        var orderProcessor = new OrderProcessor(PizzaMenu.AvailablePizza);
            
        while (true)
        {
            foreach (string order in orders)
            {
                Console.WriteLine("###### Order : " + order + " ######");
                ProcessOrder(orderProcessor, order);
            }
            
            if (!(fileReader is CommandLineReader))
            {
                break;
            }
            // Read new orders for CommandLineReader
            orders = fileReader.ReadFileLines(filePath); 
        }
    }

    public string GetFileType(string filename)
    {
        Dictionary<string, string> fileTypes = new Dictionary<string, string>()
        {
            { ".txt", "TXT" },
            { ".json", "JSON" },
            { ".xml", "XML" }
        };

        string extension = Path.GetExtension(filename);

        if (!string.IsNullOrEmpty(extension) && fileTypes.ContainsKey(extension))
        {
            return fileTypes[extension];
        }

        return "";
    }
    
    public void ProcessOrder(OrderProcessor orderProcessor, string order)
    {
        // 3.1 Prise en compte des commandes 
        var orderDetails = orderProcessor.ParseOrders(order);

        // 3.2 Edition d'une facture 
        orderProcessor.EditInvoice(orderDetails);

        // 3.3. Edition des instructions de préparation
        orderProcessor.EditInstruction(orderDetails);

        // 3.6 Extension du programme : Listing des ingrédients utilisés
        orderProcessor.ListUsedIngredient(orderDetails);
    }
}