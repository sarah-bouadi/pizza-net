namespace pizza_net;

public class TextFileReader : FileReader
{
    public List<string> ReadFileLines(string filePath)
    {
        List<string> lines = new List<string>();
        
        try
        {
            lines = File.ReadAllLines(filePath).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while reading the file: " + ex.Message);
        }
        
        return lines;
    }
}