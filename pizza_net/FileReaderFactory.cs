// namespace pizza_net;
//
// public class FileReaderFactory : FileReader
// {
//     public List<string> ReadFileLines(string filePath)
//     {
//         throw new NotImplementedException();
//     }
//
//     private FileReaderFactory(string fileType, string filePath = "")
//     {
//         return createFileReader(fileType, filePath);
//     }
//
//     public static FileReader createFileReader(string fileType, string filePath = "")
//     {
//         if (fileType.Equals("TXT"))
//         {
//             return new XmlFileReader();
//         }
//         else if (fileType.Equals("JSON"))
//         {
//             return new TextFileReader();
//         }
//         else if (fileType.Equals("XML"))
//         {
//             return new JsonFileReader();
//         }
//         return new CommandLineReader();
//     }
// }