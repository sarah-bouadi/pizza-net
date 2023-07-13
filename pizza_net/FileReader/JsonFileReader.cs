using System.Text.Json;

namespace pizza_net;

public class JsonFileReader : FileReader
{
    public List<string> ReadFileLines(string filePath)
    {
        string json = File.ReadAllText(filePath);
        List<string> lines = new List<string>();

        JsonDocument doc = JsonDocument.Parse(json);

        foreach (JsonElement element in doc.RootElement.EnumerateArray())
        {
            string line = string.Empty;

            foreach (JsonProperty property in element.EnumerateObject())
            {
                string key = property.Name;
                string value = property.Value.ToString();

                line += $"{value} {key}, ";
            }

            line = line.TrimEnd(',', ' ');

            lines.Add(line);
        }
        return lines;
    }
}