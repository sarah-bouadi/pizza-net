using System.Xml;

namespace pizza_net;

public class XmlFileReader : FileReader
{
    public List<string> ReadFileLines(string filePath)
    {
        string xml = File.ReadAllText(filePath);
        List<string> lines = new List<string>();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);

        XmlNodeList orderNodes = xmlDoc.SelectNodes("//order");

        foreach (XmlNode orderNode in orderNodes)
        {
            List<string> products = new List<string>();

            XmlNodeList productNodes = orderNode.SelectNodes(".//product");
            foreach (XmlNode productNode in productNodes)
            {
                string name = productNode.SelectSingleNode("name")?.InnerText;
                string quantity = productNode.SelectSingleNode("quantity")?.InnerText;

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(quantity))
                {
                    string product = $"{quantity} {name}";
                    products.Add(product);
                }
            }

            string line = string.Join(", ", products);
            lines.Add(line);
        }

        return lines;
    }
}