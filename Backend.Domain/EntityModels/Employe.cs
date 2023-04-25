using Microsoft.AspNetCore.Identity;

namespace Backend.Domain.EntityModels
{
    public class Employe:IdentityUser<string>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string ImagePath { get; set; } = "Default.png";
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate{ get; set; }
        public ICollection<PersonelTask> PersonelTasks{ get; set; }
    }
}
