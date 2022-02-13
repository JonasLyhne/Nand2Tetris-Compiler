namespace CompilerProject.Tokenization.TokenExtractors;

public class StringTokenExtractor : ITokenExtractor
{
    public bool TryGetToken(ref string input, ref IList<Token> tokens)
    {
        if (input[0] == '"')
        {
            var stringEnd = input.IndexOf('"', 1);
            var stringValue = input[..stringEnd].Replace("\"", "");
            tokens.Add(new Token(TokenValueType.String, stringValue));
            input = input.Remove(0, stringValue.Length + 2);
            return true;
        }

        return false;
    }
}