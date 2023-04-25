using Backend.Domain.EntityModels;

namespace Backend.Application.Dtos
{
    public class RoleWithUserByIdDto
    {
        public Role Role { get; set; }
        public IQueryable<Employe> Employes { get; set; }
        public IQueryable<Employe> NonEmployes{ get; set; }
    }
}
