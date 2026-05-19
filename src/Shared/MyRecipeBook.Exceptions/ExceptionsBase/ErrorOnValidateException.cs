namespace MyRecipeBook.Exceptions.ExceptionsBase
{
    public class ErrorOnValidateException : MyRecipeBookException
    {
        public IList<string> ErrorMessage { get; set; }

        public ErrorOnValidateException(IList<string> errorMessage)
        {
            ErrorMessage = errorMessage;  
        }
    }
}
