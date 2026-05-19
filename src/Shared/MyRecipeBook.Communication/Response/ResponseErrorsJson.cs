namespace MyRecipeBook.Communication.Response;

public class ResponseErrorsJson
{
    public IList<string> Erros { get; set; }

    public ResponseErrorsJson(IList<string> erros)
    {
        Erros = erros;           
    }

    public ResponseErrorsJson(string errors)
    {
        Erros = new List<string>();
        Erros.Add(errors);           
    }
}
