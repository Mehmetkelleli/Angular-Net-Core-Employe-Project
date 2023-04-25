namespace Backend.Domain.EntityModels
{
    public class PersonelTask:BaseClass
    {
        public int WageHours{ get; set; }
        public bool WageHourState { get; set; }
        public DateTime CurrentDatetime { get; set; }
        public string EmployeId { get; set; }
        public Employe Employe { get; set; }
        public bool State{ get; set; }
    }
}
