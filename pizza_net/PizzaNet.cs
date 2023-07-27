namespace pizza_net;

public class PizzaNet
{
    public void launchPizzeria(string filePath, string targetFilePath)
    {
        // Create and Read file content
        var fileReader = FileReaderFactory.CreateFileReader(GetFileType(filePath));
        List<string> orders = fileReader.ReadFileLines(filePath);

        // Create Invoice Output
        IInvoiceOutput invoiceOutput = getInvoiceOutput(fileReader);

        var orderProcessor = new OrderProcessor(PizzaMenu.AvailablePizza);

        orderProcessor.SetInvoiceOutput(invoiceOutput);
            
        while (true)
        {
            foreach (string order in orders)
            {
                Console.WriteLine("###### Order : " + order + " ######");
                ProcessOrder(orderProcessor, order, targetFilePath);
            }
            
            if (!(fileReader is CommandLineReader))
            {
                break;
            }
            // Read new orders for CommandLineReader
            orders = fileReader.ReadFileLines(filePath); 
        }
    }

    public IInvoiceOutput getInvoiceOutput(FileReader fileReader)
    {
        // Create the concrete component based on the file type
        IInvoiceOutput concreteComponent;
        if (fileReader is JsonFileReader)
        {
            concreteComponent = new JsonInvoiceOutput();
        }
        else if (fileReader is XmlFileReader)
        {
            concreteComponent = new XmlInvoiceOutput();
        }
        else if (fileReader is TextFileReader)
        {
            concreteComponent = new TextInvoiceOutput();
        }
        else
        {
            // Default to console output if the file type is not JSON or XML
            concreteComponent = new ConsoleInvoiceOutput();
        }

        // Optionally, apply decorators to the concrete component
        return new InvoiceHeaderDecorator(concreteComponent);
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
    
    public void ProcessOrder(OrderProcessor orderProcessor, string order, string targetFilePath)
    {
        // 3.1 Prise en compte des commandes 
        var orderDetails = orderProcessor.ParseOrders(order);

        // 3.2 Edition d'une facture 
        orderProcessor.EditInvoice(orderDetails, targetFilePath);

        // 3.3. Edition des instructions de préparation
        orderProcessor.EditInstruction(orderDetails);
        
        // 3.6 Extension du programme : Listing des ingrédients utilisés
        orderProcessor.ListUsedIngredient(orderDetails);
    }
}