using Backend.Domain.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence.Database
{
    public class DataContext:IdentityDbContext<Employe,Role,string>
    {
        public DataContext(DbContextOptions options):base(options) { }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PersonelTask> PersonelTasks { get; set; }
    }
}
