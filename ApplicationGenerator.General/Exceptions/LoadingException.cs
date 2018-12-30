using System;

namespace ApplicationGenerator.General.Exceptions
{
    public class LoadingException : Exception
    {
       public LoadingException(string message, Exception baseException) : base(message, baseException)
        {
           
        }
    }
}
