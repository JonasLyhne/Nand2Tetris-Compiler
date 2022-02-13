namespace CompilerProject.Tokenization.TokenExtractors;

/// <summary>
/// Interface used for extracting/parsing
/// tokens from a string value containing Jack code.
/// </summary>
public interface ITokenExtractor
{
    bool TryGetToken(ref string input, ref IList<Token> tokens);
}