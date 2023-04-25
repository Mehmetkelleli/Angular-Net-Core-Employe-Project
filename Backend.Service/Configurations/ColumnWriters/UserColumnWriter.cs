using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace Backend.Service.Configurations.ColumnWriters
{
    public class UserColumnWriter : ColumnWriterBase
    {
        public UserColumnWriter() : base(NpgsqlDbType.Varchar) { }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            var (username,values) = logEvent.Properties.FirstOrDefault(i => i.Key == "user_name");
            return values == null ? "user" : values.ToString();
        }
    }
}
