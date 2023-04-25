using System;
using System.Text.Json;

namespace Backend.Domain.EntityModels
{
    public class BaseClass
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? OldObject{ get; set; }
        public string? UpdateUserName { get; set; }

        public T GetOldObject<T>() where T :class
        {
            return JsonSerializer.Deserialize<T>(this.OldObject);
        }
    }
}
