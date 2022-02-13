using CompilerProject.Tokenization.TokenExtractors;

namespace CompilerProject.Tokenization;

public class Tokenizer
{
    private TokenTable tokenTable;
    private readonly IList<ITokenExtractor> tokenExtractors;
    // private string input;

    public Tokenizer(TokenTable tokenTable)
    {
        this.tokenTable = tokenTable;
        tokenExtractors = new List<ITokenExtractor>()
        {
            new StringTokenExtractor(),
            new IntegerTokenExtractor(),
            new SymbolTokenExtractor(this.tokenTable),
            new KeywordTokenExtractor(this.tokenTable),
            new IdentifierTokenExtractor(this.tokenTable)
        };
    }


    /// <summary>
    /// Tokenizes an input string containing jack code.
    /// Splitting segments of code into tokens.
    /// </summary>
    /// <param name="inputString">String value containing jack Code</param>
    /// <returns>IReadOnlyCollection of Tokens</returns>
    public IReadOnlyCollection<Token> Tokenize(IList<string> inputString)
    {
        return ExtractTokens(RemoveEmptyLines(RemoveComments(inputString)));
    }
    
    private IReadOnlyCollection<Token> ExtractTokens(IList<string> input)
    {
        IList<Token> tokens = new List<Token>();
        foreach (var line in input)
        {
            var refLine = line.Trim();
            while (refLine.Length > 0)
            {
                refLine = refLine.Trim();
                foreach (var tokenExtractor in tokenExtractors)
                {
                    if (tokenExtractor.TryGetToken(ref refLine, ref tokens)) break;
                }
            }
        }
        return tokens as IReadOnlyCollection<Token> ?? throw new InvalidOperationException("No tokens were extracted");
    }

    /// <summary>
    /// Removes Single line comments from the input string.
    /// </summary>
    private IList<string> RemoveComments(IList<string> input)
    {
        return input.Where(l => !l.Contains("//", StringComparison.Ordinal)).ToList();
    }

    /// <summary>
    /// Removes string that consists of empty lines. 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private IList<string> RemoveEmptyLines(IList<string> input)
    {
        return input.Where(l => !string.IsNullOrEmpty(l)).ToList();
    }
}