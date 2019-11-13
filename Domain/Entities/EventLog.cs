using System;
using System.Collections.Generic;
using System.Text;

namespace Exam2C2P.Domain.Entities
{
    public class EventLog
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
    }
}
