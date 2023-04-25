using Microsoft.AspNetCore.Http;

namespace Backend.Application.Abstractions
{
    public interface IStorage
    {
        Task<bool> SendFile(IFormFile File, string extensions,string path);
        string ReName(string Name);
        Task<bool> Delete(string FileName,string path);
    }
}
