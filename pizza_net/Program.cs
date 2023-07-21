namespace pizza_net
{
    public class Program
    {
        public static void Main()
        {
            // string relativeFilePath = @"";
            // string relativeFilePath = @"..testPizzaMenu.txt";
            // string relativeFilePath = @"..testPizzaMenu.json";
            string relativeFilePath = @"..testPizzaMenu.xml";

            // Get Absolute path
            string baseDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(baseDirectory, relativeFilePath).Replace("bin/Debug/net7.0/..", "");
            
            var pizzaNet = new PizzaNet();
            pizzaNet.launchPizzeria(filePath);
        }
    }
}