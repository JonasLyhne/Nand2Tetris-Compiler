using System.Text.RegularExpressions;

namespace CompilerProject.Tokenization.TokenExtractors;

public class IntegerTokenExtractor : ITokenExtractor
{
    public bool TryGetToken(ref string input, ref IList<Token> tokens)
    {
        if (int.TryParse(input[0].ToString(), out _))
        {
            var endIndex = input.IndexOf(' ', 0);
            var inputPart = endIndex >= 0 ? input[..endIndex] : input;
            var intPart = Regex.Match(inputPart, @"\d+").Value;
            if (int.TryParse(intPart, out _))
            {
                tokens.Add(new Token(TokenValueType.Integer, intPart));
                input = input.Remove(0, intPart.Length);
                return true;
            }
        }

        return false;
    }
}