using System.Xml.Serialization;

namespace CompilerProject.Tokenization;

public static class TokenTableLoader
{
    public static TokenTable LoadTokenTable()
    {
        using var stream = new FileStream($"C:\\School\\Compiler\\CompilerProject\\TokenTableData\\{nameof(TokenTable)}.xml", FileMode.Open);
        var xml = new XmlSerializer(typeof(TokenTable));
        return (TokenTable)xml.Deserialize(stream)!;
    }
}