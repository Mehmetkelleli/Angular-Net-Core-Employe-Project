using Backend.Domain.EntityModels;

namespace Backend.Application.Dtos
{
    public class RoleWithUserDto
    {
        public Role Role{ get; set; }
        public IQueryable<Employe> Employes{ get; set; }
    }
}
