namespace pizza_net
{
    public class Program
    {
        public static void Main()
        {
            // string relativeFilePath = @"";
            // string relativeTargetFilePath = @"";
            
            // string relativeFilePath = @"..testPizzaMenu.txt";
            // string relativeTargetFilePath = @"..invoice_testPizzaMenu.txt";
            
            // string relativeFilePath = @"..testPizzaMenu.json";
            // string relativeTargetFilePath = @"..invoice_testPizzaMenu.json";
            
            string relativeFilePath = @"..testPizzaMenu.xml";
            string relativeTargetFilePath = @"..invoice_testPizzaMenu.xml";
            
            // Get Absolute path
            string baseDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(baseDirectory, relativeFilePath).Replace("bin/Debug/net7.0/..", "");
            
            var pizzaNet = new PizzaNet();
            pizzaNet.launchPizzeria(filePath, relativeTargetFilePath);
        }
    }
}