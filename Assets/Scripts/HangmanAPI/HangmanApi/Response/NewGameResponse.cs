using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class NewGameResponse : Response
{
    public string hangman;
    public string token;
}