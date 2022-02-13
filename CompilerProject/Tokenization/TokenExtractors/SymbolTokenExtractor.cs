namespace CompilerProject.Tokenization.TokenExtractors;

public class SymbolTokenExtractor : ITokenExtractor
{
    private readonly TokenTable tokenTable;

    public SymbolTokenExtractor(TokenTable tokenTable)
    {
        this.tokenTable = tokenTable;
    }
    public bool TryGetToken(ref string input, ref IList<Token> tokens)
    {
        if (tokenTable.Symbols.Contains(input[0]))
        {
            tokens.Add(new Token(TokenValueType.Symbol, input[..1]));
            input = input.Remove(0, 1);
            return true;
        }

        return false;
    }
}