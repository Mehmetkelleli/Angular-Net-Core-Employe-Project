using Backend.Domain.EntityModels;

namespace Backend.Application.Dtos
{
    public class EmployeWithWage
    {
        public Employe Employe { get; set; }
        public double TotalWage { get; set; }
        public int TotalHours { get; set; }
        public string[] Roles { get; set; }
        public double? TotalPrice { get; set; }
        public int? TotalPriceHour { get; set; }
    }
}
