using System.Xml;
using System.Xml.Serialization;
using CompilerProject.Tokenization;

namespace CompilerProject.Data;

public static class StreamIO
{
    
    public static IList<string> ReadFile(string path)
    {
        using var stream = new FileStream(path, FileMode.Open);
        using var reader = new StreamReader(stream);
        var result = new List<string>();
        while (!reader.EndOfStream)
        {
            result.Add(reader.ReadLine() ?? string.Empty);
        }
        return result;
    }

    public static XmlDocument WriteToXml(IReadOnlyCollection<Token> tokens)
    {
        var doc = new XmlDocument();
        var node = doc.CreateElement("Jack");
        doc.AppendChild(node);

        foreach (var token in tokens)
        {
            var root = doc.CreateElement(token.Type.ToString());
            root.InnerText = token.Value;
            node.AppendChild(root);
        }

        return doc;
    }
    
    
    public static void SaveXmlDocument(XmlDocument document, string path)
    {
        document.Save($@"{path}");
    }
}