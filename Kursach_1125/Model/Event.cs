using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_1125.Model
{
    public class Event
    {
        public int Id { get; set; }
        public int TPKZoneId { get; set; }
        public int TentantId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
