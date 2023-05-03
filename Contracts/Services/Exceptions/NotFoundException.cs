using System.Runtime.Serialization;

namespace Contracts.Services.Exceptions
{
    public class URLNotFoundException : Exception
    {

        public URLNotFoundException() : base("URL Bulunamadı!")
        {
        }

        public URLNotFoundException(string? message) : base(message)
        {
        }

        public URLNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
