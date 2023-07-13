namespace pizza_net;

public interface FileReader
{
    List<String> ReadFileLines(string filePath);
}