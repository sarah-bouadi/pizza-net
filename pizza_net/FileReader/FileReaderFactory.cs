namespace pizza_net;

public class FileReaderFactory
{
    public static FileReader CreateFileReader(string fileType)
    {
        return fileType switch
        {
            "TXT" => new TextFileReader(),
            "JSON" => new JsonFileReader(),
            "XML" => new XmlFileReader(),
            _ => new CommandLineReader()
        };
    }
}