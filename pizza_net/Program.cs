using System.Text;

namespace pizza_net
{
    public class Program
    {
        public static void Main()
        {
            // string filePath = "";
            // string filePath = "/Users/soulte92/Documents/SCHOOL/ESGI-4AL/S2/DESIGN PATTERN/PROJECTS/pizza-net/pizza_net/testPizzaMenu.txt";
            // string filePath = "/Users/soulte92/Documents/SCHOOL/ESGI-4AL/S2/DESIGN PATTERN/PROJECTS/pizza-net/pizza_net/testPizzaMenu.json";
            string filePath = "/Users/soulte92/Documents/SCHOOL/ESGI-4AL/S2/DESIGN PATTERN/PROJECTS/pizza-net/pizza_net/testPizzaMenu.xml";

            var pizzaNet = new PizzaNet();
            pizzaNet.launchPizzeria(filePath);
        }
    }
}

