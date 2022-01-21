using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GuessLetterRequest : Request
{
    public string token;
    public string letter;
}