using Microsoft.AspNetCore.Identity;

namespace Backend.Domain.EntityModels
{
    public class Role:IdentityRole<string>
    {
        public double Wage{ get; set; }
        public double OvertimePay{ get; set; }
    }
}
