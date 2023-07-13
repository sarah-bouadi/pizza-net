using System.Text;

namespace pizza_net
{
    public class Program
    {
        public static void OpenPizzeria(OrderProcessor orderProcessor, string order)
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

        public static void Main()
        {
            // string filePath = "";
            
            // string filePath = "/Users/soulte92/Documents/SCHOOL/ESGI-4AL/S2/DESIGN PATTERN/PROJECTS/pizza-net/pizza_net/testPizzaMenu.txt";
            // var fileReader = FileReaderFactory.CreateFileReader("TXT");
            //
            // string filePath = "/Users/soulte92/Documents/SCHOOL/ESGI-4AL/S2/DESIGN PATTERN/PROJECTS/pizza-net/pizza_net/testPizzaMenu.json";
            // var fileReader = FileReaderFactory.CreateFileReader("JSON");
            
            string filePath = "/Users/soulte92/Documents/SCHOOL/ESGI-4AL/S2/DESIGN PATTERN/PROJECTS/pizza-net/pizza_net/testPizzaMenu.xml";
            var fileReader = FileReaderFactory.CreateFileReader("XML");
            
            List<string> orders = fileReader.ReadFileLines(filePath);

            var orderProcessor = new OrderProcessor(PizzaMenu.AvailablePizza);
            
            while (true)
            {
                foreach (string order in orders)
                {
                    Console.WriteLine("###### Order : " + order + " ######");
                    OpenPizzeria(orderProcessor, order);
                }
            
                if (!(fileReader is CommandLineReader))
                {
                    break;
                }
                // Read new orders for CommandLineReader
                orders = fileReader.ReadFileLines(filePath); 
            }
        }
    }
}

