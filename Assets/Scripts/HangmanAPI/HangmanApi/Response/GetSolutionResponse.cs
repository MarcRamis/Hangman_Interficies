using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GetSolutionResponse : Response
{
    public string solution;
    public string token;
}