using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GuessLetterResponse : Response
{
    public string hangman;
    public string token;
    public bool correct;
}