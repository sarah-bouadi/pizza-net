namespace pizza_net;

public class CommandLineReader : FileReader
{
    public List<string> ReadFileLines(string filePath = "")
    {
        Console.WriteLine("Entrez votre commande :");
        var input = Console.ReadLine();
        return new List<string>() { input };
    }
}