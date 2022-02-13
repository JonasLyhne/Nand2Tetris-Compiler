namespace CompilerProject.Tokenization.TokenExtractors;

public class KeywordTokenExtractor : ITokenExtractor
{
    private readonly TokenTable tokenTable;

    public KeywordTokenExtractor(TokenTable tokenTable)
    {
        this.tokenTable = tokenTable;
    }
    public bool TryGetToken(ref string input, ref IList<Token> tokens)
    {
        var keyword = TokenExtractorHelper.GetKeywordOrIdentifierString(input, tokenTable);
        if (keyword != null && tokenTable.KeyWords.Contains(keyword))
        {
            tokens.Add(new Token(TokenValueType.Keyword, keyword));
            input = input.Remove(0, keyword.Length);
            return true;
        }

        return false;
    }
}