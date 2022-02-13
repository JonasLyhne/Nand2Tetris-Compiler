namespace CompilerProject.Tokenization;

public class Token
{
    public TokenValueType Type { get; }

    public string Value { get; }

    public Token(TokenValueType type, string value)
    {
        Type = type;
        Value = value;
    }
}