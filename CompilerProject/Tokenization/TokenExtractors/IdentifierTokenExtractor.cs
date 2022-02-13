using System.Text.RegularExpressions;

namespace CompilerProject.Tokenization.TokenExtractors;

public class IdentifierTokenExtractor : ITokenExtractor
{
    private readonly TokenTable tokenTable;

    public IdentifierTokenExtractor(TokenTable tokenTable)
    {
        this.tokenTable = tokenTable;
    }
    public bool TryGetToken(ref string input, ref IList<Token> tokens)
    {
        var identifier = TokenExtractorHelper.GetKeywordOrIdentifierString(input, tokenTable);
        if (!string.IsNullOrEmpty(identifier))
        {
            tokens.Add(new Token(TokenValueType.Identifier, identifier));
            input = input.Remove(0, identifier.Length);
            return true;
        }

        return false;
    }
}