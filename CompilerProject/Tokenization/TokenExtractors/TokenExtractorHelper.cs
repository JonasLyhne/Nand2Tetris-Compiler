namespace CompilerProject.Tokenization.TokenExtractors;

public static class TokenExtractorHelper
{
    public static string? GetKeywordOrIdentifierString(string value, TokenTable tokenTable)
    {
        var indexEnd = value.IndexOf(' ', 0);
        var linePart = indexEnd >= 0 ? value[..indexEnd] : value;

        var end = linePart.IndexOfAny(tokenTable.Symbols.ToArray());
        var line = end >= 0 ? linePart[..end] : linePart;

        return string.Join("",line.Where(l => char.IsLetter(l) || l == '.').ToArray());
    }
}