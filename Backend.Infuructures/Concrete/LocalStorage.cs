using Backend.Application.Abstractions;
namespace Backend.Infuructures.Concrete
{
    public class LocalStorage : IStorage
    {
        public async Task<bool> Delete(string FileName,string path)
        {
            System.IO.File.Delete($"wwwroot\\{path}\\{FileName}");
            return true;
        }

        public string ReName(string Name)
        {
            var ame = DateTime.Now.Ticks+Name;
            ame = ame.ToLower().Trim()
                 .Replace(" ", "-")
                 .Replace("/", "-")
                 .Replace("@", "-")
                 .Replace("=", "-").
                  Replace('ı', 'i').
                  Replace('ö', 'o').
                  Replace('ü', 'u').
                  Replace('ş', 's').
                  Replace('ğ', 'g').
                  Replace('ç', 'c').
                  Replace('İ', 'I').
                  Replace('Ö', 'O').
                  Replace('Ü', 'U').
                  Replace('Ş', 'S').
                  Replace('Ğ', 'G').
                  Replace('Ç', 'C');
            return ame;
        }

        public async Task<bool> SendFile(Microsoft.AspNetCore.Http.IFormFile File, string extensions,string path)
        {
            if (!Directory.Exists($"wwwroot\\{path}"))
            {
                Directory.CreateDirectory($"wwwroot\\{path}");
            }
            if(Path.GetExtension(File.FileName) != extensions)
            {
                return false;
            }

            var patha = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{path}", ReName(File.FileName));
            using (var stream = new FileStream(patha, FileMode.Create))
            {
                await File.CopyToAsync(stream);
            }
            
            return true;
        }
    }
}
