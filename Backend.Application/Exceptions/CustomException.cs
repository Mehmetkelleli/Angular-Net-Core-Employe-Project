using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Backend.Application.Exceptions
{
    public class CustomException:Exception
    {
        public CustomException(string[] error) : base(JsonSerializer.Serialize(error)) { }
    }
    public class NotFoundExc:Exception
    {
        public NotFoundExc(string[] error) : base(JsonSerializer.Serialize(error)) { }
    }
}
